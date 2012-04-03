namespace DgvFilterPopup
{
    partial class DgvMultiTextBoxColumnFilter
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DgvMultiTextBoxColumnFilter));
            this.comboBoxOperator = new System.Windows.Forms.ComboBox();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.buttonStack = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.buttonUnStack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxOperator
            // 
            this.comboBoxOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOperator.FormattingEnabled = true;
            this.comboBoxOperator.Location = new System.Drawing.Point(71, 3);
            this.comboBoxOperator.Name = "comboBoxOperator";
            this.comboBoxOperator.Size = new System.Drawing.Size(80, 21);
            this.comboBoxOperator.TabIndex = 0;
            // 
            // textBoxValue
            // 
            this.textBoxValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxValue.Location = new System.Drawing.Point(158, 3);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(158, 20);
            this.textBoxValue.TabIndex = 0;
            // 
            // buttonStack
            // 
            this.buttonStack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStack.ImageKey = "Stack_16.png";
            this.buttonStack.ImageList = this.imageList;
            this.buttonStack.Location = new System.Drawing.Point(255, 29);
            this.buttonStack.Name = "buttonStack";
            this.buttonStack.Size = new System.Drawing.Size(24, 24);
            this.buttonStack.TabIndex = 1000;
            this.buttonStack.UseVisualStyleBackColor = true;
            this.buttonStack.Click += new System.EventHandler(this.buttonStack_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Stack_16.png");
            this.imageList.Images.SetKeyName(1, "UnStack_16.png");
            // 
            // buttonUnStack
            // 
            this.buttonUnStack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUnStack.Enabled = false;
            this.buttonUnStack.ImageKey = "UnStack_16.png";
            this.buttonUnStack.ImageList = this.imageList;
            this.buttonUnStack.Location = new System.Drawing.Point(285, 29);
            this.buttonUnStack.Name = "buttonUnStack";
            this.buttonUnStack.Size = new System.Drawing.Size(24, 24);
            this.buttonUnStack.TabIndex = 1000;
            this.buttonUnStack.UseVisualStyleBackColor = true;
            this.buttonUnStack.Click += new System.EventHandler(this.buttonUnStack_Click);
            // 
            // DgvMultiTextBoxColumnFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.comboBoxOperator);
            this.Controls.Add(this.textBoxValue);
            this.Controls.Add(this.buttonStack);
            this.Controls.Add(this.buttonUnStack);
            this.Name = "DgvMultiTextBoxColumnFilter";
            this.Size = new System.Drawing.Size(320, 57);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxOperator;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Button buttonStack;
        private System.Windows.Forms.Button buttonUnStack;
        private System.Windows.Forms.ImageList imageList;
    }
}
