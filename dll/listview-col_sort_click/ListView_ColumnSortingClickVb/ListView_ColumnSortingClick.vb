Imports System.Windows.Forms

Partial Public Class ListView_ColumnSortingClick
    Inherits ListView

    Private SortingColumn As ColumnHeader = Nothing

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub ListView_ColumnSortingClick_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles MyBase.ColumnClick
        If Not (Disposing OrElse IsDisposed) Then
            'Get the new sorting column.
            Dim new_sorting_column As ColumnHeader = Columns(e.Column)

            'Figure out the new sorting order.
            Dim sort_order As SortOrder
            If SortingColumn Is Nothing Then
                'New column. Sort ascending.
                sort_order = SortOrder.Ascending
            Else
                'See if this is the same column.
                If new_sorting_column Is SortingColumn Then
                    'Same column. Switch the sort order.
                    If SortingColumn.Text.StartsWith("> ") Then
                        sort_order = SortOrder.Descending
                    Else
                        sort_order = SortOrder.Ascending
                    End If
                Else
                    'New column. Sort ascending.
                    sort_order = SortOrder.Ascending
                End If

                'Remove the old sort indicator.
                SortingColumn.Text = SortingColumn.Text.Substring(2)
            End If

            'Display the new sort order.
            SortingColumn = new_sorting_column
            If sort_order = SortOrder.Ascending Then
                SortingColumn.Text = "> " & SortingColumn.Text
            Else
                SortingColumn.Text = "< " & SortingColumn.Text
            End If

            'Create a comparer.
            ListViewItemSorter = New ListView_ColumnSorting(e.Column, sort_order)

            'Sort.
            Sort()
        End If
    End Sub
End Class

Friend Class ListView_ColumnSorting
    Implements IComparer

    Private ReadOnly ColumnNumber As Integer
    Private ReadOnly SortOrder As SortOrder

    Friend Sub New(column_number As Integer, sort_order As SortOrder)
        ColumnNumber = column_number
        SortOrder = sort_order
    End Sub

    'Compare two ListViewItems.
    Public Function Compare(object_x As Object, object_y As Object) As Integer Implements IComparer.Compare
        'Get the objects as ListViewItems.
        Dim item_x As ListViewItem = TryCast(object_x, ListViewItem)
        Dim item_y As ListViewItem = TryCast(object_y, ListViewItem)

        'Get the corresponding sub-item values.
        Dim string_x As String

        If item_x.SubItems.Count <= ColumnNumber Then
            string_x = ""
        Else
            string_x = item_x.SubItems(ColumnNumber).Text
        End If

        Dim string_y As String

        If item_y.SubItems.Count <= ColumnNumber Then
            string_y = ""
        Else
            string_y = item_y.SubItems(ColumnNumber).Text
        End If

        'Compare them.
        Dim result As Integer
        Dim double_x As Double = Nothing, double_y As Double = Nothing, date_x As Date = Nothing, date_y As Date = Nothing

        If Double.TryParse(string_x, double_x) AndAlso Double.TryParse(string_y, double_y) Then
            'Treat as a number.
            result = double_x.CompareTo(double_y)
        Else

            If Date.TryParse(string_x, date_x) AndAlso Date.TryParse(string_y, date_y) Then
                'Treat as a date.
                result = date_x.CompareTo(date_y)
            Else
                'Treat as a string.
                result = string_x.CompareTo(string_y)
            End If
        End If

        'Return the correct result depending on whether
        'we're sorting ascending or descending.
        If SortOrder = SortOrder.Ascending Then
            Return result
        Else
            Return -result
        End If
    End Function
End Class