using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using System.Globalization;

namespace DgvFilterPopup
{
    /// <summary>
    /// A standard <i>column filter</i> implementation for textbox columns.
    /// </summary>
    public partial class DgvMultiCheckBoxColumnFilter : DgvBaseColumnFilter
    {
        #region MEMBERS
        /// <summary>
        /// TODO: Documentation Member
        /// </summary>
        private List<String> checkedItems = new List<String>();
        /// <summary>
        /// TODO: Documentation Member
        /// </summary>
        private DataTable distinctDataTable;
        /// <summary>
        /// TODO: Documentation Member
        /// </summary>
        private Int32 maxCaptionItems;
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Gets the ComboBox ctl containing the available operators.
        /// </summary>
        public ListView ListView
        {
            get { return listView; }
        }
        #endregion

        #region CONSTRUCTORS & FINALIZERS
        /// <summary>
        /// Initializes a new userPermissions of the <see cref="DgvTextBoxColumnFilter"/> class.
        /// </summary>
        public DgvMultiCheckBoxColumnFilter() :
            this(null, 5)
        { }

        /// <summary>
        /// Initializes a new userPermissions of the <see cref="DgvTextBoxColumnFilter"/> class.
        /// </summary>
        /// <param name="maxItems"></param>
        public DgvMultiCheckBoxColumnFilter(Int32? width) :
            this(width, 5)
        { }

        /// <summary>
        /// Initializes a new userPermissions of the <see cref="DgvTextBoxColumnFilter"/> class.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="maxCaptionItems"></param>
        public DgvMultiCheckBoxColumnFilter(Int32? width, Int32 maxCaptionItems)
        {
            InitializeComponent();

            if (width.HasValue)
            {
                Int32 offsetWidth = width.Value - this.Width;

                this.columnHeader.Width += offsetWidth;
                this.Width += offsetWidth;
            }

            this.maxCaptionItems = maxCaptionItems;

            this.textBoxSearch.TextChanged += new EventHandler(this.textBoxSearch_TextChanged);
        }
        #endregion

        #region FILTER EVENTS
        /// <summary>
        /// TODO: Documentation ProcessDialogKey
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override Boolean ProcessDialogKey(Keys keyData)
        {
            if ((keyData & Keys.Alt) == Keys.Alt)
                return false;

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// Perform filter initialitazion and raises the FilterInitializing event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> userPermissions containing the event data.</param>
        /// <remarks>
        /// When this <i>column filter</i> ctl is added to the <i>column filters</i> array of the <i>filter manager</i>,
        /// the latter calls the <see cref="DgvBaseColumnFilter.Init"/> method which, in turn, calls this method.
        /// You can ovverride this method to provide initialization code or you can create an event handler and 
        /// set the <i>Cancel</i> ctl of event argument to true, to skip standard initialization.
        /// </remarks>
        protected override void OnFilterInitializing(object sender, CancelEventArgs e)
        {
            base.OnFilterInitializing(sender, e);

            if (e.Cancel) return;

            if (!(this.DataGridViewColumn is DataGridViewComboBoxColumn))
            {
                this.distinctDataTable = this.BoundDataView.ToTable(true, new String[] { this.DataGridViewColumn.DataPropertyName });
                this.distinctDataTable.DefaultView.Sort = this.DataGridViewColumn.DataPropertyName;
            }

            this.FilterListView(null);
        }

        /// <summary>
        /// Builds the filter expression and raises the FilterExpressionBuilding event
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> userPermissions containing the event data.</param>
        /// <remarks>
        /// Override <b>OnFilterExpressionBuilding</b> to provide a filter expression construction
        /// logic and to set the values of the <see cref="DgvBaseColumnFilter.FilterExpression"/> and <see cref="DgvBaseColumnFilter.FilterCaption"/> properties.
        /// The <see cref="DgvFilterManager"/> will use these properties in constructing the whole filter expression and to change the header text of the filtered column.
        /// Otherwise, you can create an event handler and set the <i>Cancel</i> ctl of event argument to true, to skip standard filter expression building logic.
        /// </remarks>
        protected override void OnFilterExpressionBuilding(object sender, CancelEventArgs e)
        {
            base.OnFilterExpressionBuilding(sender, e);

            if (e.Cancel)
            {
                FilterManager.RebuildFilter();
                return;
            }

            FilterResult filterResult = new FilterResult();
            filterResult.Caption = this.OriginalDataGridViewColumnHeaderText;

            if (this.checkBoxSelectNull.Checked)
            {
                filterResult.Expression = GetNullCondition(this.DataGridViewColumn.DataPropertyName);
                filterResult.Caption += "\n= Ø";
            }

            if (this.checkedItems.Count > 0)
            {
                if (!String.IsNullOrEmpty(filterResult.Expression))
                    filterResult.Expression += " OR ";

                filterResult.Expression += this.DataGridViewColumn.DataPropertyName + " IN (";
            }

            Int32 i = 0;

            foreach (String filter in this.checkedItems)
            {
                if (!String.IsNullOrEmpty(filter))
                    filterResult = this.ApplyFilter(filterResult, filter, i);

                i++;
            }

            if (this.checkedItems.Count > 0)
                filterResult.Expression += filterResult.Expression.Substring(0, filterResult.Expression.Length - 1) + ")";

            if (i > this.maxCaptionItems)
                filterResult.Caption += "\n...";

            if (!String.IsNullOrEmpty(filterResult.Expression))
            {
                FilterExpression = filterResult.Expression;
                FilterCaption = filterResult.Caption;
                FilterManager.RebuildFilter();
            }
            else
            {
                FilterManager.ActivateFilter(false, this.DataGridViewColumn.Index);
            }
        }

        /// <summary>
        /// TODO: Documentation OnFilterChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFilterChanged(object sender, EventArgs e)
        {
            if (!FilterApplySoon || !this.Visible)
                return;

            Active = true;
            FilterExpressionBuild();
        }
        #endregion

        #region METHODS
        /// <summary>
        /// TODO: Documentation ApplyFilter
        /// </summary>
        /// <param name="filterResult"></param>
        /// <param name="filterText"></param>
        /// <returns></returns>
        private FilterResult ApplyFilter(FilterResult filterResult, String filterText, Int32 index)
        {
            if (index < this.maxCaptionItems)
                filterResult.Caption += "\n";

            if (ColumnDataType == typeof(String))
            {
                // Managing the string-column case
                String escapedFilterValue = DgvBaseColumnFilter.StringEscape(filterText.ToString());

                filterResult.Expression += "'" + escapedFilterValue + "',";

                if (index < this.maxCaptionItems)
                    filterResult.Caption += "= " + filterText;
            }
            else
            {
                // Managing the other cases
                String formattedValue = DgvBaseColumnFilter.FormatValue(filterText, this.ColumnDataType);

                if (!String.IsNullOrEmpty(formattedValue))
                {
                    filterResult.Expression += formattedValue + ",";

                    if (index < this.maxCaptionItems)
                        filterResult.Caption += "= " + filterText;
                }
            }

            return filterResult;
        }

        /// <summary>
        /// TODO: Documentation FilterListView
        /// </summary>
        /// <param name="filter"></param>
        private void FilterListView(String filter)
        {
            this.listView.ItemChecked -= this.listView_ItemChecked;
            this.listView.SuspendLayout();
            this.listView.Clear();
            this.listView.Columns.Add(this.columnHeader);

            if (this.DataGridViewColumn is DataGridViewComboBoxColumn)
            {
                foreach (Object item in ((DataGridViewComboBoxColumn)DataGridViewColumn).Items)
                {
                    String itemValue = item.ToString();

                    if (String.IsNullOrEmpty(filter) || itemValue.ToLower().Contains(filter.ToLower()))
                        this.listView.Items.Add(itemValue);
                }
            }
            else
            {
                foreach (DataRow item in this.distinctDataTable.Rows)
                {
                    String itemValue = item[this.DataGridViewColumn.DataPropertyName].ToString();

                    if (String.IsNullOrEmpty(filter) || itemValue.ToLower().Contains(filter.ToLower()))
                        this.listView.Items.Add(itemValue);
                }
            }

            foreach (String item in this.checkedItems)
            {
                ListViewItem itemList = this.listView.FindItemWithText(item);

                if (itemList != null)
                    itemList.Checked = true;
            }

            this.listView.ResumeLayout(false);
            this.listView.ItemChecked += this.listView_ItemChecked;
        }
        #endregion

        #region EVENTS
        /// <summary>
        /// TODO: Documentation textBoxSearch_TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            this.FilterListView(this.textBoxSearch.Text);
        }

        /// <summary>
        /// TODO: Documentation listView_ItemChecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                if (!this.checkedItems.Contains(e.Item.Text))
                    this.checkedItems.Add(e.Item.Text);
            }
            else
            {
                if (this.checkedItems.Contains(e.Item.Text))
                    this.checkedItems.Remove(e.Item.Text);
            }

            this.checkBoxSelectAll.CheckStateChanged -= this.checkBoxSelectAll_CheckStateChanged;

            if (this.checkedItems.Count == this.listView.Items.Count)
                this.checkBoxSelectAll.CheckState = CheckState.Checked;
            else if (this.checkedItems.Count == 0)
                this.checkBoxSelectAll.CheckState = CheckState.Unchecked;
            else
                this.checkBoxSelectAll.CheckState = CheckState.Indeterminate;

            this.checkBoxSelectAll.CheckStateChanged += this.checkBoxSelectAll_CheckStateChanged;

            this.OnFilterChanged(sender, e);
        }

        /// <summary>
        /// TODO: Documentation textBoxSearch_TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSelectNull_CheckedChanged(object sender, EventArgs e)
        {
            this.OnFilterChanged(sender, e);
        }

        /// <summary>
        /// TODO: Documentation checkBoxSelectAll_CheckStateChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSelectAll_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.checkBoxSelectAll.CheckState != CheckState.Indeterminate)
            {
                this.listView.ItemChecked -= this.listView_ItemChecked;
                this.checkBoxSelectNull.CheckedChanged -= this.checkBoxSelectNull_CheckedChanged;

                this.checkedItems = new List<String>();

                if (this.checkBoxSelectAll.CheckState == CheckState.Checked)
                {
                    this.checkBoxSelectNull.Checked = true;

                    foreach (ListViewItem item in this.listView.Items)
                    {
                        item.Checked = true;
                        this.checkedItems.Add(item.Text);
                    }
                }
                else if (this.checkBoxSelectAll.CheckState == CheckState.Unchecked)
                {
                    this.checkBoxSelectNull.Checked = false;

                    foreach (ListViewItem item in this.listView.Items)
                        item.Checked = false;
                }

                this.listView.ItemChecked += this.listView_ItemChecked;
                this.checkBoxSelectNull.CheckedChanged += this.checkBoxSelectNull_CheckedChanged;

                this.OnFilterChanged(sender, e);
            }
        }
        #endregion
    }
}