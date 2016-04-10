using System;

namespace ImitModelling
{
	public abstract class CellFactory
	{
		public abstract Cell createCell(int x, int y);
	}

	public class WallFactory : CellFactory
	{
		public override Cell createCell(int x, int y)
		{
			return new WallCell(x, y);
		}
	}

	public class SpawnFactory : CellFactory
	{
		public override Cell createCell(int x, int y)
		{
			return new SpawnCell(x, y, 1.0);
		}
	}

	public class ExitFactory : CellFactory
	{
		public override Cell createCell(int x, int y)
		{
			return new ExitCell(x, y);
		}
	}

	public class EmptyFactory : CellFactory
	{
		public override Cell createCell(int x, int y)
		{
			return new EmptyCell(x, y);
		}
	}

	public class CheckpointFactory : CellFactory
	{
		public override Cell createCell(int x, int y)
		{
			return new CheckpointCell(x, y);
		}
	}

}