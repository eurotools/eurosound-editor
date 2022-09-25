using System;
using System.Collections;
using System.Windows.Forms;

namespace sb_editor.Custom_Controls
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class ListViewColumnSorter : IComparer
    {
        private readonly int ColumnNumber;
        private readonly SortOrder SortOrder;

        //-------------------------------------------------------------------------------------------------------------------------------
        internal ListViewColumnSorter(int column_number, SortOrder sort_order)
        {
            ColumnNumber = column_number;
            SortOrder = sort_order;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public int Compare(object object_x, object object_y)
        {
            //Get the objects as ListViewItems.
            ListViewItem item_x = object_x as ListViewItem;
            ListViewItem item_y = object_y as ListViewItem;

            //Get the corresponding sub-item values.
            string string_x;
            if (item_x.SubItems.Count <= ColumnNumber)
            {
                string_x = string.Empty;
            }
            else
            {
                string_x = item_x.SubItems[ColumnNumber].Text;
            }

            string string_y;
            if (item_y.SubItems.Count <= ColumnNumber)
            {
                string_y = string.Empty;
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

    //-------------------------------------------------------------------------------------------------------------------------------
}
