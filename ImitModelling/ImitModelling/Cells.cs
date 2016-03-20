using System;
using System.Drawing;

namespace ImitModelling
{
	[Serializable]
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
		public abstract void Draw(Painter painter);
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

	[Serializable]
	public abstract class NotOccupiedCell : Cell
	{
		public NotOccupiedCell(int x, int y) : base(x, y)
		{
		}
	}

	[Serializable]
	public abstract class OccupiedCell : Cell
	{
		public OccupiedCell(int x, int y) : base(x, y)
		{
		}
	}

	[Serializable]
	public class EmptyCell : NotOccupiedCell
	{
		public EmptyCell(int x, int y) : base(x, y)
		{
		}
		public override void Draw(Painter painter)
		{
			painter.draw(this);
		}
	}

	[Serializable]
	public class SpawnCell : NotOccupiedCell
	{
		private double distribution;
		public SpawnCell(int x, int y, double distr) : base(x, y)
		{
			distribution = distr;
		}

		public override void Draw(Painter painter)
		{
			painter.draw(this);
		}
	}

	[Serializable]
	public class AgentCell : OccupiedCell
	{
		public AgentCell(int x, int y) : base(x, y)
		{
		}
		public override void Draw(Painter painter)
		{
			painter.draw(this);
		}
	}

	[Serializable]
	public class ExitCell : NotOccupiedCell
	{
		public ExitCell(int x, int y) : base(x, y)
		{
		}
		public override void Draw(Painter painter)
		{
			painter.draw(this);
		}
	}

	[Serializable]
	public class WallCell : OccupiedCell
	{
		public WallCell(int x, int y) : base(x, y)
		{
		}
		public override void Draw(Painter painter)
		{
			painter.draw(this);
		}
	}

	/*
	[Serializable]
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
	*/
}
