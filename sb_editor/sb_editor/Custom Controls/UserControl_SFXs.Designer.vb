<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UserControl_SFXs
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label_TotalSfx = New System.Windows.Forms.Label()
        Me.GroupBox_SFX_List = New System.Windows.Forms.GroupBox()
        Me.Button_UnUsedHashCodes = New System.Windows.Forms.Button()
        Me.CheckBox_SortByDate = New System.Windows.Forms.CheckBox()
        Me.Button_ShowAll = New System.Windows.Forms.Button()
        Me.Button_UpdateList = New System.Windows.Forms.Button()
        Me.ComboBox_SFX_Section = New System.Windows.Forms.ComboBox()
        Me.ContextMenu_SFXs = New System.Windows.Forms.ContextMenu()
        Me.ContextMenuSfx_AddToDb = New System.Windows.Forms.MenuItem()
        Me.ContextMenuSfx_Properties = New System.Windows.Forms.MenuItem()
        Me.ContextMenuSfx_EditSfx = New System.Windows.Forms.MenuItem()
        Me.ContextMenuSfx_AddNewSfx = New System.Windows.Forms.MenuItem()
        Me.ContextMenuSfx_Copy = New System.Windows.Forms.MenuItem()
        Me.ContextMenuSfx_Delete = New System.Windows.Forms.MenuItem()
        Me.ContextMenuSfx_Rename = New System.Windows.Forms.MenuItem()
        Me.ContextMenuSfx_NewMultiple = New System.Windows.Forms.MenuItem()
        Me.ContextMenuSfx_MultiEditor = New System.Windows.Forms.MenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ComboBox_Temporal = New System.Windows.Forms.ComboBox()
        Me.ListBox_SFXs = New sb_editor.MultiSelListBox()
        Me.GroupBox_SFX_List.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label_TotalSfx
        '
        Me.Label_TotalSfx.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label_TotalSfx.AutoSize = True
        Me.Label_TotalSfx.Location = New System.Drawing.Point(3, 566)
        Me.Label_TotalSfx.Name = "Label_TotalSfx"
        Me.Label_TotalSfx.Size = New System.Drawing.Size(43, 13)
        Me.Label_TotalSfx.TabIndex = 1
        Me.Label_TotalSfx.Text = "Total: 0"
        '
        'GroupBox_SFX_List
        '
        Me.GroupBox_SFX_List.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_SFX_List.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox_SFX_List.Controls.Add(Me.Button_UnUsedHashCodes)
        Me.GroupBox_SFX_List.Controls.Add(Me.CheckBox_SortByDate)
        Me.GroupBox_SFX_List.Controls.Add(Me.Button_ShowAll)
        Me.GroupBox_SFX_List.Controls.Add(Me.Button_UpdateList)
        Me.GroupBox_SFX_List.Controls.Add(Me.ComboBox_SFX_Section)
        Me.GroupBox_SFX_List.Location = New System.Drawing.Point(3, 585)
        Me.GroupBox_SFX_List.Name = "GroupBox_SFX_List"
        Me.GroupBox_SFX_List.Size = New System.Drawing.Size(271, 72)
        Me.GroupBox_SFX_List.TabIndex = 2
        Me.GroupBox_SFX_List.TabStop = False
        Me.GroupBox_SFX_List.Text = "Filter SFX List"
        '
        'Button_UnUsedHashCodes
        '
        Me.Button_UnUsedHashCodes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_UnUsedHashCodes.Location = New System.Drawing.Point(109, 43)
        Me.Button_UnUsedHashCodes.Name = "Button_UnUsedHashCodes"
        Me.Button_UnUsedHashCodes.Size = New System.Drawing.Size(75, 23)
        Me.Button_UnUsedHashCodes.TabIndex = 3
        Me.Button_UnUsedHashCodes.Text = "Un-Used"
        Me.Button_UnUsedHashCodes.UseVisualStyleBackColor = True
        '
        'CheckBox_SortByDate
        '
        Me.CheckBox_SortByDate.AutoSize = True
        Me.CheckBox_SortByDate.Location = New System.Drawing.Point(6, 47)
        Me.CheckBox_SortByDate.Name = "CheckBox_SortByDate"
        Me.CheckBox_SortByDate.Size = New System.Drawing.Size(91, 17)
        Me.CheckBox_SortByDate.TabIndex = 2
        Me.CheckBox_SortByDate.Text = "Sort by Date?"
        Me.CheckBox_SortByDate.UseVisualStyleBackColor = True
        '
        'Button_ShowAll
        '
        Me.Button_ShowAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_ShowAll.Location = New System.Drawing.Point(190, 43)
        Me.Button_ShowAll.Name = "Button_ShowAll"
        Me.Button_ShowAll.Size = New System.Drawing.Size(75, 23)
        Me.Button_ShowAll.TabIndex = 4
        Me.Button_ShowAll.Text = "All"
        Me.Button_ShowAll.UseVisualStyleBackColor = True
        '
        'Button_UpdateList
        '
        Me.Button_UpdateList.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_UpdateList.Location = New System.Drawing.Point(190, 14)
        Me.Button_UpdateList.Name = "Button_UpdateList"
        Me.Button_UpdateList.Size = New System.Drawing.Size(75, 23)
        Me.Button_UpdateList.TabIndex = 1
        Me.Button_UpdateList.Text = "Update"
        Me.Button_UpdateList.UseVisualStyleBackColor = True
        '
        'ComboBox_SFX_Section
        '
        Me.ComboBox_SFX_Section.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox_SFX_Section.FormattingEnabled = True
        Me.ComboBox_SFX_Section.ItemHeight = 13
        Me.ComboBox_SFX_Section.Location = New System.Drawing.Point(6, 16)
        Me.ComboBox_SFX_Section.Name = "ComboBox_SFX_Section"
        Me.ComboBox_SFX_Section.Size = New System.Drawing.Size(178, 21)
        Me.ComboBox_SFX_Section.TabIndex = 0
        '
        'ContextMenu_SFXs
        '
        Me.ContextMenu_SFXs.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.ContextMenuSfx_AddToDb, Me.ContextMenuSfx_Properties, Me.ContextMenuSfx_EditSfx, Me.ContextMenuSfx_AddNewSfx, Me.ContextMenuSfx_Copy, Me.ContextMenuSfx_Delete, Me.ContextMenuSfx_Rename, Me.ContextMenuSfx_NewMultiple, Me.ContextMenuSfx_MultiEditor})
        '
        'ContextMenuSfx_AddToDb
        '
        Me.ContextMenuSfx_AddToDb.Index = 0
        Me.ContextMenuSfx_AddToDb.Text = "Add to DB"
        '
        'ContextMenuSfx_Properties
        '
        Me.ContextMenuSfx_Properties.Index = 1
        Me.ContextMenuSfx_Properties.Text = "Properties"
        '
        'ContextMenuSfx_EditSfx
        '
        Me.ContextMenuSfx_EditSfx.Index = 2
        Me.ContextMenuSfx_EditSfx.Text = "Edit"
        '
        'ContextMenuSfx_AddNewSfx
        '
        Me.ContextMenuSfx_AddNewSfx.Index = 3
        Me.ContextMenuSfx_AddNewSfx.Text = "New"
        '
        'ContextMenuSfx_Copy
        '
        Me.ContextMenuSfx_Copy.Index = 4
        Me.ContextMenuSfx_Copy.Text = "Copy"
        '
        'ContextMenuSfx_Delete
        '
        Me.ContextMenuSfx_Delete.Index = 5
        Me.ContextMenuSfx_Delete.Text = "Delete"
        '
        'ContextMenuSfx_Rename
        '
        Me.ContextMenuSfx_Rename.Index = 6
        Me.ContextMenuSfx_Rename.Text = "Rename"
        '
        'ContextMenuSfx_NewMultiple
        '
        Me.ContextMenuSfx_NewMultiple.Index = 7
        Me.ContextMenuSfx_NewMultiple.Text = "New Multiple"
        '
        'ContextMenuSfx_MultiEditor
        '
        Me.ContextMenuSfx_MultiEditor.Index = 8
        Me.ContextMenuSfx_MultiEditor.Text = "Multi Editor"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.ComboBox_Temporal)
        Me.Panel1.Controls.Add(Me.ListBox_SFXs)
        Me.Panel1.Location = New System.Drawing.Point(0, 32)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(277, 531)
        Me.Panel1.TabIndex = 3
        '
        'ComboBox_Temporal
        '
        Me.ComboBox_Temporal.FormattingEnabled = True
        Me.ComboBox_Temporal.Location = New System.Drawing.Point(9, 293)
        Me.ComboBox_Temporal.Name = "ComboBox_Temporal"
        Me.ComboBox_Temporal.Size = New System.Drawing.Size(221, 21)
        Me.ComboBox_Temporal.TabIndex = 2
        Me.ComboBox_Temporal.Visible = False
        '
        'ListBox_SFXs
        '
        Me.ListBox_SFXs.AllowDrop = True
        Me.ListBox_SFXs.ContextMenu = Me.ContextMenu_SFXs
        Me.ListBox_SFXs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox_SFXs.DragDropEffectVal = System.Windows.Forms.DragDropEffects.Copy
        Me.ListBox_SFXs.FormattingEnabled = True
        Me.ListBox_SFXs.HorizontalScrollbar = True
        Me.ListBox_SFXs.Location = New System.Drawing.Point(0, 0)
        Me.ListBox_SFXs.Name = "ListBox_SFXs"
        Me.ListBox_SFXs.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox_SFXs.Size = New System.Drawing.Size(277, 531)
        Me.ListBox_SFXs.Sorted = True
        Me.ListBox_SFXs.TabIndex = 1
        '
        'UserControl_SFXs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label_TotalSfx)
        Me.Controls.Add(Me.GroupBox_SFX_List)
        Me.Name = "UserControl_SFXs"
        Me.Size = New System.Drawing.Size(277, 660)
        Me.GroupBox_SFX_List.ResumeLayout(False)
        Me.GroupBox_SFX_List.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label_TotalSfx As Label
    Private WithEvents GroupBox_SFX_List As GroupBox
    Private WithEvents Button_ShowAll As Button
    Private WithEvents Button_UpdateList As Button
    Friend WithEvents Button_UnUsedHashCodes As Button
    Friend WithEvents CheckBox_SortByDate As CheckBox
    Friend WithEvents ContextMenu_SFXs As ContextMenu
    Friend WithEvents ContextMenuSfx_AddNewSfx As MenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ListBox_SFXs As MultiSelListBox
    Friend WithEvents ContextMenuSfx_AddToDb As MenuItem
    Friend WithEvents ContextMenuSfx_Properties As MenuItem
    Friend WithEvents ContextMenuSfx_EditSfx As MenuItem
    Friend WithEvents ContextMenuSfx_Copy As MenuItem
    Friend WithEvents ContextMenuSfx_Delete As MenuItem
    Friend WithEvents ContextMenuSfx_Rename As MenuItem
    Friend WithEvents ContextMenuSfx_NewMultiple As MenuItem
    Friend WithEvents ContextMenuSfx_MultiEditor As MenuItem
    Friend WithEvents ComboBox_Temporal As ComboBox
    Protected Friend WithEvents ComboBox_SFX_Section As ComboBox
End Class
