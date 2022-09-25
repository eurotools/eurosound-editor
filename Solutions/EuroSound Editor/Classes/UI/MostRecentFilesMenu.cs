using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.Menu;

namespace sb_editor.Classes
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class MostRecentFilesMenu
    {
        private ClickedHandler clickedHandler;
        protected MenuItem recentFileMenuItem;
        protected string registryKeyName;
        protected int numEntries = 0;
        protected int maxEntries = 4;
        protected int maxShortenPathLength = 30;
        protected Mutex mruStripMutex;
        #region MruMenuItem

        /// <summary>
        /// The menu item which will contain the MRU entry.
        /// </summary>
        /// <remarks>The menu may display a shortened or otherwise invalid pathname.
        /// This class stores the actual filename, preferably as a fully
        /// resolved labelName, that will be returned in the event handler.</remarks>
        public class MruMenuItem : MenuItem
        {
            /// <summary>
            /// Initializes an MruMenuItem object.
            /// </summary>
            /// <param labelName="filename">The string to actually return in the <paramref labelName="eventHandler">eventHandler</paramref>.</param>
            /// <param labelName="entryname">The string that will be displayed in the menu.</param>
            /// <param labelName="eventHandler">The <see cref="EventHandler">EventHandler</see> that 
            /// handles the <see cref="MenuItem.Click">Click</see> event for this menu item.</param>
            public MruMenuItem(string filename, string entryname, EventHandler eventHandler)
            {
                Tag = filename;
                Text = entryname;
                Click += eventHandler;
            }

            /// <summary>
            /// Gets the filename.
            /// </summary>
            /// <value>Gets the filename.</value>
            public string Filename
            {
                get
                {
                    return (string)Tag;
                }
                set
                {
                    Tag = value;
                }
            }
        }
        #endregion

        #region Construction

        protected MostRecentFilesMenu() { }

        //-------------------------------------------------------------------------------------------------------------------------------
        protected void Init(MenuItem recentFileMenuItem, ClickedHandler clickedHandler, string registryKeyName, bool loadFromRegistry, int maxEntries)
        {
            this.recentFileMenuItem = recentFileMenuItem ?? throw new ArgumentNullException("recentFileMenuItem");
            this.recentFileMenuItem.Checked = false;
            this.recentFileMenuItem.Enabled = false;

            MaxEntries = maxEntries;
            this.clickedHandler = clickedHandler;

            if (registryKeyName != null)
            {
                IniFilePath = registryKeyName;
                if (loadFromRegistry)
                {
                    LoadFromIniFile();
                }
            }
        }

        #endregion

        #region Event Handling

        public delegate void ClickedHandler(int number, string filename);

        //-------------------------------------------------------------------------------------------------------------------------------
        protected void OnClick(object sender, System.EventArgs e)
        {
            MruMenuItem menuItem = (MruMenuItem)sender;
            clickedHandler(MenuItems.IndexOf(menuItem) - StartIndex, menuItem.Filename);
        }
        #endregion

        #region Properties
        //-------------------------------------------------------------------------------------------------------------------------------
        public virtual MenuItemCollection MenuItems
        {
            get
            {
                return recentFileMenuItem.MenuItems;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public virtual int StartIndex
        {
            get
            {
                return 0;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public virtual int EndIndex
        {
            get
            {
                return numEntries;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public int MaxEntries
        {
            get
            {
                return maxEntries;
            }
            set
            {
                if (value > 16)
                {
                    maxEntries = 16;
                }
                else
                {
                    maxEntries = value < 4 ? 4 : value;

                    int index = StartIndex + maxEntries;
                    while (numEntries > maxEntries)
                    {
                        MenuItems.RemoveAt(index);
                        numEntries--;
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public int MaxShortenPathLength
        {
            get
            {
                return maxShortenPathLength;
            }
            set
            {
                maxShortenPathLength = value < 16 ? 16 : value;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public virtual bool IsInline
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Helper Methods
        //-------------------------------------------------------------------------------------------------------------------------------
        protected virtual void Enable()
        {
            recentFileMenuItem.Enabled = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        protected virtual void Disable()
        {
            recentFileMenuItem.Enabled = false;
            //recentFileMenuItem.MenuItems.RemoveAt(0);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        protected virtual void SetFirstFile(MruMenuItem menuItem)
        {
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void SetFirstFile(int number)
        {
            if (number > 0 && numEntries > 1 && number < numEntries)
            {
                MruMenuItem menuItem = (MruMenuItem)MenuItems[StartIndex + number];

                MenuItems.RemoveAt(StartIndex + number);
                MenuItems.Add(StartIndex, menuItem);

                SetFirstFile(menuItem);
                FixupPrefixes(0);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string FixupEntryname(int number, string entryname)
        {
            if (number < 9)
                return "&" + (number + 1) + "  " + entryname;
            else if (number == 9)
                return "1&0" + "  " + entryname;
            else
                return (number + 1) + "  " + entryname;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        protected void FixupPrefixes(int startNumber)
        {
            if (startNumber < 0)
                startNumber = 0;

            if (startNumber < maxEntries)
            {
                for (int i = StartIndex + startNumber; i < EndIndex; i++, startNumber++)
                {
                    int offset = MenuItems[i].Text.Substring(0, 3) == "1&0" ? 5 : 4;
                    MenuItems[i].Text = FixupEntryname(startNumber, MenuItems[i].Text.Substring(offset));
                    //					MenuItems[i].Text = FixupEntryname(startNumber, MenuItems[i].Text.Substring(startNumber == 10 ? 5 : 4));
                }
            }
        }

        /// <summary>
        /// Shortens a pathname for display purposes.
        /// </summary>
        /// <param labelName="pathname">The pathname to shorten.</param>
        /// <param labelName="maxLength">The maximum number of characters to be displayed.</param>
        /// <remarks>Shortens a pathname by either removing consecutive components of a path
        /// and/or by removing characters from the end of the filename and replacing
        /// then with three elipses (...)
        /// <para>In all cases, the root of the passed path will be preserved in it's entirety.</para>
        /// <para>If a UNC path is used or the pathname and maxLength are particularly short,
        /// the resulting path may be longer than maxLength.</para>
        /// <para>This method expects fully resolved pathnames to be passed to it.
        /// (Use Path.GetFullPath() to obtain this.)</para>
        /// </remarks>
        /// <returns></returns>
        static public string ShortenPathname(string FullPath)
        {
            string ShortPath;
            string SplittedPath;

            //Get filename
            ShortPath = Path.GetFileName(FullPath);

            //Split path
            string[] Paths = FullPath.Split(new string[] { "\\" }, StringSplitOptions.None);

            //We have more than 3 directories
            if (Paths.Length > 3)
            {
                SplittedPath = string.Join(@"\", new string[] { Paths[0], Paths[1], "...", Paths[Paths.Length - 1] });
                if (SplittedPath.Length <= 30)
                {
                    ShortPath = SplittedPath;
                }
            }
            //We have 2 directories (Root Folder Filename)
            else if (Paths.Length == 3)
            {
                SplittedPath = string.Join(@"\", new string[] { Paths[0], Paths[1], Paths[Paths.Length - 1] });
                if (SplittedPath.Length <= 30)
                {
                    ShortPath = SplittedPath;
                }
            }
            //We have root and file name
            else if (Paths.Length < 3)
            {
                SplittedPath = string.Join(@"\", new string[] { Paths[0], Paths[Paths.Length - 1] });
                if (SplittedPath.Length <= 30)
                {
                    ShortPath = SplittedPath;
                }
            }

            return ShortPath;
        }

        #endregion

        #region Get Methods

        /// <summary>
        /// Returns the entry number matching the passed filename.
        /// </summary>
        /// <param name="filename">The filename to search for.</param>
        /// <returns>The entry number of the matching filename or -1 if not found.</returns>
        public int FindFilenameNumber(string filename)
        {
            if (filename == null)
                throw new ArgumentNullException("filename");

            if (filename.Length == 0)
                throw new ArgumentException("filename");

            if (numEntries > 0)
            {
                int number = 0;
                for (int i = StartIndex; i < EndIndex; i++, number++)
                {
                    if (string.Compare(((MruMenuItem)MenuItems[i]).Filename, filename, true) == 0)
                    {
                        return number;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns the menu index of the passed filename.
        /// </summary>
        /// <param name="filename">The filename to search for.</param>
        /// <returns>The menu index of the matching filename or -1 if not found.</returns>
        public int FindFilenameMenuIndex(string filename)
        {
            int number = FindFilenameNumber(filename);
            return number < 0 ? -1 : StartIndex + number;
        }
        #endregion

        #region Add Methods
        //-------------------------------------------------------------------------------------------------------------------------------
        public void AddFile(string filename)
        {
            string pathname = Path.GetFullPath(filename);
            AddFile(pathname, ShortenPathname(pathname));
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void AddFile(string filename, string entryname)
        {
            if (filename == null)
                throw new ArgumentNullException("filename");

            if (filename.Length == 0)
                throw new ArgumentException("filename");

            if (numEntries > 0)
            {
                int index = FindFilenameMenuIndex(filename);
                if (index >= 0)
                {
                    SetFirstFile(index - StartIndex);
                    return;
                }
            }

            if (numEntries < maxEntries)
            {
                MruMenuItem menuItem = new MruMenuItem(filename, FixupEntryname(0, entryname), new System.EventHandler(OnClick));
                MenuItems.Add(StartIndex, menuItem);
                SetFirstFile(menuItem);

                if (numEntries++ == 0)
                {
                    Enable();
                }
                else
                {
                    FixupPrefixes(1);
                }
            }
            else if (numEntries > 1)
            {
                MruMenuItem menuItem = (MruMenuItem)MenuItems[StartIndex + numEntries - 1];
                MenuItems.RemoveAt(StartIndex + numEntries - 1);

                menuItem.Text = FixupEntryname(0, entryname);
                menuItem.Filename = filename;

                MenuItems.Add(StartIndex, menuItem);

                SetFirstFile(menuItem);
                FixupPrefixes(1);
            }
        }

        #endregion

        #region Remove Methods
        //-------------------------------------------------------------------------------------------------------------------------------
        public void RemoveFile(int number)
        {
            if (number >= 0 && number < numEntries)
            {
                if (--numEntries == 0)
                {
                    Disable();
                }
                else
                {
                    int startIndex = StartIndex;
                    if (number == 0)
                    {
                        SetFirstFile((MruMenuItem)MenuItems[startIndex + 1]);
                    }

                    MenuItems.RemoveAt(startIndex + number);

                    if (number < numEntries)
                    {
                        FixupPrefixes(number);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public virtual void RemoveAll()
        {
            if (numEntries > 0)
            {
                // remove all items in the sub menu
                MenuItems.Clear();
                Disable();
                numEntries = 0;
            }
        }

        #endregion

        #region Registry Methods
        //-------------------------------------------------------------------------------------------------------------------------------
        public string IniFilePath
        {
            get
            {
                return registryKeyName;
            }
            set
            {
                if (mruStripMutex != null)
                    mruStripMutex.Close();

                registryKeyName = value.Trim();
                if (registryKeyName.Length == 0)
                {
                    registryKeyName = null;
                    mruStripMutex = null;
                }
                else
                {
                    string mutexName = registryKeyName.Replace('\\', '_').Replace('/', '_') + "Mutex";
                    mruStripMutex = new Mutex(false, mutexName);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void LoadFromIniFile()
        {
            IniFile iniFunctions = new IniFile(IniFilePath);
            if (iniFunctions != null)
            {
                mruStripMutex.WaitOne();
                RemoveAll();

                for (int number = maxEntries; number > 0; number--)
                {
                    string filename = iniFunctions.Read("Recent" + (number - 1), "RecentFiles");
                    if (!string.IsNullOrEmpty(filename))
                    {
                        AddFile(filename);
                    }
                }
                mruStripMutex.ReleaseMutex();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void SaveToIniFile()
        {
            IniFile iniFunctions = new IniFile(IniFilePath);
            if (iniFunctions != null)
            {
                mruStripMutex.WaitOne();

                int i = StartIndex;
                for (; i < EndIndex; i++)
                {
                    iniFunctions.Write("Recent" + i, ((MruMenuItem)MenuItems[i]).Filename, "RecentFiles");
                }
                mruStripMutex.ReleaseMutex();
            }
        }
        #endregion
    }

    /// <summary>
    /// Represents an inline most recently used (mru) menu.
    /// </summary>
    /// <remarks>
    /// This class shows the MRU list "inline". To display
    /// the MRU list as a popup menu use <see labelName="MruMenu">MruMenu</see>.
    /// </remarks>
    public class MruStripMenuInline : MostRecentFilesMenu
    {
        protected MenuItem owningMenu;
        protected MenuItem firstMenuItem;

        #region Construction

        //private MruStripMenuInline(
        //-------------------------------------------------------------------------------------------------------------------------------
        public MruStripMenuInline(MenuItem owningMenu, MenuItem recentFileMenuItem, ClickedHandler clickedHandler, string registryKeyName, int maxEntries)
            : this(owningMenu, recentFileMenuItem, clickedHandler, registryKeyName, true, maxEntries)
        {
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public MruStripMenuInline(MenuItem owningMenu, MenuItem recentFileMenuItem, ClickedHandler clickedHandler, string registryKeyName, bool loadFromRegistry, int maxEntries)
        {
            maxShortenPathLength = 48;
            this.owningMenu = owningMenu;
            firstMenuItem = recentFileMenuItem;
            Init(recentFileMenuItem, clickedHandler, registryKeyName, loadFromRegistry, maxEntries);
        }

        #endregion

        #region Overridden Properties
        //-------------------------------------------------------------------------------------------------------------------------------
        public override MenuItemCollection MenuItems
        {
            get
            {
                return owningMenu.MenuItems;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public override int StartIndex
        {
            get
            {
                return MenuItems.IndexOf(firstMenuItem);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public override int EndIndex
        {
            get
            {
                return StartIndex + numEntries;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public override bool IsInline
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Overridden Methods
        //-------------------------------------------------------------------------------------------------------------------------------
        protected override void Enable()
        {
            MenuItems.Remove(recentFileMenuItem);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        protected override void SetFirstFile(MruMenuItem menuItem)
        {
            firstMenuItem = menuItem;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        protected override void Disable()
        {
            int index = MenuItems.IndexOf(firstMenuItem);
            MenuItems.RemoveAt(index);
            MenuItems.Add(index, recentFileMenuItem);
            firstMenuItem = recentFileMenuItem;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public override void RemoveAll()
        {
            // inline menu must remove items from the containing menu
            if (numEntries > 0)
            {
                for (int index = EndIndex - 1; index > StartIndex; index--)
                {
                    MenuItems.RemoveAt(index);
                }
                Disable();
                numEntries = 0;
            }
        }

        #endregion
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
