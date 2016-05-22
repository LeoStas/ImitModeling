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
		private List<Tuple<int, int>> checkpoints;
		public List<Tuple<int, int>> CheckPoints
		{
			get
			{
				return checkpoints;
			}
			set
			{
				checkpoints = value;
			}
		}
		public AgentCell Agent
		{
			get
			{
				return agent;
			}
			set
			{
				agent = value;
			}
		}
		public AgentMoveEvent(AgentCell agent, List<Tuple<int, int>> checkpoints) : base()
		{
			this.agent = agent;
			this.checkpoints = checkpoints;
		}
		public override void execute(EventExecutor executor)
		{
			executor.execute(this);
		}
	}

	public class EventExecutor
	{
		private Grid grid;
		private ImitationForm form;
		public EventExecutor(Grid grid, ImitationForm form)
		{
			this.grid = grid;
			this.form = form;
		}

		public int Distance(Tuple<int, int> coord1, Tuple<int, int> coord2)
		{
			return Math.Abs(coord1.Item1 - coord2.Item1) + Math.Abs(coord1.Item2 - coord2.Item2);
		}

		public void execute(AgentMoveEvent ev)
		{
			List<Tuple<int, int>> neighbours = grid.neightboursCross(new Tuple<int, int>(ev.Agent.X, ev.Agent.Y));
			Dictionary<Tuple<int, int>, int> dict = new Dictionary<Tuple<int, int>, int>();
			foreach (var neighbour in neighbours) {
				if (ev.CheckPoints.Count > 0) {
					dict[neighbour] = Distance(neighbour, ev.CheckPoints[0]);
				} else {
					int minDistance = -1;
					foreach (var exit in grid.exitCells) {
						minDistance = minDistance == -1 ? Distance(neighbour, new Tuple<int, int>(exit.X, exit.Y)) :
								Math.Min(minDistance, Distance(neighbour, new Tuple<int, int>(exit.X, exit.Y)));
					}
					dict[neighbour] = minDistance;
				}
			}
			List<KeyValuePair<Tuple<int, int>, int>> distances = dict.ToList();
			distances.Sort(delegate (KeyValuePair<Tuple<int, int>, int> pair1,
				KeyValuePair<Tuple<int, int>, int> pair2)
				{
					return pair1.Value.CompareTo(pair2.Value);
				}
			);
			for (int i = 0; i < distances.Count && distances[i].Value == distances[0].Value; ++i) {
				if (grid.getCell(distances[i].Key) is NotOccupiedCell) {
					grid.setCell(ev.Agent.X, ev.Agent.Y, new EmptyCell(ev.Agent.X, ev.Agent.Y));
					ev.Agent.X = distances[i].Key.Item1;
					ev.Agent.Y = distances[i].Key.Item2;
					grid.setCell(distances[i].Key, ev.Agent);
					break;
				}
			}
			if (ev.CheckPoints.Count > 0 && Distance(new Tuple<int, int>(ev.Agent.X, ev.Agent.Y), ev.CheckPoints[0]) <= 2) {
				ev.CheckPoints.RemoveAt(0);
			}
			form.AddEvent(ev);
		}
		public void execute(GenerateAgentsEvent ev)
		{
			List<Tuple<int, int>> neighbours = grid.neightbours(new Tuple<int, int>(ev.Spawn.X, ev.Spawn.Y));
			foreach (var neighbour in neighbours) {
				if (ev.EstimateAgents == 0) break;
				if (grid.getCell(neighbour) is NotOccupiedCell) {
					AgentCell agent = new AgentCell(neighbour.Item1, neighbour.Item2);
					grid.setCell(neighbour.Item1, neighbour.Item2, agent);
					form.AddEvent(new AgentMoveEvent(agent, ev.Spawn.checkPoints));
					ev.EstimateAgents--;
				}
			}
			if (ev.EstimateAgents > 0) {
				form.AddEvent(ev);
			}
		}
	}
}
