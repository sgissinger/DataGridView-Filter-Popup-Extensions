using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DgvFilterPopup;


namespace DgvFilterPopupDemo {
    public partial class CustomizedFilterHost : DgvBaseFilterHost {
        public CustomizedFilterHost() {
            InitializeComponent();
            this.Region = BitmapToRegion((Bitmap)this.BackgroundImage,Color.FromArgb(255, 0, 0));
            this.CurrentColumnFilterChanged += new EventHandler(CloudFilterHost_CurrentColumnFilterChanged);
            lblDelete.BackColor = Color.Transparent;
            lblDeleteAll.BackColor = Color.Transparent;
            lblOK.BackColor = Color.Transparent;
        }

        void CloudFilterHost_CurrentColumnFilterChanged(object sender, EventArgs e) {
            label1.Text = CurrentColumnFilter.OriginalDataGridViewColumnHeaderText;
        }

        protected override void DoAutoFit() {
            //This empty override of the DoAutoFit force the fixed size of the host.
            //We just allow alignment
            AlignFilter();
        }

        public override Control FilterClientArea {
            get {
                return this.panel1;
            }
        }

        private void lblDelete_Click(object sender, EventArgs e) {
            FilterManager.ActivateFilter(false);
            this.Popup.Close();
        }

        private void lblDeleteAll_Click(object sender, EventArgs e) {
            FilterManager.ActivateAllFilters(false);
            this.Popup.Close();
        }

        private void lblOK_Click(object sender, EventArgs e) {
            FilterManager.ActivateFilter(true);
            this.Popup.Close();
        }
    }
}
