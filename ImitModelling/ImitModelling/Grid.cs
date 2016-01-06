using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImitModelling
{
	public class Grid
	{
		private Cell[,] grid;
		private List<Cell> spawnCells;
		public Grid()
		{
			spawnCells = null;
			grid = null;
		}
		public void Draw(int pictureWidth, int pictureHeight, Graphics g)
		{
			if (grid == null) return;
			int xOffset = Math.Max((pictureWidth - grid.GetLength(0) * Cell.r) / 2, 0);
			int yOffset = Math.Max((pictureHeight - grid.GetLength(1) * Cell.r) / 2, 0);
			for (int i = 0; i < grid.GetLength(0); ++i) {
				for (int j = 0; j < grid.GetLength(1); ++j) {
					grid[i, j].Draw(xOffset, yOffset, g);
				}
			}
		}

		public List<Point> neightbours(Point cur)
		{
			if (grid == null) {
				return null;
			}
			List<Point> res = new List<Point>();
			for (int i = Math.Max(cur.X - 1, 0); i < Math.Min(cur.X + 2, grid.GetLength(0)); ++i) {
				for (int j = Math.Max(cur.Y - 1, 0); j < Math.Min(cur.Y + 2, grid.GetLength(1)); ++j) {
					res.Add(new Point(i, j));
				}
			}
			return res;
		}

		public void generateAgents()
		{
			if (grid == null) return;
			for (int i = 0; i < grid.GetLength(0); ++i) {
				for (int j = 0; j < grid.GetLength(1); ++j) {
					if (grid[i, j] is SpawnCell) {
						List<Point> p = neightbours(new Point(i, j));
						foreach (Point cur in p) {
							if (grid[cur.X, cur.Y] is EmptyCell || grid[cur.X, cur.Y] is SpawnCell) {
								grid[cur.X, cur.Y] = new AgentCell(cur.X, cur.Y);
							}
						}
					}

				}
			}
		}
		public void LoadRoom(String filename)
		{
			if (filename == null) return;
			XmlDocument doc = new XmlDocument();
			{
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
					}

					//Debug.WriteLine("--------------");
				}
			}
		}
	}
}
