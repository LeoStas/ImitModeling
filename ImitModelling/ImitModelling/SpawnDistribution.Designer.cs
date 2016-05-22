namespace ImitModelling
{
	partial class SpawnDistribution
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
			if (disposing && (components != null)) {
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
			this.distribUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.distribUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// distribUpDown
			// 
			this.distribUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.distribUpDown.Location = new System.Drawing.Point(13, 13);
			this.distribUpDown.Name = "distribUpDown";
			this.distribUpDown.Size = new System.Drawing.Size(176, 20);
			this.distribUpDown.TabIndex = 0;
			this.distribUpDown.ValueChanged += new System.EventHandler(this.distribUpDown_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(195, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(23, 20);
			this.label1.TabIndex = 1;
			this.label1.Text = "%";
			// 
			// SpawnDistribution
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(239, 51);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.distribUpDown);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "SpawnDistribution";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "SpawnDistribution";
			((System.ComponentModel.ISupportInitialize)(this.distribUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown distribUpDown;
		private System.Windows.Forms.Label label1;
	}
}