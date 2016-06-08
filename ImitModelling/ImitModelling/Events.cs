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
		private List<Tuple<int, int>> visited;
		public bool surrounded = false;
		public int attempts = 0;
		public int weight;
		public List<Tuple<int, int>> Visited
		{
			get
			{
				return visited;
			}
			set
			{
				visited = value;
			}
		}
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
			this.checkpoints = new List<Tuple<int, int>>();
			this.checkpoints.AddRange(checkpoints);
			this.visited = new List<Tuple<int, int>>();
			this.visited.Add(new Tuple<int, int>(agent.X, agent.Y));
		}
		public override void execute(EventExecutor executor)
		{
			executor.execute(this);
		}
	}
	public class TickEvent : Event
	{
		private int ticks = 0;
		public int Ticks
		{
			get
			{
				return ticks;
			}
			set
			{
				ticks = value;
			}
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
		public double PyphDistance(Tuple<int, int> coord1, Tuple<int, int> coord2)
		{
			return Math.Sqrt(Math.Pow(coord1.Item1 - coord2.Item1, 2) + Math.Pow(coord1.Item2 - coord2.Item2, 2));
		}

		public void execute(TickEvent ev)
		{
			ev.Ticks++;
		}

		public void execute(AgentMoveEvent ev)
		{
			Tuple<int, int> cur = new Tuple<int, int>(ev.Agent.X, ev.Agent.Y);
			List<Tuple<int, int>> neighbours = grid.neightboursCross(cur);
			var dict = new Dictionary<Tuple<int, int>, int>();
			foreach (var neighbour in neighbours) {
				dict[neighbour] = grid.Weight(neighbour);
			}
			List<KeyValuePair<Tuple<int, int>, int>> distances = dict.ToList();
			distances.Sort(delegate (KeyValuePair<Tuple<int, int>, int> pair1,
				KeyValuePair<Tuple<int, int>, int> pair2)
				{
					int res = pair1.Value.CompareTo(pair2.Value);
					if (res != 0) {
						return res;
					}
					double diagdistance1 = -1;
					double diagdistance2 = -1;
					foreach (var exit in grid.exitCells) {
						double curDist = PyphDistance(pair1.Key, new Tuple<int, int>(exit.X, exit.Y));
						if (diagdistance1 == -1 || diagdistance1 < curDist) {
							diagdistance1 = curDist;
						}
						curDist = PyphDistance(pair2.Key, new Tuple<int, int>(exit.X, exit.Y));
						if (diagdistance2 == -1 || diagdistance2 < curDist) {
							diagdistance2 = curDist;
						}
						res = diagdistance1 < diagdistance2 ? -1 : 1;
					}
					return res;
				}
			);
			int prev = -1;
			bool moveMade = false;
			for (int i = 0; i < distances.Count; ++i) {
				if (distances[i].Value == -1) { // it's WALL!
					continue;
				}
				if (prev == -1) {
					prev = distances[i].Value;
				}
				if (grid.Weight(distances[i].Key) > grid.Weight(cur) + ev.attempts) {
					break;
				}
				var nextCell = grid.getCell(distances[i].Key);
				if (ev.Visited.Contains<Tuple<int, int>>(new Tuple<int, int>(nextCell.X, nextCell.Y))) continue;
				if (nextCell is NotOccupiedCell) {
					grid.setCell(ev.Agent.X, ev.Agent.Y, new EmptyCell(ev.Agent.X, ev.Agent.Y));
					if (!(nextCell is ExitCell)) {
						ev.Visited[0] = (new Tuple<int, int>(ev.Agent.X, ev.Agent.Y));
						ev.weight = grid.Weight(distances[i].Key);
						ev.Agent.X = nextCell.X;
						ev.Agent.Y = nextCell.Y;
						grid.setCell(distances[i].Key, ev.Agent);
					} else {
						grid.agentCells.Remove(ev.Agent);
						return;
					}
					ev.attempts = 0;
					moveMade = true;
					break;
				}
			}
			if (!moveMade && ev.attempts == 0) {
				ev.attempts = 1;
				form.PreAddEvent(ev);
				return;
			}
			/*Dictionary<Tuple<int, int>, Tuple<int, double>> dict = new Dictionary<Tuple<int, int>, Tuple<int, double>>();
			Tuple<int, int> goal = null;
			foreach (var neighbour in neighbours) {
				if (ev.CheckPoints.Count > 0) {
					dict[neighbour] = new Tuple<int, double>(Distance(neighbour, ev.CheckPoints[0]), PyphDistance(neighbour, ev.CheckPoints[0]));
					goal = ev.CheckPoints[0];
				} else {
					Tuple<int, double> minDistance = new Tuple<int, double>(-1, 0.0);
					foreach (var exit in grid.exitCells) {
						int newDist = Distance(neighbour, new Tuple<int, int>(exit.X, exit.Y));
						double newDoubDist = PyphDistance(neighbour, new Tuple<int, int>(exit.X, exit.Y));
						if (minDistance.Item1 == -1 || minDistance.Item1 > newDist || 
								(minDistance.Item1 == newDist &&  minDistance.Item2 > newDoubDist)) {
							minDistance = new Tuple<int, double>(newDist, newDoubDist);
							goal = new Tuple<int, int>(exit.X, exit.Y);
						}
					}
					dict[neighbour] = minDistance;
				}
			}
			List<KeyValuePair<Tuple<int, int>, Tuple<int, double>>> distances = dict.ToList();
			distances.Sort(delegate (KeyValuePair<Tuple<int, int>, Tuple<int, double>> pair1,
				KeyValuePair<Tuple<int, int>, Tuple<int, double>> pair2)
				{
					int res;
					if ((res = pair1.Value.Item1.CompareTo(pair2.Value.Item1)) != 0) {
						return res;
					}
					return pair1.Value.Item2.CompareTo(pair2.Value.Item2);
				}
			);
			bool isWall = true;
			bool moveMade = false;
			int curDist = -1;
			for (int i = 0; i < distances.Count; ++i) {
				var nextCell = grid.getCell(distances[i].Key);
				if (ev.Visited.Contains<Tuple<int, int>>(new Tuple<int, int>(nextCell.X, nextCell.Y))) continue;
				if (!(nextCell is WallCell)) {
					isWall = false;
				}
				if (nextCell is NotOccupiedCell) {
					grid.setCell(ev.Agent.X, ev.Agent.Y, new EmptyCell(ev.Agent.X, ev.Agent.Y));
					if (!(nextCell is ExitCell)) {
						ev.Visited[0] = (new Tuple<int, int>(ev.Agent.X, ev.Agent.Y));
						ev.Agent.X = nextCell.X;
						ev.Agent.Y = nextCell.Y;
						grid.setCell(distances[i].Key, ev.Agent);
					} else {
						grid.agentCells.Remove(ev.Agent);
						return;
					}
					ev.attempts = 0;
					moveMade = true;
					break;
				}
			}
			if (!moveMade && !isWall) {
				ev.attempts++;
			}
			if (isWall || ev.attempts > 100) {
				ev.surrounded = true;
				var forbid = new List<Type>();
				forbid.Add(typeof(WallCell));
				var coords = grid.AStarNext(new Tuple<int, int>(ev.Agent.X, ev.Agent.Y), goal, forbid);
				var cur = grid.getCell(coords);
				if (cur is NotOccupiedCell) {
					grid.setCell(ev.Agent.X, ev.Agent.Y, new EmptyCell(ev.Agent.X, ev.Agent.Y));
					if (!(cur is ExitCell)) {
						ev.LastMove = new Tuple<int, int>(ev.Agent.X, ev.Agent.Y);
						ev.Agent.X = cur.X;
						ev.Agent.Y = cur.Y;
						grid.setCell(cur.X, cur.Y, ev.Agent);
					} else {
						grid.agentCells.Remove(ev.Agent);
						return;
					}
				}
			}
			*/
			/*
			if (ev.CheckPoints.Count > 0 && Distance(new Tuple<int, int>(ev.Agent.X, ev.Agent.Y), ev.CheckPoints[0]) <= 2) {
				ev.CheckPoints.RemoveAt(0);
			}*/
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
					AgentMoveEvent aev = new AgentMoveEvent(agent, ev.Spawn.checkPoints);
					aev.weight = grid.Weight(new Tuple<int, int>(aev.Agent.X, aev.Agent.Y));
					form.AddEvent(aev);
					ev.EstimateAgents--;
				}
			}
			if (ev.EstimateAgents > 0) {
				form.AddEvent(ev);
			}
		}
	}
}
