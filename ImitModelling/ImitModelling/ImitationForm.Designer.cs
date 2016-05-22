namespace ImitModelling
{
    partial class ImitationForm
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
			this.components = new System.ComponentModel.Container();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.LoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FinishEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.масштабToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.увеличитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.уменьшитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.WallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SpawnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.StartDemoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.incSize = new System.Windows.Forms.Button();
			this.decSize = new System.Windows.Forms.Button();
			this.timerMove = new System.Windows.Forms.Timer(this.components);
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.panel1 = new System.Windows.Forms.Panel();
			this.totalAgentsUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.totalAgentsUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox
			// 
			this.pictureBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(476, 398);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
			this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
			this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.масштабToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.StartDemoToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(501, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// файлToolStripMenuItem
			// 
			this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.LoadToolStripMenuItem,
            this.FinishEditToolStripMenuItem});
			this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
			this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.файлToolStripMenuItem.Text = "Файл";
			// 
			// createToolStripMenuItem
			// 
			this.createToolStripMenuItem.Name = "createToolStripMenuItem";
			this.createToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
			this.createToolStripMenuItem.Text = "Создать";
			this.createToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
			this.saveToolStripMenuItem.Text = "Сохранить";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// LoadToolStripMenuItem
			// 
			this.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem";
			this.LoadToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
			this.LoadToolStripMenuItem.Text = "Загрузить";
			this.LoadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItem_Click);
			// 
			// FinishEditToolStripMenuItem
			// 
			this.FinishEditToolStripMenuItem.Name = "FinishEditToolStripMenuItem";
			this.FinishEditToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
			this.FinishEditToolStripMenuItem.Text = "Закончить редактирование";
			this.FinishEditToolStripMenuItem.Click += new System.EventHandler(this.FinishEditToolStripMenuItem_Click);
			// 
			// масштабToolStripMenuItem
			// 
			this.масштабToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.увеличитьToolStripMenuItem,
            this.уменьшитьToolStripMenuItem});
			this.масштабToolStripMenuItem.Name = "масштабToolStripMenuItem";
			this.масштабToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
			this.масштабToolStripMenuItem.Text = "Масштаб";
			// 
			// увеличитьToolStripMenuItem
			// 
			this.увеличитьToolStripMenuItem.Name = "увеличитьToolStripMenuItem";
			this.увеличитьToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.увеличитьToolStripMenuItem.Text = "Увеличить (+)";
			this.увеличитьToolStripMenuItem.Click += new System.EventHandler(this.incSize_Click);
			// 
			// уменьшитьToolStripMenuItem
			// 
			this.уменьшитьToolStripMenuItem.Name = "уменьшитьToolStripMenuItem";
			this.уменьшитьToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.уменьшитьToolStripMenuItem.Text = "Уменьшить (–)";
			this.уменьшитьToolStripMenuItem.Click += new System.EventHandler(this.decSize_Click);
			// 
			// EditToolStripMenuItem
			// 
			this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WallToolStripMenuItem,
            this.SpawnToolStripMenuItem,
            this.ExitToolStripMenuItem});
			this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
			this.EditToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.EditToolStripMenuItem.Text = "Правка";
			// 
			// WallToolStripMenuItem
			// 
			this.WallToolStripMenuItem.Enabled = false;
			this.WallToolStripMenuItem.Name = "WallToolStripMenuItem";
			this.WallToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
			this.WallToolStripMenuItem.Text = "Стена";
			this.WallToolStripMenuItem.Click += new System.EventHandler(this.WallToolStripMenuItem_Click);
			// 
			// SpawnToolStripMenuItem
			// 
			this.SpawnToolStripMenuItem.Enabled = false;
			this.SpawnToolStripMenuItem.Name = "SpawnToolStripMenuItem";
			this.SpawnToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
			this.SpawnToolStripMenuItem.Text = "Спаун";
			this.SpawnToolStripMenuItem.Click += new System.EventHandler(this.SpawnToolStripMenuItem_Click);
			// 
			// ExitToolStripMenuItem
			// 
			this.ExitToolStripMenuItem.Enabled = false;
			this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
			this.ExitToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
			this.ExitToolStripMenuItem.Text = "Выход";
			this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
			// 
			// StartDemoToolStripMenuItem
			// 
			this.StartDemoToolStripMenuItem.Name = "StartDemoToolStripMenuItem";
			this.StartDemoToolStripMenuItem.Size = new System.Drawing.Size(160, 20);
			this.StartDemoToolStripMenuItem.Text = "Запустить демонстрацию";
			this.StartDemoToolStripMenuItem.Click += new System.EventHandler(this.StartDemoToolStripMenuItem_Click);
			// 
			// incSize
			// 
			this.incSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.incSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.incSize.Location = new System.Drawing.Point(434, 432);
			this.incSize.Name = "incSize";
			this.incSize.Size = new System.Drawing.Size(23, 23);
			this.incSize.TabIndex = 2;
			this.incSize.Text = "+";
			this.incSize.UseVisualStyleBackColor = true;
			this.incSize.Click += new System.EventHandler(this.incSize_Click);
			// 
			// decSize
			// 
			this.decSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.decSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.decSize.Location = new System.Drawing.Point(463, 432);
			this.decSize.Name = "decSize";
			this.decSize.Size = new System.Drawing.Size(25, 23);
			this.decSize.TabIndex = 3;
			this.decSize.Text = "-";
			this.decSize.UseVisualStyleBackColor = true;
			this.decSize.Click += new System.EventHandler(this.decSize_Click);
			// 
			// timerMove
			// 
			this.timerMove.Interval = 500;
			this.timerMove.Tick += new System.EventHandler(this.timerMove_Tick);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.pictureBox);
			this.panel1.Location = new System.Drawing.Point(13, 28);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(476, 398);
			this.panel1.TabIndex = 7;
			// 
			// totalAgentsUpDown
			// 
			this.totalAgentsUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.totalAgentsUpDown.Enabled = false;
			this.totalAgentsUpDown.Location = new System.Drawing.Point(165, 432);
			this.totalAgentsUpDown.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
			this.totalAgentsUpDown.Name = "totalAgentsUpDown";
			this.totalAgentsUpDown.Size = new System.Drawing.Size(84, 20);
			this.totalAgentsUpDown.TabIndex = 8;
			this.totalAgentsUpDown.ValueChanged += new System.EventHandler(this.totalAgentsUpDown_ValueChanged);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 434);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(149, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Общее количество агентов:";
			// 
			// ImitationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(501, 467);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.totalAgentsUpDown);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.decSize);
			this.Controls.Add(this.incSize);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "ImitationForm";
			this.Text = "Imitation";
			this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.totalAgentsUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadToolStripMenuItem;
		private System.Windows.Forms.Button incSize;
		private System.Windows.Forms.Button decSize;
		private System.Windows.Forms.ToolStripMenuItem масштабToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem увеличитьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem уменьшитьToolStripMenuItem;
		private System.Windows.Forms.Timer timerMove;
		private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem WallToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SpawnToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FinishEditToolStripMenuItem;
		private System.Windows.Forms.NumericUpDown totalAgentsUpDown;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolStripMenuItem StartDemoToolStripMenuItem;
	}
}

