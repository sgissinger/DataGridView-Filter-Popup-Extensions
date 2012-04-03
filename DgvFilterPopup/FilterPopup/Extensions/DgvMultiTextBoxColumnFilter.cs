using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DgvFilterPopup
{
    /// <summary>
    /// A standard <i>column filter</i> implementation for textbox columns.
    /// </summary>
    public partial class DgvMultiTextBoxColumnFilter : DgvBaseColumnFilter
    {
        #region
        /// <summary>
        /// TODO: Documentation Member
        /// </summary>
        //private Int32 lastIndex;
        /// <summary>
        /// TODO: Documentation Member
        /// </summary>
        private List<FilterComponents> filters = new List<FilterComponents>();
        /// <summary>
        /// TODO: Documentation Member
        /// </summary>
        private Object[] comboItems;
        /// <summary>
        /// TODO: Documentation Member
        /// </summary>
        private Object[] comboItemsWithoutNull;
        /// <summary>
        /// TODO: Documentation Member
        /// </summary>
        private Object[] comboItemsAndOr = new Object[] { "OR", "AND" };
        /// <summary>
        /// TODO: Documentation Member
        /// </summary>
        private Int32 comboWidth;
        /// <summary>
        /// TODO: Documentation Member
        /// </summary>
        private Int32 textBoxWidth;
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Gets the ComboBox ctl containing the available operators.
        /// </summary>
        public ComboBox ComboBoxOperator
        {
            get { return comboBoxOperator; }
        }

        /// <summary>
        /// Gets the TextBox ctl containing the value.
        /// </summary>
        public TextBox TextBoxValue
        {
            get { return textBoxValue; }
        }
        #endregion

        #region CONSTRUCTOR & FINALIZERS
        /// <summary>
        /// Initializes a new userPermissions of the <see cref="DgvTextBoxColumnFilter"/> class.
        /// </summary>
        public DgvMultiTextBoxColumnFilter()
        {
            InitializeComponent();

            comboBoxOperator.SelectedValueChanged += new EventHandler(this.OnFilterChanged);
            textBoxValue.TextChanged += new EventHandler(this.OnFilterChanged);
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

            if (ColumnDataType == typeof(String))
            {
                this.Width = 320;

                this.comboItems = new Object[] { "..xxx..", "xxx..", "..xxx", "=", "<>", "= Ø", "<> Ø" };
                this.comboItemsWithoutNull = new Object[] { "..xxx..", "xxx..", "..xxx", "=", "<>" };
                this.comboWidth = 100;
                this.textBoxWidth = 150;
            }
            else
            {
                this.Width = 220;

                this.comboItems = new Object[] { "=", "<>", ">", "<", "<=", ">=", "= Ø", "<> Ø" };
                this.comboItemsWithoutNull = new Object[] { "=", "<>", ">", "<", "<=", ">=" };
                this.comboWidth = 70;
                this.textBoxWidth = 80;
            }

            this.comboBoxOperator.Width = this.comboWidth;
            this.comboBoxOperator.Items.AddRange(this.comboItems);
            this.comboBoxOperator.SelectedIndex = 0;

            this.textBoxValue.Width = this.textBoxWidth;
            this.textBoxValue.Location = new Point(58 + this.comboWidth, this.textBoxValue.Location.Y);

            this.FilterHost.RegisterComboBox(this.comboBoxOperator);
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
                this.FilterManager.RebuildFilter();
                return;
            }

            String resultFilterCaption = this.OriginalDataGridViewColumnHeaderText + "\n";
            String resultFilterExpression = String.Empty;

            // Managing the NULL and NOT NULL cases which are entityType-independent
            if (this.comboBoxOperator.Text == "= Ø")
                resultFilterExpression = DgvBaseColumnFilter.GetNullCondition(this.DataGridViewColumn.DataPropertyName);

            if (this.comboBoxOperator.Text == "<> Ø")
                resultFilterExpression = DgvBaseColumnFilter.GetNotNullCondition(this.DataGridViewColumn.DataPropertyName);

            if (!String.IsNullOrEmpty(resultFilterExpression))
            {
                this.FilterExpression = resultFilterExpression;
                this.FilterCaption = resultFilterCaption + "\n" + this.comboBoxOperator.Text;
                this.FilterManager.RebuildFilter();

                return;
            }

            FilterResult filterResult = new FilterResult();

            filterResult = this.ApplyFilter(filterResult,
                                            this.comboBoxOperator,
                                            this.textBoxValue.Text,
                                            null);

            foreach (FilterComponents filter in this.filters)
            {
                if (!String.IsNullOrEmpty(filter.TextBoxValue.Text))
                {
                    filterResult = this.ApplyFilter(filterResult,
                                                    filter.ComboBoxOperator,
                                                    filter.TextBoxValue.Text,
                                                    filter.ComboBoxAndOr.Text);
                }
            }

            resultFilterExpression = filterResult.Expression;
            resultFilterCaption += filterResult.Caption;

            if (!String.IsNullOrEmpty(resultFilterExpression))
            {
                this.FilterExpression = resultFilterExpression;
                this.FilterCaption = resultFilterCaption;
                this.FilterManager.RebuildFilter();
            }
        }

        /// <summary>
        /// TODO: Documentation OnFilterChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFilterChanged(object sender, EventArgs e)
        {
            if (!this.FilterApplySoon || !this.Visible)
                return;

            this.Active = true;
            this.FilterExpressionBuild();
        }
        #endregion

        #region EVENTS
        /// <summary>
        /// TODO: Documentation textBoxValue_TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*private void textBoxValue_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Int32 textBoxIdx = -1;

            foreach (FilterComponents filter in filters)
            {
                if (filter.TextBoxValue == textBox)
                {
                    textBoxIdx = filters.IndexOf(filter);
                    break;
                }
            }

            if (sender == textBoxValue)
            {
                if (!String.IsNullOrEmpty(this.textBoxValue.Text) && filters.Count == 0)
                    this.StackFilter();
            }
            else
            {
                if (textBoxIdx == lastIndex - 1 &&
                    !String.IsNullOrEmpty(textBox.Text))
                {
                    this.StackFilter();
                }

                if (textBoxIdx == lastIndex - 2 &&
                    String.IsNullOrEmpty(textBox.Text))
                {
                    this.UnstackFilter();
                }
            }
        }*/

        /// <summary>
        /// TODO: Documentation buttonStack_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStack_Click(object sender, EventArgs e)
        {
            this.StackFilter();
            this.OnFilterChanged(sender, e);

            this.buttonUnStack.Enabled = true;
        }

        /// <summary>
        /// TODO: Documentation buttonUnStack_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUnStack_Click(object sender, EventArgs e)
        {
            this.UnstackFilter();
            this.OnFilterChanged(sender, e);

            this.buttonUnStack.Enabled = this.filters.Count > 0;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// TODO: Documentation StackFilter
        /// </summary>
        private void StackFilter()
        {
            Int32 idx = filters.Count + 1;

            ComboBox comboBoxAndOr = new ComboBox();
            comboBoxAndOr.Location = new Point(3, 3 + (idx * 26));
            comboBoxAndOr.Size = new Size(40, 21);
            comboBoxAndOr.TabIndex = idx;
            comboBoxAndOr.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAndOr.FormattingEnabled = true;
            comboBoxAndOr.Items.AddRange(this.comboItemsAndOr);
            comboBoxAndOr.SelectedIndex = 0;

            ComboBox comboBox = new ComboBox();
            comboBox.Location = new Point(51, 3 + (idx * 26));
            comboBox.Size = new Size(this.comboWidth, 21);
            comboBox.TabIndex = idx;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.FormattingEnabled = true;
            comboBox.Items.AddRange(this.comboItemsWithoutNull);
            comboBox.SelectedIndex = 0;

            TextBox textBox = new TextBox();
            textBox.Location = new Point(58 + this.comboWidth, 3 + (idx * 26));
            textBox.Size = new Size(this.textBoxWidth, 20);
            textBox.TabIndex = idx;
            textBox.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Right);

            FilterComponents filter = new FilterComponents()
            {
                ComboBoxAndOr = comboBoxAndOr,
                ComboBoxOperator = comboBox,
                TextBoxValue = textBox
            };

            comboBoxAndOr.SelectedValueChanged += new EventHandler(OnFilterChanged);
            comboBox.SelectedValueChanged += new EventHandler(OnFilterChanged);
            //textBox.TextChanged += new EventHandler(this.textBoxValue_TextChanged);
            textBox.TextChanged += new EventHandler(OnFilterChanged);

            this.Controls.Add(filter.ComboBoxAndOr);
            this.Controls.Add(filter.ComboBoxOperator);
            this.Controls.Add(filter.TextBoxValue);

            this.FilterHost.RegisterComboBox(filter.ComboBoxAndOr);
            this.FilterHost.RegisterComboBox(filter.ComboBoxOperator);
            this.FilterHost.Height = this.FilterHost.Height + 26;
            this.Height = this.Height + 26;

            filters.Add(filter);
            //lastIndex++;
        }

        /// <summary>
        /// TODO: Documentation UnstackFilter
        /// </summary>
        private void UnstackFilter()
        {
            FilterComponents filter = this.filters[filters.Count - 1];

            filter.ComboBoxAndOr.SelectedValueChanged -= new EventHandler(this.OnFilterChanged);
            filter.ComboBoxOperator.SelectedValueChanged -= new EventHandler(this.OnFilterChanged);
            //filter.TextBoxValue.TextChanged -= new EventHandler(this.textBoxValue_TextChanged);
            filter.TextBoxValue.TextChanged -= new EventHandler(this.OnFilterChanged);

            this.Controls.Remove(filter.ComboBoxAndOr);
            this.Controls.Remove(filter.ComboBoxOperator);
            this.Controls.Remove(filter.TextBoxValue);

            this.FilterHost.UnRegisterComboBox(filter.ComboBoxAndOr);
            this.FilterHost.UnRegisterComboBox(filter.ComboBoxOperator);
            this.FilterHost.Height = this.FilterHost.Height - 26;
            this.Height = this.Height - 26;

            this.filters.RemoveAt(filters.Count - 1);
            //lastIndex--;
        }

        /// <summary>
        /// TODO: Documentation ApplyFilter
        /// </summary>
        /// <param name="filterResult"></param>
        /// <param name="comboBox"></param>
        /// <param name="filterValue"></param>
        /// <param name="andor"></param>
        /// <returns></returns>
        private FilterResult ApplyFilter(FilterResult filterResult, ComboBox comboBox, String filterValue, String andor)
        {
            if (!String.IsNullOrEmpty(andor))
            {
                if (andor == "AND")
                {
                    filterResult.Expression += " AND ";
                    filterResult.Caption += "\nAND ";
                }
                else
                {
                    filterResult.Expression += " OR ";
                    filterResult.Caption += "\nOR ";
                }
            }

            if (ColumnDataType == typeof(String))
            {
                // Managing the string-column case
                filterValue = StringEscape(filterValue);

                switch (comboBox.Text)
                {
                    case "..xxx..":
                        filterResult.Expression += this.DataGridViewColumn.DataPropertyName + " LIKE '%" + filterValue + "%'";
                        filterResult.Caption += "= '.." + filterValue + "..'";
                        break;

                    case "xxx..":
                        filterResult.Expression += this.DataGridViewColumn.DataPropertyName + " LIKE '" + filterValue + "%'";
                        filterResult.Caption += "= '" + filterValue + "..'";
                        break;

                    case "..xxx":
                        filterResult.Expression += this.DataGridViewColumn.DataPropertyName + " LIKE '%" + filterValue + "'";
                        filterResult.Caption += "= '.." + filterValue + "'";
                        break;

                    default:
                        filterResult.Expression += this.DataGridViewColumn.DataPropertyName + " " + comboBox.Text + "'" + filterValue + "'";
                        filterResult.Caption += comboBox.Text + " '" + filterValue + "'";
                        break;
                }
            }
            else
            {
                // Managing the numeric-column case
                String formattedValue = FormatValue(filterValue, this.ColumnDataType);

                if (!String.IsNullOrEmpty(formattedValue))
                {
                    filterResult.Expression += this.DataGridViewColumn.DataPropertyName + " " + comboBox.Text + formattedValue;
                    filterResult.Caption += comboBox.Text + " " + filterValue;
                }
            }

            return filterResult;
        }
        #endregion

        /// <summary>
        /// TODO: Documentation Class
        /// </summary>
        private class FilterComponents
        {
            public ComboBox ComboBoxAndOr { get; set; }
            public ComboBox ComboBoxOperator { get; set; }
            public TextBox TextBoxValue { get; set; }
        }
    }
}