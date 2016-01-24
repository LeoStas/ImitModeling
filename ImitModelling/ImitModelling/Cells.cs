using System;
using System.Drawing;

namespace ImitModelling
{
	public abstract class Cell
	{
		private int x;
		private int y;
		public static int r;
		static Cell()
		{
			r = 5;
		}
		public Cell(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
		public Point gridToPictureTransform(int xOffset, int yOffset)
		{
			Point res = new Point();
			res.X = x * Cell.r + xOffset;
			res.Y = y * Cell.r + yOffset;
			return res;
		}
		public abstract void Draw(int xOffset, int yOffset, Graphics g);
		public int X
		{
			get
			{
				return this.x;
			}
			set
			{
				x = value;
			}
		}
		public int Y
		{
			get
			{
				return this.y;
			}
			set
			{
				y = value;
			}
		}
	}

	public class EmptyCell : Cell
	{
		public EmptyCell(int x, int y) : base(x, y)
		{
		}
		public override void Draw(int xOffset, int yOffset, Graphics g)
		{
			Point p = gridToPictureTransform(xOffset, yOffset);
			g.DrawRectangle(Pens.Black, p.X, p.Y, Cell.r, Cell.r);
		}
	}

	public class SpawnCell : Cell
	{
		private double distribution;
		public SpawnCell(int x, int y, double distr) : base(x, y)
		{
			distribution = distr;
		}

		public override void Draw(int xOffset, int yOffset, Graphics g)
		{
		}
	}

	public class AgentCell : Cell
	{
		public AgentCell(int x, int y) : base(x, y)
		{
		}
		public override void Draw(int xOffset, int yOffset, Graphics g)
		{
			Color color = Color.Green;
			Point p = gridToPictureTransform(xOffset, yOffset);
			g.FillEllipse(new SolidBrush(color), p.X, p.Y, r, r);
		}
	}

	public class ExitCell : Cell
	{
		public ExitCell(int x, int y) : base(x, y)
		{
		}
		public override void Draw(int xOffset, int yOffset, Graphics g)
		{
			Point p = gridToPictureTransform(xOffset, yOffset);
			g.FillRectangle(new SolidBrush(Color.Orange), p.X, p.Y, Cell.r, Cell.r);
		}
	}

	public class WallCell : Cell
	{
		public WallCell(int x, int y) : base(x, y)
		{
		}
		public override void Draw(int xOffset, int yOffset, Graphics g)
		{
			Point p = gridToPictureTransform(xOffset, yOffset);
			g.FillRectangle(new SolidBrush(Color.Black), p.X, p.Y, Cell.r, Cell.r);
			//g.DrawRectangle(Pens.Black, p.X, p.Y, Cell.r, Cell.r);
		}
	}

	public class DebugCell : Cell
	{
		public DebugCell(int x, int y) : base(x, y)
		{
		}
		public override void Draw(int xOffset, int yOffset, Graphics g)
		{
			Point p = gridToPictureTransform(xOffset, yOffset);
			g.FillRectangle(new SolidBrush(Color.Red), p.X, p.Y, Cell.r, Cell.r);
		}

	}
}
