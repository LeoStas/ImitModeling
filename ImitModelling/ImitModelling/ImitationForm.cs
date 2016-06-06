using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Threading;

namespace ImitModelling
{
	public partial class ImitationForm : Form
    {
		[Serializable]
		class Project
		{
			public Grid grid;
			public ProgramState state;
			public int TotalEstimate;
			public int TotalAgents;
		}
		private CellFactory factory;
		private Type painter;
		private bool isDown;
		private Project prj;
		List<Event> events;
		Thread thread;

        public ImitationForm()
        {
			prj = new Project();
            prj.grid = new Grid();
			prj.state = new ProgramState();
			events = new List<Event>();
			prj.state.LoadState(this);
			isDown = false;
			factory = new EmptyFactory();
			painter = typeof(CreatePainter);
			prj.TotalEstimate = 100;
            InitializeComponent();
			this.pictureBox.Hide();
		}

		public void AddEvent(Event ev)
		{
			events.Add(ev);
		}

		public void SaveState(string filename)
		{
			var bf = new BinaryFormatter();
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
			prj.state.Reload(this);
			prj.state.LoadState(this);
			Cell.r = (int)bf.Deserialize(fs);
			fs.Close();
		}

		private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK) {
				LoadState(this.openFileDialog1.FileName);
				prj.state.LoadState(this);
				this.pictureBox.Show();
				this.pictureBox.Invalidate();
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
			//this.pictureBox.Invalidate();
        }

		private int xOffset()
		{
			return Math.Max((pictureBox.Width - prj.grid.Width() * Cell.r) / 2, 0);
		}

		private int yOffset()
		{
			return Math.Max((pictureBox.Height - prj.grid.Height() * Cell.r) / 2, 0);
		}

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
			Painter cptr;
			if (painter == typeof(CreatePainter)) {
				cptr = new CreatePainter(xOffset(), yOffset(), e.Graphics, prj.grid);
			} else {
				cptr = new WorkPainter(xOffset(), yOffset(), e.Graphics, prj.grid);
			}
			prj.grid.Draw(cptr);
			Size size = new Size(prj.grid.Width() * Cell.r, prj.grid.Height() * Cell.r);
			panel1.AutoScrollMinSize = size;
		}

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            this.pictureBox.Invalidate();
        }

		private void incSize_Click(object sender, EventArgs e)
		{
			if (prj.grid == null) return;
			++Cell.r;
			this.pictureBox.Invalidate();
		}

		private void decSize_Click(object sender, EventArgs e)
		{
			if (prj.grid == null) return;
			if (Cell.r > 1) {
				--Cell.r;
				this.pictureBox.Invalidate();
			}
		}

		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{

		}

		private void timerMove_Tick(object sender, EventArgs e)
		{
			/*
			EventExecutor evexec = new EventExecutor(prj.grid, this);
			if (events.Count > 0) {
				
				ev.execute(evexec);
			}
			*/
			if (thread != null && thread.ThreadState == ThreadState.Suspended) {
				this.pictureBox.Invalidate();
				thread.Resume();
			} else if (thread != null && thread.ThreadState == ThreadState.Stopped) {
				timerMove.Stop();
				TickEvent ev = events[0] as TickEvent;
				events.RemoveAt(0);
				string str = "Эвакуация проведена за " + ev.Ticks + " тактов";
				MessageBox.Show(this, str, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
			}
		}

		private void createToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.pictureBox.Show();
			prj.grid = new Grid();
			prj.grid.createEmptyGrid(this.panel1.Height / Cell.r, this.panel1.Width / Cell.r);
			prj.state = new EditAllCellsState(this);
			prj.state.LoadState(this);
			this.pictureBox.Invalidate();
		}

		public void LoadMenuEdit(bool [] enabled)
		{
			if (enabled == null) return;
			int i = 0;
			foreach (ToolStripMenuItem tsmi in this.DrawToolStripMenuItem.DropDownItems) {
				tsmi.Checked = false;
				tsmi.Enabled = enabled[i];
			}
		}

		public void LoadMouseHandlers(MouseEventHandler[] evHandlers)
		{
			if (evHandlers == null) return;
			this.pictureBox.MouseDown -= EditAll_PictureBox_MouseDown;
			this.pictureBox.MouseDown -= EditSpawn_PictureBox_MouseDown;
			this.pictureBox.MouseMove -= EditAll_PictureBox_MouseMove;
			this.pictureBox.MouseMove -= EditSpawn_PictureBox_MouseMove;
			this.pictureBox.MouseUp -= EditAll_PictureBox_MouseUp;
			this.pictureBox.MouseUp -= EditSpawn_PictureBox_MouseUp;
			this.pictureBox.MouseDown += evHandlers[(int)ProgramState.MouseHandlersNames.MouseDownName];
			this.pictureBox.MouseMove += evHandlers[(int)ProgramState.MouseHandlersNames.MouseMoveName];
			this.pictureBox.MouseUp += evHandlers[(int)ProgramState.MouseHandlersNames.MouseUpName];
		}

		public void EditAll_PictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			if (prj.grid == null) return;
			Cell chosen = prj.grid.findCellPictureCoords(new Point(e.X, e.Y), xOffset(), yOffset());
			if (chosen != null) {
				Cell toSet = null;
				if (e.Button == MouseButtons.Left) {
					toSet = factory.createCell(chosen.X, chosen.Y);
				} else if (e.Button == MouseButtons.Right) {
					toSet = new EmptyCell(chosen.X, chosen.Y);
					if (chosen is ExitCell) {
						foreach (var exit in prj.grid.exitCells) {
							if (exit.X == chosen.X && exit.Y == chosen.Y) {
								prj.grid.exitCells.Remove(exit);
								break;
							}
						}
					} else if (chosen is SpawnCell) {
						foreach (var spawn in prj.grid.spawnCells) {
							if (spawn.X == chosen.X && spawn.Y == chosen.Y) {
								prj.grid.spawnCells.Remove(spawn);
								break;
							}
						}
					}
				}
				if (toSet != null) {
					prj.grid.setCell(chosen.X, chosen.Y, toSet);
				}
			}
			isDown = true;
			this.pictureBox.Invalidate();
		}

		public void EditAll_PictureBox_MouseMove(object sender, MouseEventArgs e)
		{
			if (isDown && prj.grid != null) {
				Cell chosen = prj.grid.findCellPictureCoords(new Point(e.X, e.Y), xOffset(), yOffset());
				if (chosen != null) {
					Cell toSet = null;
					if (e.Button == MouseButtons.Left) {
						toSet = factory.createCell(chosen.X, chosen.Y);
					} else if (e.Button == MouseButtons.Right) {
						toSet = new EmptyCell(chosen.X, chosen.Y);
						if (chosen is ExitCell) {
							foreach (var exit in prj.grid.exitCells) {
								if (exit.X == chosen.X && exit.Y == chosen.Y) {
									prj.grid.exitCells.Remove(exit);
									break;
								}
							}
						} else if (chosen is SpawnCell) {
							foreach (var spawn in prj.grid.spawnCells) {
								if (spawn.X == chosen.X && spawn.Y == chosen.Y) {
									prj.grid.spawnCells.Remove(spawn);
									break;
								}
							}
						}
					}
					if (toSet != null) {
						prj.grid.setCell(chosen.X, chosen.Y, toSet);
					}
				}
			}
			this.pictureBox.Invalidate();
		}

		public void EditAll_PictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			isDown = false;
		}

		public void EditSpawn_PictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			if (prj.grid == null) return;
			Cell chosen = prj.grid.findCellPictureCoords(new Point(e.X, e.Y), xOffset(), yOffset());
			if (chosen != null) {
				if (e.Button == MouseButtons.Left) {
					if (chosen is SpawnCell) {
						prj.grid.savedSpawn = chosen as SpawnCell;
					}
					if (chosen is EmptyCell && prj.grid.savedSpawn != null) {
						CheckpointCell toSet = new CheckpointCell(chosen.X, chosen.Y);
						prj.grid.savedSpawn.checkPoints.Add(new Tuple<int, int>(chosen.X, chosen.Y));
						prj.grid.setCell(chosen.X, chosen.Y, toSet);
					} else if (chosen is CheckpointCell && prj.grid.savedSpawn != null) {
						var coords = new Tuple<int, int>(chosen.X, chosen.Y);
						if (!prj.grid.savedSpawn.checkPoints.Contains(coords)) {
							prj.grid.savedSpawn.checkPoints.Add(coords);
						}
					}
				} else if (e.Button == MouseButtons.Right) {
					if (chosen is SpawnCell) {
						prj.grid.savedSpawn = chosen as SpawnCell;
						this.pictureBox.Invalidate();
						SpawnDistribution form = new SpawnDistribution((int)Math.Round(prj.grid.savedSpawn.Distribution * 100), prj.TotalEstimate);
						form.DistribChanged += new SpawnDistribution.SpawnDistribChangedHandler(F1_DistribChanged);
						form.ShowDialog(this);
					} else if (chosen is CheckpointCell) {
						bool hasOther = false;
						foreach (var spawn in prj.grid.spawnCells) {
							if (spawn == prj.grid.savedSpawn) {
								spawn.checkPoints.Remove(new Tuple<int, int>(chosen.X, chosen.Y));
							} else {
								hasOther = true;
							}
						}
						if (!hasOther) {
							prj.grid.setCell(chosen.X, chosen.Y, new EmptyCell(chosen.X, chosen.Y));
						}
					}
				}
			}
			this.pictureBox.Invalidate();
		}

		public void EditSpawn_PictureBox_MouseMove(object sender, MouseEventArgs e)
		{

		}

		public void EditSpawn_PictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			
		}

		public void F1_DistribChanged(object sender, DistribEventArgs e)
		{
			prj.TotalEstimate -= (int)(Math.Round((e.Distribution - prj.grid.savedSpawn.Distribution) * 100));
			prj.grid.savedSpawn.Distribution = e.Distribution;
			this.pictureBox.Invalidate();
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.saveFileDialog1.ShowDialog() == DialogResult.OK) {
				SaveState(this.saveFileDialog1.FileName);
			}
		}

		private void WallToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (ToolStripMenuItem tsmi in this.DrawToolStripMenuItem.DropDownItems) {
				tsmi.Checked = false;
			}
			this.WallToolStripMenuItem.Checked = true;
			this.menuStrip1.Invalidate();
			factory = new WallFactory();
		}

		private void SpawnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (ToolStripMenuItem tsmi in this.DrawToolStripMenuItem.DropDownItems) {
				tsmi.Checked = false;
			}
			this.SpawnToolStripMenuItem.Checked = true;
			factory = new SpawnFactory();
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (ToolStripMenuItem tsmi in this.DrawToolStripMenuItem.DropDownItems) {
				tsmi.Checked = false;
			}
			this.ExitToolStripMenuItem.Checked = true;
			factory = new ExitFactory();
		}

		private void FinishEditToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (prj.grid == null) return;
			prj.grid.CutGrid();
			prj.state = new EditSpawnsState(this);
			prj.state.LoadState(this);
			this.pictureBox.Invalidate();
		}

		public void setTotalEnabled(bool enabled)
		{
			this.totalAgentsUpDown.Enabled = enabled;
		}

		public void reDraw()
		{
			this.pictureBox.Invalidate();
		}

		private void StartDemoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (prj.TotalAgents <= 0) {
				return;
			}
			foreach (var spawn in prj.grid.spawnCells) {
				events.Add(new GenerateAgentsEvent(spawn, (int)Math.Round(spawn.Distribution * prj.TotalAgents)));
			}
			AddEvent(new TickEvent());
			EventExecutor evexec = new EventExecutor(prj.grid, this);
			/*while (events.Count > 0) {
				Event ev = events[0];
				events.RemoveAt(0);
				ev.execute(evexec);
			}*/
			prj.state = new WorkingState(this);
			prj.state.LoadState(this);
			painter = typeof(WorkPainter);
			thread = new Thread(Cycle);
			this.timerMove.Start();
			thread.Start();
		}

		private void Cycle()
		{
			EventExecutor evexec = new EventExecutor(prj.grid, this);
			while (events.Count > 0) {
				Event ev = events[0];
				events.RemoveAt(0);
				ev.execute(evexec);
				bool last = false;
				if (ev is TickEvent) {
					events.Add(ev);
					last = (events.Count <= 1);
					thread.Suspend();
				}
				if (last) {
					break;
				}
			}
		}

		private void totalAgentsUpDown_ValueChanged(object sender, EventArgs e)
		{
			prj.TotalAgents = (int)this.totalAgentsUpDown.Value;
		}

		private void SlowDemoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			timerMove.Interval *= timerMove.Interval > 10000 ? 1 : 5;
		}

		private void SpeedUpDemoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			timerMove.Interval /= timerMove.Interval >= 5 ? 5 : 1;
		}
	}
	public class DistribEventArgs : EventArgs
	{
		private double distribution;
		public double Distribution
		{
			get
			{
				return distribution;
			}
			set
			{
				distribution = value;
			}
		}
		public DistribEventArgs(double distribution) : base()
		{
			this.distribution = distribution;
		}
	}
}
