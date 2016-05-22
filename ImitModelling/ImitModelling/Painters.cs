using System;
using System.Drawing;

namespace ImitModelling
{
	public abstract class Painter
	{
		protected int xOffset;
		protected int yOffset;
		protected Graphics g;
		protected Grid grid;
		public Painter(int xOffset, int yOffset, Graphics g, Grid grid)
		{
			this.xOffset = xOffset;
			this.yOffset = yOffset;
			this.g = g;
			this.grid = grid;
		}
		public abstract void draw(EmptyCell cell);
		public abstract void draw(ExitCell cell);
		public abstract void draw(SpawnCell cell);
		public abstract void draw(AgentCell cell);
		public abstract void draw(WallCell cell);
		public abstract void draw(CheckpointCell cell);
	}

	public class WorkPainter : Painter
	{
		public WorkPainter(int xOffset, int yOffset, Graphics g, Grid grid) : base(xOffset, yOffset, g, grid)
		{

		}
		public override void draw(EmptyCell cell)
		{
			Point p = cell.gridToPictureTransform(xOffset, yOffset);
			g.DrawRectangle(Pens.Black, p.X, p.Y, Cell.r, Cell.r);
		}
		public override void draw(ExitCell cell)
		{
			Point p = cell.gridToPictureTransform(xOffset, yOffset);
			g.FillRectangle(new SolidBrush(Color.Orange), p.X, p.Y, Cell.r, Cell.r);
		}
		public override void draw(SpawnCell cell)
		{
			Point p = cell.gridToPictureTransform(xOffset, yOffset);
			g.DrawRectangle(Pens.Black, p.X, p.Y, Cell.r, Cell.r);
		}
		public override void draw(AgentCell cell)
		{
			Color color = Color.Green;
			Point p = cell.gridToPictureTransform(xOffset, yOffset);
			g.FillEllipse(new SolidBrush(color), p.X, p.Y, Cell.r, Cell.r);
		}
		public override void draw(WallCell cell)
		{
			Point p = cell.gridToPictureTransform(xOffset, yOffset);
			g.FillRectangle(new SolidBrush(Color.Black), p.X, p.Y, Cell.r, Cell.r);
		}
		public override void draw(CheckpointCell cell)
		{
			Point p = cell.gridToPictureTransform(xOffset, yOffset);
			g.DrawRectangle(Pens.Black, p.X, p.Y, Cell.r, Cell.r);
		}
	}

	public class CreatePainter : Painter
	{
		public CreatePainter(int xOffset, int yOffset, Graphics g, Grid grid) : base(xOffset, yOffset, g, grid)
		{

		}
		public override void draw(EmptyCell cell)
		{
			Point p = cell.gridToPictureTransform(xOffset, yOffset);
			g.DrawRectangle(Pens.Black, p.X, p.Y, Cell.r, Cell.r);
		}
		public override void draw(ExitCell cell)
		{
			Point p = cell.gridToPictureTransform(xOffset, yOffset);
			g.FillRectangle(new SolidBrush(Color.Orange), p.X, p.Y, Cell.r, Cell.r);
		}
		public override void draw(SpawnCell cell)
		{
			Point p = cell.gridToPictureTransform(xOffset, yOffset);
			Color color;
			if (grid.savedSpawn == cell) {
				color = Color.BlueViolet;
			} else {
				color = Color.Blue;
			}
			g.FillRectangle(new SolidBrush(color), p.X, p.Y, Cell.r, Cell.r);
			g.DrawRectangle(Pens.Black, p.X, p.Y, Cell.r, Cell.r);
		}
		public override void draw(AgentCell cell)
		{
			Color color = Color.Green;
			Point p = cell.gridToPictureTransform(xOffset, yOffset);
			g.FillEllipse(new SolidBrush(color), p.X, p.Y, Cell.r, Cell.r);
		}
		public override void draw(WallCell cell)
		{
			Point p = cell.gridToPictureTransform(xOffset, yOffset);
			g.FillRectangle(new SolidBrush(Color.Black), p.X, p.Y, Cell.r, Cell.r);
		}
		public override void draw(CheckpointCell cell)
		{
			SpawnCell saved = grid.savedSpawn;
			Point p = cell.gridToPictureTransform(xOffset, yOffset);
			g.DrawRectangle(Pens.Black, p.X, p.Y, Cell.r, Cell.r);
			if (saved != null && saved.checkPoints.Contains(new Tuple<int, int>(cell.X, cell.Y))) {
				g.FillRectangle(new SolidBrush(Color.Red), p.X, p.Y, Cell.r, Cell.r);
			}
		}
	}
}