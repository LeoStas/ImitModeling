using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ImitModelling
{
	[Serializable]
	public partial class ImitationForm : Form
    {
		[Serializable]
		class Project
		{
			public Grid grid;
		}
		static int NUM;
		private CellFactory factory;
		private bool isDown;
		private Project prj;
		static ImitationForm()
		{
			NUM = 6;
		}

        public ImitationForm()
        {
			prj = new Project();
            prj.grid = new Grid();
			isDown = false;
			factory = new EmptyFactory();
            InitializeComponent();
			this.pictureBox1.Hide();
		}

		public void SaveState(string filename)
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
			bf.Serialize(fs, prj);
			bf.Serialize(fs, Cell.r);
			fs.Close();
		}

		public void LoadState(string filename)
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
			prj = (Project)bf.Deserialize(fs);
			Cell.r = (int)bf.Deserialize(fs);
			fs.Close();
		}

		private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK) {
				LoadState(this.openFileDialog1.FileName);
				this.pictureBox1.Show();
				this.pictureBox1.Invalidate();
			}
			/*
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
			string fileName;
			this.pictureBox1.Show();
			fileName = ofd.FileName;
            prj.grid.LoadRoom(fileName);
			thread = new Thread(prj.grid.fillFull);
			*/
			//thread.Start();
			this.pictureBox1.Invalidate();
        }

		private int xOffset()
		{
			return Math.Max((pictureBox1.Width - prj.grid.Width() * Cell.r) / 2, 0);
		}

		private int yOffset()
		{
			return Math.Max((pictureBox1.Height - prj.grid.Height() * Cell.r) / 2, 0);
		}

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
			CreatePainter cptr = new CreatePainter(xOffset(), yOffset(), e.Graphics);
			prj.grid.Draw(cptr);
			Size size = new Size(prj.grid.Width() * Cell.r, prj.grid.Height() * Cell.r);
			panel1.AutoScrollMinSize = size;
		}

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            this.pictureBox1.Invalidate();
        }

		private void incSize_Click(object sender, EventArgs e)
		{
			if (prj.grid == null) return;
			++Cell.r;
			this.pictureBox1.Invalidate();
		}

		private void decSize_Click(object sender, EventArgs e)
		{
			if (prj.grid == null) return;
			if (Cell.r > 1) {
				--Cell.r;
				this.pictureBox1.Invalidate();
			}
		}

		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{

		}

		private void timerMove_Tick(object sender, EventArgs e)
		{
			if (NUM > 0) {
				prj.grid.generateAgents();
				NUM--;
			}
			prj.grid.makeMove();
			this.pictureBox1.Invalidate();
		}

		private void createToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.pictureBox1.Show();
			prj.grid = new Grid();
			prj.grid.createEmptyGrid(this.panel1.Height / Cell.r, this.panel1.Width / Cell.r);
			this.WallToolStripMenuItem.Enabled = true;
			this.SpawnToolStripMenuItem.Enabled = true;
			this.ExitToolStripMenuItem.Enabled = true;
			this.CheckpointToolStripMenuItem.Enabled = true;
			this.pictureBox1.MouseDown += pictureBox1_MouseDown;
			this.pictureBox1.MouseMove += pictureBox1_MouseMove;
			this.pictureBox1.MouseUp += pictureBox1_MouseUp;
			this.pictureBox1.Invalidate();
		}

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			if (prj.grid == null) return;
			Cell chosen = prj.grid.findCellPictureCoords(new Point(e.X, e.Y), xOffset(), yOffset());
			if (chosen != null) {
				Cell toSet = null;
				if (e.Button == MouseButtons.Left) {
					toSet = factory.createCell(chosen.X, chosen.Y);
				} else if (e.Button == MouseButtons.Right) {
					toSet = new EmptyCell(chosen.X, chosen.Y);
				}
				if (toSet != null) {
					prj.grid.setCell(chosen.X, chosen.Y, toSet);
				}
			}
			isDown = true;
			this.pictureBox1.Invalidate();
		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			if (isDown && prj.grid != null) {
				Cell chosen = prj.grid.findCellPictureCoords(new Point(e.X, e.Y), xOffset(), yOffset());
				if (chosen != null) {
					Cell toSet = null;
					if (e.Button == MouseButtons.Left) {
						toSet = factory.createCell(chosen.X, chosen.Y);
					} else if (e.Button == MouseButtons.Right) {
						toSet = new EmptyCell(chosen.X, chosen.Y);
					}
					if (toSet != null) {
						prj.grid.setCell(chosen.X, chosen.Y, toSet);
					}
				}
			}
			this.pictureBox1.Invalidate();
		}

		private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
		{
			isDown = false;
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.saveFileDialog1.ShowDialog() == DialogResult.OK) {
				SaveState(this.saveFileDialog1.FileName);
			}
		}

		private void WallToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (ToolStripMenuItem tsmi in this.EditToolStripMenuItem.DropDownItems) {
				tsmi.Checked = false;
			}
			this.WallToolStripMenuItem.Checked = true;
			this.menuStrip1.Invalidate();
			factory = new WallFactory();
		}

		private void SpawnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (ToolStripMenuItem tsmi in this.EditToolStripMenuItem.DropDownItems) {
				tsmi.Checked = false;
			}
			this.SpawnToolStripMenuItem.Checked = true;
			factory = new SpawnFactory();
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (ToolStripMenuItem tsmi in this.EditToolStripMenuItem.DropDownItems) {
				tsmi.Checked = false;
			}
			this.ExitToolStripMenuItem.Checked = true;
			factory = new ExitFactory();
		}

		private void CheckpointToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (ToolStripMenuItem tsmi in this.EditToolStripMenuItem.DropDownItems) {
				tsmi.Checked = false;
			}
			this.CheckpointToolStripMenuItem.Checked = true;
			factory = new CheckpointFactory();
		}

		private void FinishEditToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (prj.grid == null) return;
			prj.grid.CutGrid();
			this.pictureBox1.Invalidate();
		}
	}    
}
