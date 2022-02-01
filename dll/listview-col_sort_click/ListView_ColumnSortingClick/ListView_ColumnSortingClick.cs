using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ListView_ColumnSortingClick
{
    public partial class ListView_ColumnSortingClick : ListView
    {
        private ColumnHeader SortingColumn = null;

        public ListView_ColumnSortingClick()
        {
            InitializeComponent();
        }

        private void ListView_Extended_ColumnSorting_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (!(Disposing || IsDisposed))
            {
                //Get the new sorting column.
                ColumnHeader new_sorting_column = Columns[e.Column];

                //Figure out the new sorting order.
                SortOrder sort_order;
                if (SortingColumn == null)
                {
                    //New column. Sort ascending.
                    sort_order = SortOrder.Ascending;
                }
                else
                {
                    //See if this is the same column.
                    if (new_sorting_column == SortingColumn)
                    {
                        //Same column. Switch the sort order.
                        if (SortingColumn.Text.StartsWith("> "))
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

                    //Remove the old sort indicator.
                    SortingColumn.Text = SortingColumn.Text.Substring(2);
                }

                //Display the new sort order.
                SortingColumn = new_sorting_column;
                if (sort_order == SortOrder.Ascending)
                {
                    SortingColumn.Text = "> " + SortingColumn.Text;
                }
                else
                {
                    SortingColumn.Text = "< " + SortingColumn.Text;
                }

                //Create a comparer.
                ListViewItemSorter = new ListView_ColumnSorting(e.Column, sort_order);

                //Sort.
                Sort();
            }
        }
    }

    internal class ListView_ColumnSorting : System.Collections.IComparer
    {
        private readonly int ColumnNumber;
        private readonly SortOrder SortOrder;

        internal ListView_ColumnSorting(int column_number, SortOrder sort_order)
        {
            ColumnNumber = column_number;
            SortOrder = sort_order;
        }

        //Compare two ListViewItems.
        public int Compare(object object_x, object object_y)
        {
            //Get the objects as ListViewItems.
            ListViewItem item_x = object_x as ListViewItem;
            ListViewItem item_y = object_y as ListViewItem;

            //Get the corresponding sub-item values.
            string string_x;

            if (item_x.SubItems.Count <= ColumnNumber)
            {
                string_x = "";
            }
            else
            {
                string_x = item_x.SubItems[ColumnNumber].Text;
            }

            string string_y;
            if (item_y.SubItems.Count <= ColumnNumber)
            {
                string_y = "";
            }
            else
            {
                string_y = item_y.SubItems[ColumnNumber].Text;
            }

            //Compare them.
            int result;
            if (double.TryParse(string_x, out double double_x) && double.TryParse(string_y, out double double_y))
            {
                //Treat as a number.
                result = double_x.CompareTo(double_y);
            }
            else
            {
                if (DateTime.TryParse(string_x, out DateTime date_x) && DateTime.TryParse(string_y, out DateTime date_y))
                {
                    //Treat as a date.
                    result = date_x.CompareTo(date_y);
                }
                else
                {
                    //Treat as a string.
                    result = string_x.CompareTo(string_y);
                }
            }

            //Return the correct result depending on whether
            //we're sorting ascending or descending.
            if (SortOrder == SortOrder.Ascending)
            {
                return result;
            }
            else
            {
                return -result;
            }
        }
    }
}
