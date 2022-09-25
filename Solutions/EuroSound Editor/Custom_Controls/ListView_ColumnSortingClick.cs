using EuroSound_Editor.Custom_Controls;
using System.Windows.Forms;

namespace EuroSound_Editor.Panels
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
