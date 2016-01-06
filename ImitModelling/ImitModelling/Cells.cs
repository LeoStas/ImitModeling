using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImitModelling
{
	public abstract class Cell
	{
		private int x;
		private int y;
		public static int r;
		static Cell()
		{
			r = 6;
		}
		public Cell(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
		public Point fieldToPictureTransform(int xOffset, int yOffset, Graphics g)
		{
			Point res = new Point();
			res.X = x * Cell.r + xOffset;
			res.Y = y * Cell.r + yOffset;
			return res;
		}
		public abstract void Draw(int pictureWidth, int pictureHeight, Graphics g);
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
		public override void Draw(int pictureWidth, int pictureHeight, Graphics g)
		{

		}
	}

	public class SpawnCell : Cell
	{
		private double distribution;
		public SpawnCell(int x, int y, double distr) : base(x, y)
		{
			distribution = distr;
		}

		public override void Draw(int pictureWidth, int pictureHeight, Graphics g)
		{

		}
	}

	public class AgentCell : Cell
	{
		public AgentCell(int x, int y) : base(x, y)
		{

		}
		public override void Draw(int pictureWidth, int pictureHeight, Graphics g)
		{
			Color color = Color.Green;
			Point p = fieldToPictureTransform(pictureWidth, pictureHeight, g);
			g.FillEllipse(new SolidBrush(color), p.X - r / 2, p.Y - r / 2, r, r);
		}
	}

	public class ExitCell : Cell
	{
		public ExitCell(int x, int y) : base(x, y)
		{

		}
		public override void Draw(int pictureWidth, int pictureHeight, Graphics g)
		{

		}
	}

	public class WallCell : Cell
	{
		public WallCell(int x, int y) : base(x, y)
		{

		}
		public override void Draw(int pictureWidth, int pictureHeight, Graphics g)
		{
			Point p = fieldToPictureTransform(pictureWidth, pictureHeight, g);
			g.FillRectangle(new SolidBrush(Color.Black), p.X, p.Y, Cell.r, Cell.r);
		}
	}
}
