﻿
namespace CSL1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.окноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewtoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.SavetoolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.параметрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LineColToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BackColToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BrsizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.размерРисункаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.окноToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.параметрыToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.windowToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1005, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // окноToolStripMenuItem
            // 
            this.окноToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewtoolStripMenuItem1,
            this.SavetoolStripMenuItem2,
            this.SaveAsStripMenuItem3,
            this.OpenStripMenuItem});
            this.окноToolStripMenuItem.Name = "окноToolStripMenuItem";
            this.окноToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.окноToolStripMenuItem.Text = "Файл";
            this.окноToolStripMenuItem.DropDownOpened += new System.EventHandler(this.окноToolStripMenuItem_DropDownOpened);
            // 
            // NewtoolStripMenuItem1
            // 
            this.NewtoolStripMenuItem1.Name = "NewtoolStripMenuItem1";
            this.NewtoolStripMenuItem1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.NewtoolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.NewtoolStripMenuItem1.Text = "Новый";
            this.NewtoolStripMenuItem1.Click += new System.EventHandler(this.NewtoolStripMenuItem1_Click);
            // 
            // SavetoolStripMenuItem2
            // 
            this.SavetoolStripMenuItem2.Name = "SavetoolStripMenuItem2";
            this.SavetoolStripMenuItem2.Size = new System.Drawing.Size(154, 22);
            this.SavetoolStripMenuItem2.Text = "Сохранить";
            this.SavetoolStripMenuItem2.Click += new System.EventHandler(this.SavetoolStripMenuItem2_Click);
            // 
            // SaveAsStripMenuItem3
            // 
            this.SaveAsStripMenuItem3.Name = "SaveAsStripMenuItem3";
            this.SaveAsStripMenuItem3.Size = new System.Drawing.Size(154, 22);
            this.SaveAsStripMenuItem3.Text = "Сохранить как";
            this.SaveAsStripMenuItem3.Click += new System.EventHandler(this.SaveAsStripMenuItem3_Click);
            // 
            // OpenStripMenuItem
            // 
            this.OpenStripMenuItem.Name = "OpenStripMenuItem";
            this.OpenStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.OpenStripMenuItem.Text = "Открыть";
            this.OpenStripMenuItem.Click += new System.EventHandler(this.OpenStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.BackColor = System.Drawing.SystemColors.Window;
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.windowToolStripMenuItem.Text = "Окно";
            this.windowToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // параметрыToolStripMenuItem
            // 
            this.параметрыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LineColToolStripMenuItem,
            this.BackColToolStripMenuItem,
            this.BrsizeToolStripMenuItem,
            this.размерРисункаToolStripMenuItem});
            this.параметрыToolStripMenuItem.Name = "параметрыToolStripMenuItem";
            this.параметрыToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.параметрыToolStripMenuItem.Text = "Параметры";
            // 
            // LineColToolStripMenuItem
            // 
            this.LineColToolStripMenuItem.Name = "LineColToolStripMenuItem";
            this.LineColToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.LineColToolStripMenuItem.Text = "Цвет линии";
            this.LineColToolStripMenuItem.Click += new System.EventHandler(this.LineColToolStripMenuItem_Click);
            // 
            // BackColToolStripMenuItem
            // 
            this.BackColToolStripMenuItem.Name = "BackColToolStripMenuItem";
            this.BackColToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.BackColToolStripMenuItem.Text = "Цвет фона";
            this.BackColToolStripMenuItem.Click += new System.EventHandler(this.BackColToolStripMenuItem_Click);
            // 
            // BrsizeToolStripMenuItem
            // 
            this.BrsizeToolStripMenuItem.Name = "BrsizeToolStripMenuItem";
            this.BrsizeToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.BrsizeToolStripMenuItem.Text = "Толщина линии";
            this.BrsizeToolStripMenuItem.Click += new System.EventHandler(this.BrsizeToolStripMenuItem_Click);
            // 
            // размерРисункаToolStripMenuItem
            // 
            this.размерРисункаToolStripMenuItem.Name = "размерРисункаToolStripMenuItem";
            this.размерРисункаToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.размерРисункаToolStripMenuItem.Text = "Размер рисунка";
            this.размерРисункаToolStripMenuItem.Click += new System.EventHandler(this.размерРисункаToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1005, 721);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem окноToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem параметрыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LineColToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BackColToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BrsizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewtoolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem SavetoolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem SaveAsStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem OpenStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem размерРисункаToolStripMenuItem;
    }
}

