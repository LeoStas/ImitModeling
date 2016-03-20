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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.LoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.масштабToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.увеличитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.уменьшитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.incSize = new System.Windows.Forms.Button();
			this.decSize = new System.Windows.Forms.Button();
			this.timerMove = new System.Windows.Forms.Timer(this.components);
			this.wallRadio = new System.Windows.Forms.RadioButton();
			this.spawnRadio = new System.Windows.Forms.RadioButton();
			this.exitRadio = new System.Windows.Forms.RadioButton();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(476, 304);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
			this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
			this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.масштабToolStripMenuItem});
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
            this.LoadToolStripMenuItem});
			this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
			this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.файлToolStripMenuItem.Text = "Файл";
			// 
			// createToolStripMenuItem
			// 
			this.createToolStripMenuItem.Name = "createToolStripMenuItem";
			this.createToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
			this.createToolStripMenuItem.Text = "Создать";
			this.createToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
			this.saveToolStripMenuItem.Text = "Сохранить";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// LoadToolStripMenuItem
			// 
			this.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem";
			this.LoadToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
			this.LoadToolStripMenuItem.Text = "Загрузить";
			this.LoadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItem_Click);
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
			// incSize
			// 
			this.incSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.incSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.incSize.Location = new System.Drawing.Point(12, 338);
			this.incSize.Name = "incSize";
			this.incSize.Size = new System.Drawing.Size(23, 23);
			this.incSize.TabIndex = 2;
			this.incSize.Text = "+";
			this.incSize.UseVisualStyleBackColor = true;
			this.incSize.Click += new System.EventHandler(this.incSize_Click);
			// 
			// decSize
			// 
			this.decSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.decSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.decSize.Location = new System.Drawing.Point(41, 338);
			this.decSize.Name = "decSize";
			this.decSize.Size = new System.Drawing.Size(25, 23);
			this.decSize.TabIndex = 3;
			this.decSize.Text = "-";
			this.decSize.UseVisualStyleBackColor = true;
			this.decSize.Click += new System.EventHandler(this.decSize_Click);
			// 
			// timerMove
			// 
			this.timerMove.Interval = 5;
			this.timerMove.Tick += new System.EventHandler(this.timerMove_Tick);
			// 
			// wallRadio
			// 
			this.wallRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.wallRadio.AutoSize = true;
			this.wallRadio.Location = new System.Drawing.Point(96, 341);
			this.wallRadio.Name = "wallRadio";
			this.wallRadio.Size = new System.Drawing.Size(55, 17);
			this.wallRadio.TabIndex = 4;
			this.wallRadio.Text = "Стена";
			this.wallRadio.UseVisualStyleBackColor = true;
			this.wallRadio.CheckedChanged += new System.EventHandler(this.wallRadio_CheckedChanged);
			// 
			// spawnRadio
			// 
			this.spawnRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.spawnRadio.AutoSize = true;
			this.spawnRadio.Location = new System.Drawing.Point(96, 365);
			this.spawnRadio.Name = "spawnRadio";
			this.spawnRadio.Size = new System.Drawing.Size(55, 17);
			this.spawnRadio.TabIndex = 5;
			this.spawnRadio.TabStop = true;
			this.spawnRadio.Text = "Спаун";
			this.spawnRadio.UseVisualStyleBackColor = true;
			this.spawnRadio.CheckedChanged += new System.EventHandler(this.spawnRadio_CheckedChanged);
			// 
			// exitRadio
			// 
			this.exitRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.exitRadio.AutoSize = true;
			this.exitRadio.Location = new System.Drawing.Point(96, 389);
			this.exitRadio.Name = "exitRadio";
			this.exitRadio.Size = new System.Drawing.Size(57, 17);
			this.exitRadio.TabIndex = 6;
			this.exitRadio.TabStop = true;
			this.exitRadio.Text = "Выход";
			this.exitRadio.UseVisualStyleBackColor = true;
			this.exitRadio.CheckedChanged += new System.EventHandler(this.exitRadio_CheckedChanged);
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
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Location = new System.Drawing.Point(13, 28);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(476, 304);
			this.panel1.TabIndex = 7;
			// 
			// ImitationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(501, 467);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.exitRadio);
			this.Controls.Add(this.spawnRadio);
			this.Controls.Add(this.wallRadio);
			this.Controls.Add(this.decSize);
			this.Controls.Add(this.incSize);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "ImitationForm";
			this.Text = "Imitation";
			this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
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
		private System.Windows.Forms.RadioButton wallRadio;
		private System.Windows.Forms.RadioButton spawnRadio;
		private System.Windows.Forms.RadioButton exitRadio;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Panel panel1;
	}
}

