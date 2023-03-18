using System;
using System.Collections;

namespace Program
{
	public class DFS
	{	
		public List<LinkedList<int>> Graph {get; private set;} 
		private int time = 0;
		private List<bool> discovered;
		private List<bool> processed;
		private List<int> entryTime;
		private List<int> exitTime;
		
		private List<int> outDegree;
		private List<int> parent;
		private List<int> reachableAncestor;
		private bool isDirected = false;

		public DFS(ref List<LinkedList<int>> graph, bool directed = false)
		{
			Graph = graph;
			discovered = Enumerable.Repeat(false, graph.Count+1).ToList();
			processed = Enumerable.Repeat(false, graph.Count+1).ToList();
			entryTime = Enumerable.Repeat(-1, graph.Count+1).ToList();
			exitTime = Enumerable.Repeat(-1, graph.Count+1).ToList();
			parent = Enumerable.Repeat(-1, graph.Count+1).ToList();
			reachableAncestor = Enumerable.Repeat(-1, graph.Count+1).ToList();
			outDegree = Enumerable.Repeat(0, graph.Count+1).ToList();
			isDirected = directed;
		}
		
		public void FindArticulation(int v=0) 
		{
			LinkedListNode<int> p;
			int y;
			
			//if (finished) { return; }
			
			discovered[v] = true;
			time += 1;
			entryTime[v] = time;
			
			processVertexEarly(v);
			
			p = Graph[v].First;
			while (p != null)
			{
				y = p.Value;
				if (discovered[y] == false)
				{
					parent[y] = v;
					processEdge(v, y);
					FindArticulation(y);
				}
				else if (!(processed[y]) || isDirected)
				{
					processEdge(v, y);
				}

			//	if (finished) return;
				p = p.Next;
			}
			
			processVertexLate(v);

			time += 1;
			exitTime[v] = time;
		
			processed[v] = true;	
		}
		
		private void processVertexEarly(int v)
		{
			reachableAncestor[v] = v;
		}

		private void processVertexLate(int v)
		{
			if (parent[v] == -1 && outDegree[v] > 1)
			{
				System.Console.WriteLine($"Vertex {v} is an articulation vertex");
			}

			if (reachableAncestor[v] == parent[v] && parent[v] != -1)
			{
				System.Console.WriteLine($"Vertex {parent[v]} is an articulation vertex");
			}

			if (reachableAncestor[v] == v && parent[v] != -1) 
			{
				System.Console.WriteLine($"Vertex {parent[v]} is an articulation vertex");
				
				// If the vertex isn't a leaf
				if (outDegree[v] > 0)
				{
					System.Console.WriteLine($"Vertex {v} is an articulation vertex");
				}
			}

			int timeV = entryTime[reachableAncestor[v]];
			int timeParentV = entryTime[reachableAncestor[parent[v]]];

			if (timeV < timeParentV)
				reachableAncestor[ parent[v] ] = reachableAncestor[v];
		}

		private void processEdge(int x, int y)
		{
			string type;

			type = getEdgeType(x, y);

			if (type == "tree") 
			{
				outDegree[x] += 1;
			}
			else if (type == "back" && y != parent[x])
			{
				if (entryTime[y] < entryTime[ reachableAncestor[x] ])
					reachableAncestor[x] = y;
			}
		}

		private string getEdgeType(int x, int y) {
			if (!discovered[y])
				return "tree";
			else if (discovered[y] && !processed[y])
				return "back";
			else
				return "unknown";
		}
	}

	public class Graph
	{
		private List<LinkedList<int>> g;
		private bool isDirected;

		public Graph(int size, bool directed = false)
		{
			g = Enumerable.Repeat(new LinkedList<int>(new int[] {}), size+1).ToList();
			isDirected = directed;
		}

		public Graph AddEdge(int x, int y)
		{
			LinkedListNode<int> v = new LinkedListNode<int>(y);
			g[x].AddLast(v);
			
			if (!isDirected)
			{
				v = new LinkedListNode<int>(x);
				g[y].AddLast(v);	
			}

			return this;
		}

		public List<LinkedList<int>> Build()
		{
			return g;
		}
	}

	class Program
	{
		public static void Main(string[] args)
		{
			System.Console.WriteLine("Hello Worlds");
		
			Graph g = new Graph(7);

			List<LinkedList<int>> adjacentyList = g.AddEdge(1, 2)
			 				       .AddEdge(1, 3)
							       .AddEdge(2, 4)
							       .AddEdge(3, 4)
							       .AddEdge(4, 5)
							       .AddEdge(4, 7)
							       .AddEdge(5, 6)
							       .AddEdge(7, 6)
							       .Build();
		
			DFS dfs = new DFS(ref adjacentyList);
			dfs.FindArticulation();			
		}
	}
}
