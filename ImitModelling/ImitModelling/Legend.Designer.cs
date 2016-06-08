namespace ImitModelling
{
	partial class Legend
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
			this.SpawnLabel = new System.Windows.Forms.Label();
			this.ExitLabel = new System.Windows.Forms.Label();
			this.AgentLabel = new System.Windows.Forms.Label();
			this.WallLabel = new System.Windows.Forms.Label();
			this.CheckpointLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// SpawnLabel
			// 
			this.SpawnLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SpawnLabel.AutoSize = true;
			this.SpawnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.SpawnLabel.ForeColor = System.Drawing.Color.Blue;
			this.SpawnLabel.Location = new System.Drawing.Point(29, 9);
			this.SpawnLabel.Name = "SpawnLabel";
			this.SpawnLabel.Size = new System.Drawing.Size(59, 20);
			this.SpawnLabel.TabIndex = 0;
			this.SpawnLabel.Text = "Спаун";
			// 
			// ExitLabel
			// 
			this.ExitLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ExitLabel.AutoSize = true;
			this.ExitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ExitLabel.ForeColor = System.Drawing.Color.Orange;
			this.ExitLabel.Location = new System.Drawing.Point(29, 29);
			this.ExitLabel.Name = "ExitLabel";
			this.ExitLabel.Size = new System.Drawing.Size(63, 20);
			this.ExitLabel.TabIndex = 1;
			this.ExitLabel.Text = "Выход";
			// 
			// AgentLabel
			// 
			this.AgentLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.AgentLabel.AutoSize = true;
			this.AgentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.AgentLabel.ForeColor = System.Drawing.Color.Green;
			this.AgentLabel.Location = new System.Drawing.Point(29, 89);
			this.AgentLabel.Name = "AgentLabel";
			this.AgentLabel.Size = new System.Drawing.Size(59, 20);
			this.AgentLabel.TabIndex = 2;
			this.AgentLabel.Text = "Агент";
			// 
			// WallLabel
			// 
			this.WallLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.WallLabel.AutoSize = true;
			this.WallLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.WallLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.WallLabel.Location = new System.Drawing.Point(29, 69);
			this.WallLabel.Name = "WallLabel";
			this.WallLabel.Size = new System.Drawing.Size(61, 20);
			this.WallLabel.TabIndex = 3;
			this.WallLabel.Text = "Стена";
			// 
			// CheckpointLabel
			// 
			this.CheckpointLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CheckpointLabel.AutoSize = true;
			this.CheckpointLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.CheckpointLabel.ForeColor = System.Drawing.Color.Red;
			this.CheckpointLabel.Location = new System.Drawing.Point(12, 49);
			this.CheckpointLabel.Name = "CheckpointLabel";
			this.CheckpointLabel.Size = new System.Drawing.Size(91, 20);
			this.CheckpointLabel.TabIndex = 4;
			this.CheckpointLabel.Text = "Чекпойнт";
			// 
			// Legend
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(120, 128);
			this.Controls.Add(this.CheckpointLabel);
			this.Controls.Add(this.WallLabel);
			this.Controls.Add(this.AgentLabel);
			this.Controls.Add(this.ExitLabel);
			this.Controls.Add(this.SpawnLabel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Legend";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Легенда";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label SpawnLabel;
		private System.Windows.Forms.Label ExitLabel;
		private System.Windows.Forms.Label AgentLabel;
		private System.Windows.Forms.Label WallLabel;
		private System.Windows.Forms.Label CheckpointLabel;
	}
}