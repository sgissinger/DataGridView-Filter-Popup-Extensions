using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DgvFilterPopup;

namespace DgvFilterPopupDemo
{
    public partial class Sample3 : DgvFilterPopupDemo.Sample0
    {
        public Sample3()
        {
            InitializeComponent();
        }

        private void Sample3_Load(object sender, EventArgs e)
        {
            InitGrid();
            DgvFilterManager fm = new DgvFilterManager();

            // Using the ColumnFilterAdding event, you may force your preferred filter,
            // BEFORE the FilterManager create the predefined filter. This event is 
            // raised for each column in the grid when you set the DataGridView property 
            // of the FilterManager.
            fm.ColumnFilterAdding += new ColumnFilterEventHandler(fm_ColumnFilterAdding);

            fm.DataGridView = dataGridView1; // this raises ColumnFilterAdding events

            // After column filters creation however, you can overwrite the created filter.
            fm["CustomerID"] = new DgvComboBoxColumnFilter();

        }

        void fm_ColumnFilterAdding(object sender, ColumnFilterEventArgs e)
        {
            switch (e.Column.Name)
            {
                case "ShipName":
                case "ShipAdress":
                    e.ColumnFilter = new DgvMultiTextBoxColumnFilter();
                    break;

                case "ShipVia":
                case "OrderDate":
                case "Freight":
                    e.ColumnFilter = new DgvMultiCheckBoxColumnFilter();
                    break;
            }
        }

    }
}

