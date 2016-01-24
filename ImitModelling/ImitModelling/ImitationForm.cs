using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace ImitModelling
{
	public partial class ImitationForm : Form
    {
        private String fileName;
        private Grid grid;
        public ImitationForm()
        {
            grid = new Grid();
            InitializeComponent();
			this.pictureBox1.Hide();
		}

		private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
			this.pictureBox1.Show();
			fileName = ofd.FileName;
            grid.LoadRoom(fileName);
            this.pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            grid.generateAgents();
            grid.Draw(this.pictureBox1.Width, this.pictureBox1.Height, e.Graphics);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            this.pictureBox1.Invalidate();
        }

		private void incSize_Click(object sender, EventArgs e)
		{
			if (grid == null) return;
			++Cell.r;
			this.pictureBox1.Invalidate();
		}

		private void decSize_Click(object sender, EventArgs e)
		{
			if (grid == null) return;
			if (Cell.r > 1) {
				--Cell.r;
				this.pictureBox1.Invalidate();
			}
		}

		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{
			Cell chosen = null;
			if (grid != null && (chosen = grid.findCellPictureCoords(new Point(e.X, e.Y))) != null) {
				List<Cell> path = grid.AStarPath(chosen);
				if (path == null) return;
				foreach (Cell cur in path) {
					grid.setCell(cur.X, cur.Y, new DebugCell(cur.X, cur.Y));
				}
			}
			this.pictureBox1.Invalidate();
		}
	}    
}
