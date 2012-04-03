using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DgvFilterPopup;

namespace DgvFilterPopupDemo {
    public partial class Sample1 : DgvFilterPopupDemo.Sample0 {
        public Sample1() {
            InitializeComponent();
        }

        private void Sample1_Load(object sender, EventArgs e) {
            InitGrid();
            new DgvFilterManager(dataGridView1);
        }
    }
}

