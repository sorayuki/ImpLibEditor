namespace ImpLibEditor
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.colDllName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSymName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNameType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHint = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colImportName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reverseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeToKeepTheSameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeToNoPrefixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeToUnderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDllName,
            this.colSymName,
            this.colNameType,
            this.colHint,
            this.colImportName});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(12, 28);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(699, 372);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // colDllName
            // 
            this.colDllName.Text = "Dll Name";
            this.colDllName.Width = 79;
            // 
            // colSymName
            // 
            this.colSymName.Text = "Link Function";
            this.colSymName.Width = 211;
            // 
            // colNameType
            // 
            this.colNameType.Text = "Name Type";
            this.colNameType.Width = 100;
            // 
            // colHint
            // 
            this.colHint.Text = "Hint/Ordinal";
            this.colHint.Width = 91;
            // 
            // colImportName
            // 
            this.colImportName.Text = "Import Name";
            this.colImportName.Width = 212;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.selectionToolStripMenuItem,
            this.editToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(723, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // selectionToolStripMenuItem
            // 
            this.selectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.noneToolStripMenuItem,
            this.reverseToolStripMenuItem});
            this.selectionToolStripMenuItem.Name = "selectionToolStripMenuItem";
            this.selectionToolStripMenuItem.Size = new System.Drawing.Size(72, 21);
            this.selectionToolStripMenuItem.Text = "Selection";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.allToolStripMenuItem.Text = "All";
            this.allToolStripMenuItem.Click += new System.EventHandler(this.allToolStripMenuItem_Click);
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.noneToolStripMenuItem.Text = "None";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // reverseToolStripMenuItem
            // 
            this.reverseToolStripMenuItem.Name = "reverseToolStripMenuItem";
            this.reverseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.reverseToolStripMenuItem.Text = "Reverse";
            this.reverseToolStripMenuItem.Click += new System.EventHandler(this.reverseToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeToKeepTheSameToolStripMenuItem,
            this.changeToNoPrefixToolStripMenuItem,
            this.changeToUnderToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(131, 21);
            this.editToolStripMenuItem.Text = "Import Name Type";
            // 
            // changeToKeepTheSameToolStripMenuItem
            // 
            this.changeToKeepTheSameToolStripMenuItem.Name = "changeToKeepTheSameToolStripMenuItem";
            this.changeToKeepTheSameToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.changeToKeepTheSameToolStripMenuItem.Text = "Change To [Keep The Same]";
            this.changeToKeepTheSameToolStripMenuItem.Click += new System.EventHandler(this.changeToKeepTheSameToolStripMenuItem_Click);
            // 
            // changeToNoPrefixToolStripMenuItem
            // 
            this.changeToNoPrefixToolStripMenuItem.Name = "changeToNoPrefixToolStripMenuItem";
            this.changeToNoPrefixToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.changeToNoPrefixToolStripMenuItem.Text = "Change To [No Prefix]";
            this.changeToNoPrefixToolStripMenuItem.Click += new System.EventHandler(this.changeToNoPrefixToolStripMenuItem_Click);
            // 
            // changeToUnderToolStripMenuItem
            // 
            this.changeToUnderToolStripMenuItem.Name = "changeToUnderToolStripMenuItem";
            this.changeToUnderToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.changeToUnderToolStripMenuItem.Text = "Change To [Undecorate]";
            this.changeToUnderToolStripMenuItem.Click += new System.EventHandler(this.changeToUnderToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(55, 21);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 412);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Import Library Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader colDllName;
        private System.Windows.Forms.ColumnHeader colSymName;
        private System.Windows.Forms.ColumnHeader colNameType;
        private System.Windows.Forms.ColumnHeader colHint;
        private System.Windows.Forms.ColumnHeader colImportName;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reverseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeToKeepTheSameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeToNoPrefixToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeToUnderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

