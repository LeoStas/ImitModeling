using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.Threading;

namespace ImitModelling
{
	[Serializable]
	public class Grid
	{
		private Cell[,] grid;
		private int[,] weights;
		public List<SpawnCell> spawnCells;
		public List<AgentCell> agentCells;
		public List<ExitCell> exitCells;
		public SpawnCell savedSpawn;
		public Grid()
		{
			spawnCells = new List<SpawnCell>();
			agentCells = new List<AgentCell>();
			exitCells = new List<ExitCell>();
			grid = null;
			savedSpawn = null;
		}

		public int Weight(Tuple<int, int> coord)
		{
			if (weights != null) {
				return weights[coord.Item2, coord.Item1];
			}
			weights = new int[Height(), Width()];
			for (int i = 0; i < Height(); ++i) {
				for (int j = 0; j < Width(); ++j) {
					weights[i, j] = -1;
				}
			}
			List<Tuple<int, int>> open = new List<Tuple<int, int>>();
			foreach (ExitCell exit in exitCells) {
				weights[exit.Y, exit.X] = 0;
				open.Add(new Tuple<int, int>(exit.X, exit.Y));
			}
			while (open.Count > 0) {
				Tuple<int, int> cur = open[0];
				open.RemoveAt(0);
				int curWeight = weights[cur.Item2, cur.Item1];
				foreach (var neightbour in neightboursCross(cur)) {
					if (getCell(neightbour) is WallCell) {
						continue;
					}
					int prevWeight = weights[neightbour.Item2, neightbour.Item1];
					if (prevWeight == -1 || prevWeight > curWeight + 1) {
						weights[neightbour.Item2, neightbour.Item1] = curWeight + 1;
						if (!open.Contains(neightbour)) {
							open.Add(neightbour);
						}
					}
				}
			}
			return weights[coord.Item2, coord.Item1];
		}

		public int Width()
		{
			return grid.GetLength(1);
		}

		public int Height()
		{
			return grid.GetLength(0);
		}

		public void CutGrid()
		{
			int xStart = Width() - 1, xEnd = 0;
			int yStart = Height() - 1, yEnd = 0;
			for (int i = 0; i < Height(); ++i) {
				for (int j = 0; j < Width(); ++j) {
					if (!(grid[i, j] is EmptyCell)) {
						if (xEnd < j) {
							xEnd = j;
						}
						if (xStart > j) {
							xStart = j;
						}
						if (yEnd < i) {
							yEnd = i;
						}
						if (yStart > i) {
							yStart = i;
						}
					} 
				}
			}
			int cutW = xEnd - xStart + 1;
			int cutH = yEnd - yStart + 1;
			if (cutH <= 0 || cutW <= 0) {
				return;
			}
			Cell[,] cutGrid = new Cell[cutH, cutW];
			for (int i = 0; i < cutH; ++i) {
				for (int j = 0; j < cutW; ++j) {
					cutGrid[i, j] = grid[yStart + i, xStart + j];
					var oldPoint = new Tuple<int, int>(cutGrid[i, j].X, cutGrid[i, j].Y);
					var newPoint = new Tuple<int, int>(j, i);
					
					foreach (var spawn in spawnCells) {
						if (spawn.X == oldPoint.Item1 && spawn.Y == oldPoint.Item2) {
							spawnCells.Remove(spawn);
							spawnCells.Add(cutGrid[i, j] as SpawnCell);
							break;
						}
					}
					foreach (var exit in exitCells) {
						if (exit.X == oldPoint.Item1 && exit.Y == oldPoint.Item2) {
							exitCells.Remove(exit);
							exitCells.Add(cutGrid[i, j] as ExitCell);
							break;
						}
					}
					foreach (var agent in agentCells) {
						if (agent.X == oldPoint.Item1 && agent.Y == oldPoint.Item2) {
							agentCells.Remove(agent);
							agentCells.Add(cutGrid[i, j] as AgentCell);
							break;
						}
					}
					cutGrid[i, j].X = j;
					cutGrid[i, j].Y = i;
				}
			}
			grid = cutGrid;
		}

		public void createEmptyGrid(int n, int m)
		{
			grid = new Cell[n, m];
			for (int i = 0; i < n; ++i) {
				for (int j = 0; j < m; ++j) {
					grid[i, j] = new EmptyCell(j, i);
				}
			}
		}

		public Cell getCell(int x, int y)
		{
			if (x < 0 || x >= Width() || 
				y < 0 || y >= Height()) {
				return null;
			} 
			return grid[y, x];
		}

		public Cell getCell(Tuple<int, int> p)
		{
			return getCell(p.Item1, p.Item2);
		}

		public bool setCell(int x, int y, Cell cell)
		{
			if (y < 0 || y >= Height() || x < 0 || x >= Width()) {
				return false;
			}
			grid[y, x] = cell;
			if (!(cell is AgentCell) && !(cell is EmptyCell)) {
				foreach (var cur in spawnCells) {
					if (cur.X == x && cur.Y == y) {
						spawnCells.Remove(cur);
						break;
					}
				}
				foreach (var cur in exitCells) {
					if (cur.X == x && cur.Y == y) {
						exitCells.Remove(cur);
						break;
					}
				}
				foreach (var cur in agentCells) {
					if (cur.X == x && cur.Y == y) {
						agentCells.Remove(cur);
						break;
					}
				}
			}
			if (cell is SpawnCell) {
				if (!spawnCells.Contains(cell as SpawnCell)) {
					spawnCells.Add(cell as SpawnCell);
				}
			} else if (cell is ExitCell) {
				if (!exitCells.Contains(cell as ExitCell)) {
					exitCells.Add(cell as ExitCell);
				}
			} else if (cell is AgentCell) {
				if (!agentCells.Contains(cell as AgentCell)) {
					agentCells.Add(cell as AgentCell);
				}
			}
			return true;
		}

		public bool setCell(Tuple<int, int> coords, Cell cell)
		{
			return setCell(coords.Item1, coords.Item2, cell);
		}

		public void Draw(Painter painter)
		{
			if (grid == null) return;
			for (int i = 0; i < Height(); ++i) {
				for (int j = 0; j < Width(); ++j) {
					grid[i, j].Draw(painter);
				}
			}
		}

		public Cell findCellPictureCoords(Point picturePoint, int xOffset, int yOffset)
		{
			for (int i = 0; i < Height(); ++i) {
				for (int j = 0; j < Width(); ++j) {
					Point coord = grid[i, j].gridToPictureTransform(xOffset, yOffset);
					if (picturePoint.X <= coord.X + Cell.r && picturePoint.X >= coord.X &&
							picturePoint.Y >= coord.Y && picturePoint.Y <= coord.Y + Cell.r) {
						return grid[i, j];
					}
				}
			}
			return null;
		}

		public List<Tuple<int, int> > neightbours(Tuple<int, int> cur) // cur.Item1 == X, cur.Item2 == Y
		{
			if (grid == null) {
				return null;
			}
			var res = new List<Tuple<int, int>>();
			for (int i = Math.Max(cur.Item2 - 1, 0); i < Math.Min(cur.Item2 + 2, Height()); ++i) {
				for (int j = Math.Max(cur.Item1 - 1, 0); j < Math.Min(cur.Item1 + 2, Width()); ++j) {
					if (cur.Item2 == i && cur.Item1 == j) continue;
					res.Add(new Tuple<int, int>(j, i));
				}
			}
			return res;
		}

		public List<Tuple<int, int>> neightboursCross(Tuple<int, int> cur)
		{
			if (grid == null) {
				return null;
			}
			var res = new List<Tuple<int, int>>();
			if (cur.Item1 - 1 >= 0) {
				res.Add(new Tuple<int, int>(cur.Item1 - 1, cur.Item2));
			}
			if (cur.Item1 + 1 < Width()) {
				res.Add(new Tuple<int, int>(cur.Item1 + 1, cur.Item2));
			}
			if (cur.Item2 - 1 >= 0) {
				res.Add(new Tuple < int, int >(cur.Item1, cur.Item2 - 1));
			}
			if (cur.Item2 + 1 < Height()) {
				res.Add(new Tuple<int, int>(cur.Item1, cur.Item2 + 1));
			}
			return res;
		}

		private int distanceLeft(Tuple<int, int> from, Tuple<int, int> to)
		{
			if (from == null || to == null) {
				return 0;
			}
			return Math.Abs(from.Item1 - to.Item1) + Math.Abs(from.Item2 - to.Item2);
		}

		private class AStarEntity
		{
			public Tuple<int, int> cell;
			public Tuple<int, int> parent;
			public int distFromStart; // g()
			public int approxLeft; // h()
			public AStarEntity(Tuple<int, int> cell, Tuple<int, int> parent, int dist, int approx)
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
			public int cost() // f()
			{
				return distFromStart + approxLeft;
			}
		}

		public Tuple<int, int> AStarNext(Tuple<int, int> start, Tuple<int, int> end, List<Type> forbidTypes)
		{
			if (grid == null) return null;
			var open = new List<AStarEntity>();
			var close = new List<AStarEntity>();
			var cur = new AStarEntity(start, null, 0, distanceLeft(start, end));
			open.Add(cur);
			var dict = new Dictionary<Tuple<int, int>, AStarEntity>();
			dict[cur.cell] = cur;
			Tuple<int, int> res = null;
			while (open.Count > 0) {
				cur = open[0];
				open.RemoveAt(0);
				if (cur.cell.Equals(end)) {
					res = cur.cell;
					while (!cur.cell.Equals(start)) {
						res = cur.cell;
						cur = dict[cur.parent];
					}
					break;
				}
				foreach (Tuple<int, int> neightbour in neightboursCross(cur.cell)) {
					if (forbidTypes.Contains(getCell(neightbour).GetType())) {
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
						aneightb.approxLeft = distanceLeft(neightbour, end);
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

		/*
		public List<Cell> AStarPath(Cell start, List<Type> forbidTypes)
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
				foreach (Cell neightbour in neightboursCross(cur.cell)) {
					if (forbidTypes.Contains(neightbour.GetType())) {
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
		*/

		//public void LoadRoom(string filename)
		//{
		//	if (filename == null) return;
		//	XmlDocument doc = new XmlDocument();
		//	doc.Load(filename);
		//	foreach (XmlNode node in doc.SelectNodes("room")) {
		//		int h = 0, w = 0;
		//		foreach (XmlAttribute attr in node.Attributes) {
		//			switch (attr.Name) {
		//				case "width":
		//					w = Int32.Parse(attr.Value.ToString()) + 1;
		//					break;
		//				case "height":
		//					h = Int32.Parse(attr.Value.ToString()) + 1;
		//					break;
		//				default:
		//					break;
		//			}
		//		}
		//		grid = new Cell[w, h];
		//		spawnCells = new List<Cell>();
		//		for (int i = 0; i < w; ++i) {
		//			for (int j = 0; j < h; ++j) {
		//				grid[i, j] = new EmptyCell(i, j);
		//			}
		//		}
		//		foreach (XmlNode child in node.SelectNodes("wall")) {
		//			//Debug.WriteLine(string.Format("{0} = {1}", child.Name, child.InnerText));
		//			int xstart = 0, ystart = 0;
		//			int xend = 0, yend = 0;
		//			foreach (XmlAttribute attr in child.Attributes) {
		//				switch (attr.Name) {
		//					case "xstart":
		//						xstart = Int32.Parse(attr.Value.ToString());
		//						break;
		//					case "ystart":
		//						ystart = Int32.Parse(attr.Value.ToString());
		//						break;
		//					case "xend":
		//						xend = Int32.Parse(attr.Value.ToString());
		//						break;
		//					case "yend":
		//						yend = Int32.Parse(attr.Value.ToString());
		//						break;
		//					default:
		//						break;
		//				}
		//			}
		//			for (int i = xstart; i <= xend; ++i) {
		//				for (int j = ystart; j <= yend; ++j) {
		//					grid[i, j] = new WallCell(i, j);
		//				}
		//			}
		//		}
		//		foreach (XmlNode child in node.SelectNodes("spawn")) {
		//			int x = 0, y = 0;
		//			double distr = 0.0;
		//			foreach (XmlAttribute attr in child.Attributes) {
		//				switch (attr.Name) {
		//					case "x":
		//						x = Int32.Parse(attr.Value.ToString());
		//						break;
		//					case "y":
		//						y = Int32.Parse(attr.Value.ToString());
		//						break;
		//					case "distribution":
		//						distr = Double.Parse(attr.Value.ToString());
		//						break;
		//					default:
		//						break;
		//				}
		//			}
		//			spawnCells.Add(grid[x, y]);
		//		}
		//		exitCells = new List<Cell>();
		//		foreach (XmlNode child in node.SelectNodes("exit")) {
		//			int xstart = 0, ystart = 0;
		//			int xend = 0, yend = 0;
		//			foreach (XmlAttribute attr in child.Attributes) {
		//				switch (attr.Name) {
		//					case "xstart":
		//						xstart = Int32.Parse(attr.Value.ToString());
		//						break;
		//					case "ystart":
		//						ystart = Int32.Parse(attr.Value.ToString());
		//						break;
		//					case "xend":
		//						xend = Int32.Parse(attr.Value.ToString());
		//						break;
		//					case "yend":
		//						yend = Int32.Parse(attr.Value.ToString());
		//						break;
		//					default:
		//						break;
		//				}
		//			}
		//			for (int i = xstart; i <= xend; ++i) {
		//				for (int j = ystart; j <= yend; ++j) {
		//					grid[i, j] = new ExitCell(i, j);
		//					exitCells.Add(grid[i, j]);
		//				}
		//			}
		//		}
		//		//Debug.WriteLine("--------------");
		//	}
		//}
	}
}
