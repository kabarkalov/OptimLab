namespace OptimLab
{
    partial class FormOpenResults
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.groupBoxMode = new System.Windows.Forms.GroupBox();
            this.textBoxPartSize = new System.Windows.Forms.TextBox();
            this.labelPartSize = new System.Windows.Forms.Label();
            this.radioButtonPartial = new System.Windows.Forms.RadioButton();
            this.radioButtonAll = new System.Windows.Forms.RadioButton();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(16, 16);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.ReadOnly = true;
            this.textBoxFileName.Size = new System.Drawing.Size(152, 20);
            this.textBoxFileName.TabIndex = 0;
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Location = new System.Drawing.Point(176, 16);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(99, 23);
            this.buttonOpenFile.TabIndex = 1;
            this.buttonOpenFile.Text = "Выбрать файл...";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // groupBoxMode
            // 
            this.groupBoxMode.Controls.Add(this.textBoxPartSize);
            this.groupBoxMode.Controls.Add(this.labelPartSize);
            this.groupBoxMode.Controls.Add(this.radioButtonPartial);
            this.groupBoxMode.Controls.Add(this.radioButtonAll);
            this.groupBoxMode.Location = new System.Drawing.Point(16, 56);
            this.groupBoxMode.Name = "groupBoxMode";
            this.groupBoxMode.Size = new System.Drawing.Size(264, 112);
            this.groupBoxMode.TabIndex = 2;
            this.groupBoxMode.TabStop = false;
            this.groupBoxMode.Text = "Режим показа";
            // 
            // textBoxPartSize
            // 
            this.textBoxPartSize.Enabled = false;
            this.textBoxPartSize.Location = new System.Drawing.Point(104, 80);
            this.textBoxPartSize.Name = "textBoxPartSize";
            this.textBoxPartSize.Size = new System.Drawing.Size(100, 20);
            this.textBoxPartSize.TabIndex = 3;
            this.textBoxPartSize.Text = "10";
            // 
            // labelPartSize
            // 
            this.labelPartSize.AutoSize = true;
            this.labelPartSize.Location = new System.Drawing.Point(16, 80);
            this.labelPartSize.Name = "labelPartSize";
            this.labelPartSize.Size = new System.Drawing.Size(88, 13);
            this.labelPartSize.TabIndex = 2;
            this.labelPartSize.Text = "Размер порции:";
            // 
            // radioButtonPartial
            // 
            this.radioButtonPartial.AutoSize = true;
            this.radioButtonPartial.Location = new System.Drawing.Point(16, 48);
            this.radioButtonPartial.Name = "radioButtonPartial";
            this.radioButtonPartial.Size = new System.Drawing.Size(78, 17);
            this.radioButtonPartial.TabIndex = 1;
            this.radioButtonPartial.Text = "По частям";
            this.radioButtonPartial.UseVisualStyleBackColor = true;
            this.radioButtonPartial.CheckedChanged += new System.EventHandler(this.radioButtonPartial_CheckedChanged);
            // 
            // radioButtonAll
            // 
            this.radioButtonAll.AutoSize = true;
            this.radioButtonAll.Checked = true;
            this.radioButtonAll.Location = new System.Drawing.Point(16, 24);
            this.radioButtonAll.Name = "radioButtonAll";
            this.radioButtonAll.Size = new System.Drawing.Size(75, 17);
            this.radioButtonAll.TabIndex = 0;
            this.radioButtonAll.TabStop = true;
            this.radioButtonAll.Text = "Все точки";
            this.radioButtonAll.UseVisualStyleBackColor = true;
            this.radioButtonAll.CheckedChanged += new System.EventHandler(this.radioButtonAll_CheckedChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(48, 184);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(168, 184);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // FormOpenResults
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(294, 219);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxMode);
            this.Controls.Add(this.buttonOpenFile);
            this.Controls.Add(this.textBoxFileName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOpenResults";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Открыть результаты";
            this.groupBoxMode.ResumeLayout(false);
            this.groupBoxMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.GroupBox groupBoxMode;
        private System.Windows.Forms.TextBox textBoxPartSize;
        private System.Windows.Forms.Label labelPartSize;
        private System.Windows.Forms.RadioButton radioButtonPartial;
        private System.Windows.Forms.RadioButton radioButtonAll;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}