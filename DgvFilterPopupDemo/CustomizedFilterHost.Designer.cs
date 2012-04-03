namespace DgvFilterPopupDemo {
    partial class CustomizedFilterHost {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDelete = new System.Windows.Forms.Label();
            this.lblDeleteAll = new System.Windows.Forms.Label();
            this.lblOK = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(14, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(343, 39);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(133, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblDelete
            // 
            this.lblDelete.BackColor = System.Drawing.SystemColors.Control;
            this.lblDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDelete.Location = new System.Drawing.Point(45, 51);
            this.lblDelete.Name = "lblDelete";
            this.lblDelete.Size = new System.Drawing.Size(37, 39);
            this.lblDelete.TabIndex = 2;
            this.lblDelete.Click += new System.EventHandler(this.lblDelete_Click);
            // 
            // lblDeleteAll
            // 
            this.lblDeleteAll.BackColor = System.Drawing.SystemColors.Control;
            this.lblDeleteAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDeleteAll.Location = new System.Drawing.Point(169, 56);
            this.lblDeleteAll.Name = "lblDeleteAll";
            this.lblDeleteAll.Size = new System.Drawing.Size(37, 39);
            this.lblDeleteAll.TabIndex = 3;
            this.lblDeleteAll.Click += new System.EventHandler(this.lblDeleteAll_Click);
            // 
            // lblOK
            // 
            this.lblOK.BackColor = System.Drawing.SystemColors.Control;
            this.lblOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblOK.Location = new System.Drawing.Point(297, 54);
            this.lblOK.Name = "lblOK";
            this.lblOK.Size = new System.Drawing.Size(37, 39);
            this.lblOK.TabIndex = 4;
            this.lblOK.Click += new System.EventHandler(this.lblOK_Click);
            // 
            // CloudFilterHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DgvFilterPopupDemo.Properties.Resources.Studio;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.lblOK);
            this.Controls.Add(this.lblDeleteAll);
            this.Controls.Add(this.lblDelete);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "CloudFilterHost";
            this.Size = new System.Drawing.Size(379, 167);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDelete;
        private System.Windows.Forms.Label lblDeleteAll;
        private System.Windows.Forms.Label lblOK;
    }
}
