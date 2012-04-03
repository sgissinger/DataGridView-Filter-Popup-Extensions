namespace DgvFilterPopup
{
    partial class DgvMultiCheckBoxColumnFilter
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.checkBoxSelectAll = new System.Windows.Forms.CheckBox();
            this.checkBoxSelectNull = new System.Windows.Forms.CheckBox();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView.CheckBoxes = true;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(181, 140);
            this.listView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView.TabIndex = 2;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView_ItemChecked);
            // 
            // columnHeader
            // 
            this.columnHeader.Text = "Name";
            this.columnHeader.Width = 160;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.textBoxSearch);
            this.splitContainer.Panel1.Controls.Add(this.checkBoxSelectAll);
            this.splitContainer.Panel1.Controls.Add(this.checkBoxSelectNull);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.listView);
            this.splitContainer.Size = new System.Drawing.Size(181, 200);
            this.splitContainer.SplitterDistance = 59;
            this.splitContainer.SplitterWidth = 1;
            this.splitContainer.TabIndex = 3;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Location = new System.Drawing.Point(0, 0);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(178, 20);
            this.textBoxSearch.TabIndex = 1;
            // 
            // checkBoxSelectAll
            // 
            this.checkBoxSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxSelectAll.AutoSize = true;
            this.checkBoxSelectAll.Location = new System.Drawing.Point(4, 26);
            this.checkBoxSelectAll.Name = "checkBoxSelectAll";
            this.checkBoxSelectAll.Size = new System.Drawing.Size(76, 17);
            this.checkBoxSelectAll.TabIndex = 0;
            this.checkBoxSelectAll.Text = "(Select All)";
            this.checkBoxSelectAll.ThreeState = true;
            this.checkBoxSelectAll.UseVisualStyleBackColor = true;
            this.checkBoxSelectAll.CheckStateChanged += new System.EventHandler(this.checkBoxSelectAll_CheckStateChanged);
            // 
            // checkBoxSelectNull
            // 
            this.checkBoxSelectNull.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxSelectNull.AutoSize = true;
            this.checkBoxSelectNull.Location = new System.Drawing.Point(4, 43);
            this.checkBoxSelectNull.Name = "checkBoxSelectNull";
            this.checkBoxSelectNull.Size = new System.Drawing.Size(34, 17);
            this.checkBoxSelectNull.TabIndex = 0;
            this.checkBoxSelectNull.Text = "Ø";
            this.checkBoxSelectNull.UseVisualStyleBackColor = true;
            this.checkBoxSelectNull.CheckedChanged += new System.EventHandler(this.checkBoxSelectNull_CheckedChanged);
            // 
            // DgvMultiCheckBoxColumnFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.splitContainer);
            this.Name = "DgvMultiCheckBoxColumnFilter";
            this.Size = new System.Drawing.Size(181, 200);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeader;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.CheckBox checkBoxSelectAll;
        private System.Windows.Forms.CheckBox checkBoxSelectNull;
        private System.Windows.Forms.TextBox textBoxSearch;
    }
}
