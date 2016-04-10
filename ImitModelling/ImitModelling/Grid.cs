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
		private List<Cell> spawnCells;
		private List<Cell> agentCells;
		private List<Cell> exitCells;
		private Dictionary<Tuple<int, int>, Tuple<int, int>> fullNext;
		public Grid()
		{
			spawnCells = null;
			agentCells = null;
			exitCells = null;
			grid = null;
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
			if (x < 0 || x >= grid.GetLength(0) || 
				y < 0 || y >= grid.GetLength(1)) {
				return null;
			} 
			return grid[x, y];
		}

		public Cell getCell(Tuple<int, int> p)
		{
			return getCell(p.Item1, p.Item2);
		}

		public void fillFull()
		{
			var forbid = new List<Type>
			{
				typeof(WallCell)
			};
			fullNext = new Dictionary<Tuple<int, int>, Tuple<int, int>>();
			for (int i = 0; i < grid.GetLength(0); ++i) {
				for (int j = 0; j < grid.GetLength(1); ++j) {
					var cur = new Tuple<int, int>(i, j);
					if (getCell(cur) is WallCell || getCell(cur) is ExitCell) {
						continue;
					}
					fullNext[cur] = AStarNext(cur, forbid);
				}
			}
		}

		public bool setCell(int x, int y, Cell cell)
		{
			if (y < 0 || y >= Height() || x < 0 || x >= Width()) {
				return false;
			}
			grid[y, x] = cell;
			return true;
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

		public List<Tuple<int, int> > neightbours(Tuple<int, int> cur)
		{
			if (grid == null) {
				return null;
			}
			var res = new List<Tuple<int, int>>();
			for (int i = Math.Max(cur.Item1 - 1, 0); i < Math.Min(cur.Item1 + 2, grid.GetLength(0)); ++i) {
				for (int j = Math.Max(cur.Item2 - 1, 0); j < Math.Min(cur.Item2 + 2, grid.GetLength(1)); ++j) {
					if (cur.Item1 == i && cur.Item2 == j) continue;
					res.Add(new Tuple<int, int>(i, j));
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
			if (cur.Item1 + 1 < grid.GetLength(0)) {
				res.Add(new Tuple<int, int>(cur.Item1 + 1, cur.Item2));
			}
			if (cur.Item2 - 1 >= 0) {
				res.Add(new Tuple < int, int >(cur.Item1, cur.Item2 - 1));
			}
			if (cur.Item2 + 1 < grid.GetLength(1)) {
				res.Add(new Tuple<int, int>(cur.Item1, cur.Item2 + 1));
			}
			return res;
		}

		public void generateAgents()
		{
			if (grid == null) return;
			foreach (Cell cell in spawnCells) {
				var p = neightbours(new Tuple<int, int>(cell.X, cell.Y));
				foreach (Tuple<int, int> cur in p) {
					if (grid[cur.Item1, cur.Item2] is EmptyCell || grid[cur.Item1, cur.Item2] is SpawnCell) {
						grid[cur.Item1, cur.Item2] = new AgentCell(cur.Item1, cur.Item2);
						if (agentCells == null) {
							agentCells = new List<Cell>();
						}
						agentCells.Add(grid[cur.Item1, cur.Item2]);
					}
				}
			}
		}

		private int distanceLeft(Tuple<int, int> cur)
		{
			if (exitCells == null || exitCells.Count == 0 || cur == null) {
				return 0;
			}
			var res = Math.Abs(cur.Item1 - exitCells[0].X) + Math.Abs(cur.Item2 - exitCells[0].Y);
			foreach (Cell exit in exitCells) {
				res = Math.Min(res, Math.Abs(cur.Item1 - exit.X) + Math.Abs(cur.Item2 - exit.Y));
			}
			return res;
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

		public void makeMove()
		{
			var paths = new List<Tuple<Cell, Tuple<int, int>>>();
			var forbid = new List<Type>
			{
				typeof(WallCell),
				typeof(AgentCell)
			};
			bool hasMore = true;
			List<Cell> tmpAgents = new List<Cell>();
			tmpAgents.AddRange(agentCells);
			while (hasMore) { 
				hasMore = false;
				for (int i = tmpAgents.Count - 1; i >= 0; --i) {
					Cell agent = tmpAgents[i];
					Tuple<int, int> next = AStarNext(new Tuple<int, int>(agent.X, agent.Y), forbid); //fullNext[new Tuple<int, int>(agent.X, agent.Y)];
					if (next == null) continue;
					if (getCell(next) is EmptyCell) {
						grid[agent.X, agent.Y] = new EmptyCell(agent.X, agent.Y);
						agent.X = next.Item1;
						agent.Y = next.Item2;
						grid[agent.X, agent.Y] = agent;
						tmpAgents.RemoveAt(i);
						hasMore = true;
					} else if (getCell(next) is ExitCell) {
						grid[agent.X, agent.Y] = new EmptyCell(agent.X, agent.Y);
						agentCells.Remove(agent);
						tmpAgents.RemoveAt(i);
					}
				}
			}
			forbid.Remove(typeof(AgentCell));
			if (tmpAgents.Count > 0) {
				foreach (Cell agent in tmpAgents) {
					Tuple<int, int> next = AStarNext(new Tuple<int, int>(agent.X, agent.Y), forbid);
					if (next != null) {
						paths.Add(new Tuple<Cell, Tuple<int, int>>(agent, next));
					}
				}
			}
			while (paths.Count > 0) {
				paths.Sort(delegate (Tuple<Cell, Tuple<int, int>> x, Tuple<Cell, Tuple<int, int>> y)
				{
					if (distanceLeft(x.Item2) == distanceLeft(y.Item2)) return 0;
					else if (distanceLeft(x.Item2) > distanceLeft(y.Item2)) return -1;
					else return 1;
				});
				for (int i = paths.Count - 1; i >= 0; --i) {
					Cell cur = paths[i].Item1;
					if (getCell(paths[i].Item2) is EmptyCell) {
						grid[cur.X, cur.Y] = new EmptyCell(cur.X, cur.Y);
						cur.X = paths[i].Item2.Item1;
						cur.Y = paths[i].Item2.Item2;
						grid[cur.X, cur.Y] = cur;
						paths.RemoveAt(i);
					} else if (getCell(paths[i].Item2) is ExitCell) {
						grid[cur.X, cur.Y] = new EmptyCell(cur.X, cur.Y);
						paths.RemoveAt(i);
						agentCells.Remove(cur);
					}
				}
				if (paths.Count > 0) break;
			}
			/*
			List<Cell> locked = new List<Cell>();
			foreach (Cell agent in agentCells) {
				Tuple<int, int> next = AStarNext(new Tuple<int, int>(agent.X, agent.Y), forbid);
				if (next == null) {
					locked.Add(agent);
					continue;
				}
				paths.Add(new Tuple<Cell, Tuple<int, int>>(agent, next));
			}
			forbid.Remove(typeof(AgentCell));
			foreach (Cell agent in locked) {
				Tuple<int, int> next = AStarNext(new Tuple<int, int>(agent.X, agent.Y), forbid);
				if (next == null) {
					continue;
				}
				paths.Add(new Tuple<Cell, Tuple<int, int>>(agent, next));
			}
			
			while (paths.Count > 0) {
				paths.Sort(delegate (Tuple<Cell, Tuple<int, int>> x, Tuple<Cell, Tuple<int, int>> y)
				{
					if (distanceLeft(x.Item2) == distanceLeft(y.Item2)) return 0;
					else if (distanceLeft(x.Item2) > distanceLeft(y.Item2)) return -1;
					else return 1;
				});
				for (int i = paths.Count - 1; i >= 0; --i) {
					Cell cur = paths[i].Item1;
					if (getCell(paths[i].Item2) is EmptyCell) {
						grid[cur.X, cur.Y] = new EmptyCell(cur.X, cur.Y);
						cur.X = paths[i].Item2.Item1;
						cur.Y = paths[i].Item2.Item2;
						grid[cur.X, cur.Y] = cur;
						paths.RemoveAt(i);
					} else if (getCell(paths[i].Item2) is ExitCell) {
						grid[cur.X, cur.Y] = new EmptyCell(cur.X, cur.Y);
						paths.RemoveAt(i);
						agentCells.Remove(cur);
					}
				}
				if (paths.Count > 0) break;
			}
			*/
		}

		public Tuple<int, int> AStarNext(Tuple<int, int> start, List<Type> forbidTypes)
		{
			if (grid == null) return null;
			var open = new List<AStarEntity>();
			var close = new List<AStarEntity>();
			var cur = new AStarEntity(start, null, 0, distanceLeft(start));
			open.Add(cur);
			var dict = new Dictionary<Tuple<int, int>, AStarEntity>();
			dict[cur.cell] = cur;
			Tuple<int, int> res = null;
			while (open.Count > 0) {
				cur = open[0];
				open.RemoveAt(0);
				if (exitCells.Contains(getCell(cur.cell))) {
					res = cur.cell;
					while (cur.cell != start) {
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
