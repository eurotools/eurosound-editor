//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Sortable ListView 
//-------------------------------------------------------------------------------------------------------------------------------
using sb_editor.Custom_Controls;
using System.Windows.Forms;

namespace sb_editor.Panels
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class ListView_ColumnSortingClick : ListView
    {
        private ColumnHeader SortingColumn = null;
        private SortOrder sort_order = SortOrder.Ascending;

        //-------------------------------------------------------------------------------------------------------------------------------
        public ListView_ColumnSortingClick()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ListView_Extended_ColumnSorting_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //Get the new sorting column.
            ColumnHeader new_sorting_column = Columns[e.Column];

            //Figure out the new sorting order.
            if (SortingColumn == null)
            {
                //New column. Sort ascending.
                sort_order = SortOrder.Descending;
            }
            else
            {
                //See if this is the same column.
                if (new_sorting_column == SortingColumn)
                {
                    //Same column. Switch the sort order.
                    if (sort_order == SortOrder.Ascending)
                    {
                        sort_order = SortOrder.Descending;
                    }
                    else
                    {
                        sort_order = SortOrder.Ascending;
                    }
                }
                else
                {
                    //New column. Sort ascending.
                    sort_order = SortOrder.Ascending;
                }
            }

            //Display the new sort order.
            SortingColumn = new_sorting_column;

            //Create a comparer.
            ListViewItemSorter = new ListViewColumnSorter(e.Column, sort_order);

            //Sort.
            Sort();
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
