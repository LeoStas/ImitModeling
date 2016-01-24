using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace ImitModelling
{
	public class Grid
	{
		private Cell[,] grid;
		private List<Cell> spawnCells;
		private List<Cell> agentCells;
		private List<Cell> exitCells;
		private int xOffset;
		private int yOffset;
		public Grid()
		{
			spawnCells = null;
			grid = null;
		}

		public bool setCell(int i, int j, Cell cell)
		{
			if (i < 0 || i >= grid.GetLength(0) || j < 0 || j >= grid.GetLength(1)) {
				return false;
			}
			grid[i, j] = cell;
			return true;
		}

		public void Draw(int pictureWidth, int pictureHeight, Graphics g)
		{
			if (grid == null) return;
			xOffset = Math.Max((pictureWidth - grid.GetLength(0) * Cell.r) / 2, 0);
			yOffset = Math.Max((pictureHeight - grid.GetLength(1) * Cell.r) / 2, 0);
			for (int i = 0; i < grid.GetLength(0); ++i) {
				for (int j = 0; j < grid.GetLength(1); ++j) {
					grid[i, j].Draw(xOffset, yOffset, g);
				}
			}
		}

		public Cell findCellPictureCoords(Point picturePoint)
		{
			for (int i = 0; i < grid.GetLength(0); ++i) {
				for (int j = 0; j < grid.GetLength(1); ++j) {
					Point coord = grid[i, j].gridToPictureTransform(xOffset, yOffset);
					if (picturePoint.X <= coord.X + Cell.r && picturePoint.X >= coord.X &&
							picturePoint.Y >= coord.Y && picturePoint.Y <= coord.Y + Cell.r) {
						return grid[i, j];
					}
				}
			}
			return null;
		}

		public List<Point> neightbours(Point cur)
		{
			if (grid == null) {
				return null;
			}
			List<Point> res = new List<Point>();
			for (int i = Math.Max(cur.X - 1, 0); i < Math.Min(cur.X + 2, grid.GetLength(0)); ++i) {
				for (int j = Math.Max(cur.Y - 1, 0); j < Math.Min(cur.Y + 2, grid.GetLength(1)); ++j) {
					if (cur.X == i && cur.Y == j) continue;
					res.Add(new Point(i, j));
				}
			}
			return res;
		}

		public List<Cell> neightboursCrux(Cell cur)
		{
			if (grid == null) {
				return null;
			}
			List<Cell> res = new List<Cell>();
			if (cur.X - 1 >= 0) {
				res.Add(grid[cur.X - 1, cur.Y]);
			}
			if (cur.X + 1 < grid.GetLength(0)) {
				res.Add(grid[cur.X + 1, cur.Y]);
			}
			if (cur.Y - 1 >= 0) {
				res.Add(grid[cur.X, cur.Y - 1]);
			}
			if (cur.Y + 1 < grid.GetLength(1)) {
				res.Add(grid[cur.X, cur.Y + 1]);
			}
			return res;
		}

		public void generateAgents()
		{
			if (grid == null) return;
			foreach (Cell cell in spawnCells) {
				List<Point> p = neightbours(new Point(cell.X, cell.Y));
				foreach (Point cur in p) {
					if (grid[cur.X, cur.Y] is EmptyCell || grid[cur.X, cur.Y] is SpawnCell) {
						grid[cur.X, cur.Y] = new AgentCell(cur.X, cur.Y);
						if (agentCells == null) {
							agentCells = new List<Cell>();
						}
						agentCells.Add(grid[cur.X, cur.Y]);
					}
				}
			}
		}

		private int distanceLeft(Cell cur)
		{
			if (exitCells == null || exitCells.Count == 0) {
				return 0;
			}
			var res = Math.Abs(cur.X - exitCells[0].X) + Math.Abs(cur.Y - exitCells[0].Y);
			foreach (Cell exit in exitCells) {
				res = Math.Min(res, Math.Abs(cur.X - exit.X) + Math.Abs(cur.Y - exit.Y));
			}
			return res;
		}

		private class AStarEntity
		{
			public Cell cell;
			public Cell parent;
			public int distFromStart;
			public int approxLeft;
			public AStarEntity(Cell cell, Cell parent, int dist, int approx)
			{
				this.cell = cell;
				this.parent = parent;
				this.distFromStart = dist;
				this.approxLeft = approx;
			}
			public AStarEntity()
			{
				this.cell = null;
				this.parent = null;
				this.distFromStart = 0;
				this.approxLeft = 0;
			}
			public int cost()
			{
				return distFromStart + approxLeft;
			}
			public static bool operator<(AStarEntity a, AStarEntity b)
			{
				return a.cost() < b.cost();
			}
			public static bool operator>(AStarEntity a, AStarEntity b)
			{
				return a.cost() > b.cost();
			}
		}

		public List<Cell> AStarPath(Cell start)
		{
			if (grid == null) return null;
			List<AStarEntity> open = new List<AStarEntity>();
			List<AStarEntity> close = new List<AStarEntity>();
			AStarEntity cur = new AStarEntity(start, null, 0, distanceLeft(start));
			open.Add(cur);
			Dictionary<Cell, AStarEntity> dict = new Dictionary<Cell, AStarEntity>();
			dict[cur.cell] = cur;
			List<Cell> res = null;
			while (open.Count > 0) {
				cur = open[0];
				open.RemoveAt(0);
				if (exitCells.Contains(cur.cell)) {
					res = new List<Cell>();
					while (cur != null) {
						res.Insert(0, cur.cell);
						if (cur.parent != null) {
							cur = dict[cur.parent];
						} else {
							cur = null;
						}
					}
					break;
				}
				foreach (Cell neightbour in neightboursCrux(cur.cell)) {
					if (!(neightbour is EmptyCell) && !(neightbour is ExitCell)) {
						continue;
					}
					int newg = cur.distFromStart + 1;
					if (!dict.ContainsKey(neightbour)) {
						dict[neightbour] = new AStarEntity();
						dict[neightbour].cell = neightbour;
					}
					AStarEntity aneightb = dict[neightbour];
					if ((open.Contains(aneightb) || close.Contains(aneightb)) &&
							aneightb.distFromStart <= newg) {
						continue;
					}
					if (!close.Contains(aneightb) || newg < aneightb.distFromStart) {
						aneightb.parent = cur.cell;
						aneightb.distFromStart = newg;
						aneightb.approxLeft = distanceLeft(neightbour);

						/*if (close.Contains(aneightb)) {
							close.Remove(aneightb);
						}*/
						if (!open.Contains(aneightb)) {
							open.Add(aneightb);
							open.Sort(delegate (AStarEntity x, AStarEntity y)
							{
								if (x.cost() == y.cost()) return 0;
								else if (x.cost() < y.cost()) return -1;
								else return 1;
							});
						}
					}
				}
				close.Add(cur);
			}
			return res;
		}

		public void LoadRoom(string filename)
		{
			if (filename == null) return;
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);
			foreach (XmlNode node in doc.SelectNodes("room")) {
				int h = 0, w = 0;
				foreach (XmlAttribute attr in node.Attributes) {
					switch (attr.Name) {
						case "width":
							w = Int32.Parse(attr.Value.ToString()) + 1;
							break;
						case "height":
							h = Int32.Parse(attr.Value.ToString()) + 1;
							break;
						default:
							break;
					}
				}
				grid = new Cell[w, h];
				spawnCells = new List<Cell>();
				for (int i = 0; i < w; ++i) {
					for (int j = 0; j < h; ++j) {
						grid[i, j] = new EmptyCell(i, j);
					}
				}
				foreach (XmlNode child in node.SelectNodes("wall")) {
					//Debug.WriteLine(string.Format("{0} = {1}", child.Name, child.InnerText));
					int xstart = 0, ystart = 0;
					int xend = 0, yend = 0;
					foreach (XmlAttribute attr in child.Attributes) {
						switch (attr.Name) {
							case "xstart":
								xstart = Int32.Parse(attr.Value.ToString());
								break;
							case "ystart":
								ystart = Int32.Parse(attr.Value.ToString());
								break;
							case "xend":
								xend = Int32.Parse(attr.Value.ToString());
								break;
							case "yend":
								yend = Int32.Parse(attr.Value.ToString());
								break;
							default:
								break;
						}
					}
					for (int i = xstart; i <= xend; ++i) {
						for (int j = ystart; j <= yend; ++j) {
							grid[i, j] = new WallCell(i, j);
						}
					}
				}
				foreach (XmlNode child in node.SelectNodes("spawn")) {
					int x = 0, y = 0;
					double distr = 0.0;
					foreach (XmlAttribute attr in child.Attributes) {
						switch (attr.Name) {
							case "x":
								x = Int32.Parse(attr.Value.ToString());
								break;
							case "y":
								y = Int32.Parse(attr.Value.ToString());
								break;
							case "distribution":
								distr = Double.Parse(attr.Value.ToString());
								break;
							default:
								break;
						}
					}
					grid[x, y] = new SpawnCell(x, y, distr);
					spawnCells.Add(grid[x, y]);
				}
				exitCells = new List<Cell>();
				foreach (XmlNode child in node.SelectNodes("exit")) {
					int xstart = 0, ystart = 0;
					int xend = 0, yend = 0;
					foreach (XmlAttribute attr in child.Attributes) {
						switch (attr.Name) {
							case "xstart":
								xstart = Int32.Parse(attr.Value.ToString());
								break;
							case "ystart":
								ystart = Int32.Parse(attr.Value.ToString());
								break;
							case "xend":
								xend = Int32.Parse(attr.Value.ToString());
								break;
							case "yend":
								yend = Int32.Parse(attr.Value.ToString());
								break;
							default:
								break;
						}
					}
					for (int i = xstart; i <= xend; ++i) {
						for (int j = ystart; j <= yend; ++j) {
							grid[i, j] = new ExitCell(i, j);
							exitCells.Add(grid[i, j]);
						}
					}
				}
				//Debug.WriteLine("--------------");
			}
		}
	}
}
