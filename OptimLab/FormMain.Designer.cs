namespace OptimLab
{
    partial class FormMain
    {
        /// <summary>
        /// Необходимая переменная для проектировщика форм.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освобождение используемых ресурсов.
        /// </summary>
        /// <param name="disposing">True, если управляемые ресурсы должны быть освобождены;
        /// в противном случае False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, сгенерированный проектировщиком форм

        /// <summary>
        /// Необходимый метод для поддержки проектировщика форм -
        /// не изменяйте его содержимое с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpenResults = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSaveResults = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMethod = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemMethodOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSolve = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemResults = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuOperationProperty = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemClear = new System.Windows.Forms.ToolStripMenuItem();
            this.labelIterations = new System.Windows.Forms.Label();
            this.labelPoint = new System.Windows.Forms.Label();
            this.labelValue = new System.Windows.Forms.Label();
            this.labelErrorInPoint = new System.Windows.Forms.Label();
            this.labelErrorInValue = new System.Windows.Forms.Label();
            this.labelEvalsTarget = new System.Windows.Forms.Label();
            this.labelEvalsConst1 = new System.Windows.Forms.Label();
            this.labelEvalsConst2 = new System.Windows.Forms.Label();
            this.labelEvalsConst3 = new System.Windows.Forms.Label();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.toolStripMenuItemTask,
            this.toolStripMenuItemMethod,
            this.toolStripMenuItemSolve,
            this.toolStripMenuItemResults});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(794, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // toolStripMenuItemFile
            // 
            this.toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpenResults,
            this.toolStripMenuItemSaveResults});
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(50, 20);
            this.toolStripMenuItemFile.Text = "Архив";
            // 
            // toolStripMenuItemOpenResults
            // 
            this.toolStripMenuItemOpenResults.Name = "toolStripMenuItemOpenResults";
            this.toolStripMenuItemOpenResults.Size = new System.Drawing.Size(216, 22);
            this.toolStripMenuItemOpenResults.Text = "Открыть результаты...";
            this.toolStripMenuItemOpenResults.Click += new System.EventHandler(this.toolStripMenuItemOpenResults_Click);
            // 
            // toolStripMenuItemSaveResults
            // 
            this.toolStripMenuItemSaveResults.Enabled = false;
            this.toolStripMenuItemSaveResults.Name = "toolStripMenuItemSaveResults";
            this.toolStripMenuItemSaveResults.Size = new System.Drawing.Size(216, 22);
            this.toolStripMenuItemSaveResults.Text = "Сохранить результаты...";
            this.toolStripMenuItemSaveResults.Click += new System.EventHandler(this.toolStripMenuItemSaveResults_Click);
            // 
            // toolStripMenuItemTask
            // 
            this.toolStripMenuItemTask.Name = "toolStripMenuItemTask";
            this.toolStripMenuItemTask.Size = new System.Drawing.Size(56, 20);
            this.toolStripMenuItemTask.Text = "Задача";
            this.toolStripMenuItemTask.Click += new System.EventHandler(this.toolStripMenuItemTask_Click);
            // 
            // toolStripMenuItemMethod
            // 
            this.toolStripMenuItemMethod.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripMenuItemMethodOptions});
            this.toolStripMenuItemMethod.Name = "toolStripMenuItemMethod";
            this.toolStripMenuItemMethod.Size = new System.Drawing.Size(52, 20);
            this.toolStripMenuItemMethod.Text = "Метод";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(191, 6);
            // 
            // toolStripMenuItemMethodOptions
            // 
            this.toolStripMenuItemMethodOptions.Name = "toolStripMenuItemMethodOptions";
            this.toolStripMenuItemMethodOptions.Size = new System.Drawing.Size(194, 22);
            this.toolStripMenuItemMethodOptions.Text = "Параметры метода...";
            this.toolStripMenuItemMethodOptions.Click += new System.EventHandler(this.toolStripMenuItemMethodOptions_Click);
            // 
            // toolStripMenuItemSolve
            // 
            this.toolStripMenuItemSolve.Name = "toolStripMenuItemSolve";
            this.toolStripMenuItemSolve.Size = new System.Drawing.Size(49, 20);
            this.toolStripMenuItemSolve.Text = "Поиск";
            this.toolStripMenuItemSolve.Click += new System.EventHandler(this.toolStripMenuItemSolve_Click);
            // 
            // toolStripMenuItemResults
            // 
            this.toolStripMenuItemResults.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuOperationProperty,
            this.toolStripMenuItemClear});
            this.toolStripMenuItemResults.Name = "toolStripMenuItemResults";
            this.toolStripMenuItemResults.Size = new System.Drawing.Size(80, 20);
            this.toolStripMenuItemResults.Text = "Результаты";
            // 
            // toolStripMenuOperationProperty
            // 
            this.toolStripMenuOperationProperty.Name = "toolStripMenuOperationProperty";
            this.toolStripMenuOperationProperty.Size = new System.Drawing.Size(247, 22);
            this.toolStripMenuOperationProperty.Text = "Операционные характеристики";
            this.toolStripMenuOperationProperty.Click += new System.EventHandler(this.toolStripMenuItemOperationProperty_Click);
            // 
            // toolStripMenuItemClear
            // 
            this.toolStripMenuItemClear.Name = "toolStripMenuItemClear";
            this.toolStripMenuItemClear.Size = new System.Drawing.Size(247, 22);
            this.toolStripMenuItemClear.Text = "Очистить";
            this.toolStripMenuItemClear.Click += new System.EventHandler(this.toolStripMenuItemClear_Click);
            // 
            // labelIterations
            // 
            this.labelIterations.AutoSize = true;
            this.labelIterations.Location = new System.Drawing.Point(8, 32);
            this.labelIterations.Name = "labelIterations";
            this.labelIterations.Size = new System.Drawing.Size(72, 13);
            this.labelIterations.TabIndex = 1;
            this.labelIterations.Text = "labelIterations";
            // 
            // labelPoint
            // 
            this.labelPoint.AutoSize = true;
            this.labelPoint.Location = new System.Drawing.Point(8, 56);
            this.labelPoint.Name = "labelPoint";
            this.labelPoint.Size = new System.Drawing.Size(53, 13);
            this.labelPoint.TabIndex = 2;
            this.labelPoint.Text = "labelPoint";
            // 
            // labelValue
            // 
            this.labelValue.AutoSize = true;
            this.labelValue.Location = new System.Drawing.Point(8, 80);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(56, 13);
            this.labelValue.TabIndex = 3;
            this.labelValue.Text = "labelValue";
            // 
            // labelErrorInPoint
            // 
            this.labelErrorInPoint.AutoSize = true;
            this.labelErrorInPoint.Location = new System.Drawing.Point(8, 104);
            this.labelErrorInPoint.Name = "labelErrorInPoint";
            this.labelErrorInPoint.Size = new System.Drawing.Size(84, 13);
            this.labelErrorInPoint.TabIndex = 4;
            this.labelErrorInPoint.Text = "labelErrorInPoint";
            // 
            // labelErrorInValue
            // 
            this.labelErrorInValue.AutoSize = true;
            this.labelErrorInValue.Location = new System.Drawing.Point(8, 128);
            this.labelErrorInValue.Name = "labelErrorInValue";
            this.labelErrorInValue.Size = new System.Drawing.Size(87, 13);
            this.labelErrorInValue.TabIndex = 5;
            this.labelErrorInValue.Text = "labelErrorInValue";
            // 
            // labelEvalsTarget
            // 
            this.labelEvalsTarget.AutoSize = true;
            this.labelEvalsTarget.Location = new System.Drawing.Point(8, 152);
            this.labelEvalsTarget.Name = "labelEvalsTarget";
            this.labelEvalsTarget.Size = new System.Drawing.Size(86, 13);
            this.labelEvalsTarget.TabIndex = 6;
            this.labelEvalsTarget.Text = "labelEvalsTarget";
            // 
            // labelEvalsConst1
            // 
            this.labelEvalsConst1.AutoSize = true;
            this.labelEvalsConst1.Location = new System.Drawing.Point(8, 176);
            this.labelEvalsConst1.Name = "labelEvalsConst1";
            this.labelEvalsConst1.Size = new System.Drawing.Size(88, 13);
            this.labelEvalsConst1.TabIndex = 7;
            this.labelEvalsConst1.Text = "labelEvalsConst1";
            // 
            // labelEvalsConst2
            // 
            this.labelEvalsConst2.AutoSize = true;
            this.labelEvalsConst2.Location = new System.Drawing.Point(8, 200);
            this.labelEvalsConst2.Name = "labelEvalsConst2";
            this.labelEvalsConst2.Size = new System.Drawing.Size(88, 13);
            this.labelEvalsConst2.TabIndex = 7;
            this.labelEvalsConst2.Text = "labelEvalsConst2";
            // 
            // labelEvalsConst3
            // 
            this.labelEvalsConst3.AutoSize = true;
            this.labelEvalsConst3.Location = new System.Drawing.Point(8, 224);
            this.labelEvalsConst3.Name = "labelEvalsConst3";
            this.labelEvalsConst3.Size = new System.Drawing.Size(88, 13);
            this.labelEvalsConst3.TabIndex = 8;
            this.labelEvalsConst3.Text = "labelEvalsConst3";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 569);
            this.Controls.Add(this.labelEvalsConst3);
            this.Controls.Add(this.labelEvalsConst2);
            this.Controls.Add(this.labelEvalsConst1);
            this.Controls.Add(this.labelEvalsTarget);
            this.Controls.Add(this.labelErrorInValue);
            this.Controls.Add(this.labelErrorInPoint);
            this.Controls.Add(this.labelValue);
            this.Controls.Add(this.labelPoint);
            this.Controls.Add(this.labelIterations);
            this.Controls.Add(this.menuStripMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FormMain";
            this.Text = "OptimLab";
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTask;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMethod;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSolve;
        private System.Windows.Forms.Label labelIterations;
        private System.Windows.Forms.Label labelPoint;
        private System.Windows.Forms.Label labelValue;
        private System.Windows.Forms.Label labelErrorInPoint;
        private System.Windows.Forms.Label labelErrorInValue;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenResults;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSaveResults;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMethodOptions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemResults;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuOperationProperty;
        private System.Windows.Forms.Label labelEvalsTarget;
        private System.Windows.Forms.Label labelEvalsConst1;
        private System.Windows.Forms.Label labelEvalsConst2;
        private System.Windows.Forms.Label labelEvalsConst3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemClear;
    }
}