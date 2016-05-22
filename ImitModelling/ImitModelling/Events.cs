using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImitModelling
{
	public abstract class Event
	{
		//protected Grid grid;
		public abstract void execute(EventExecutor executor);
	}

	public class GenerateAgentsEvent : Event
	{
		private SpawnCell spawn;
		public SpawnCell Spawn
		{
			get
			{
				return spawn;
			}
			set
			{
				spawn = value;
			}
		}
		private int estimateAgents;
		public int EstimateAgents
		{
			get
			{
				return estimateAgents;
			}
			set
			{
				estimateAgents = value;
			}
		}
		public GenerateAgentsEvent(SpawnCell spawn, int estimateAgents) : base()
		{
			this.spawn = spawn;
			this.estimateAgents = estimateAgents;
		}
		public override void execute(EventExecutor executor)
		{
			executor.execute(this);
		}
	}

	public class AgentMoveEvent : Event
	{
		private AgentCell agent;
		public AgentMoveEvent(AgentCell agent) : base()
		{
			this.agent = agent;
		}
		public override void execute(EventExecutor executor)
		{
			executor.execute(this);
		}
	}

	public class EventExecutor
	{
		private Grid grid;
		public EventExecutor(Grid grid)
		{
			this.grid = grid;
		}
		public void execute(AgentMoveEvent ev)
		{

		}
		public void execute(GenerateAgentsEvent ev)
		{
			List<Tuple<int, int>> neighbours = grid.neightbours(new Tuple<int, int>(ev.Spawn.X, ev.Spawn.Y));
			foreach (var neighbour in neighbours) {
				if (ev.EstimateAgents == 0) return;
				if (grid.getCell(neighbour) is NotOccupiedCell) {
					AgentCell agent = new AgentCell(neighbour.Item1, neighbour.Item2);
					grid.setCell(neighbour.Item1, neighbour.Item2, agent);
					ev.EstimateAgents--;
				}
			}
		}
	}
}
