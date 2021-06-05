using System.Collections.Generic;
using Priority_Queue;
using UnityEngine;

/// <summary>
/// Implementation of Amit Patel's A* Pathfinding algorithm studies
/// https://www.redblobgames.com/pathfinding/a-star/introduction.html
/// </summary>
public static class AStar
{
	
	public static int GetWalkDistance(GridGraph graph, Node start, Node goal, bool makeEndWalkable = false)
	{
		return Search(graph, start, goal, makeEndWalkable).Count;
	}
	
	public static List<Node> SearchIgnoreObstacle(GridGraph graph, Node start, Node goal, bool makeEndWalkable = false)
	{
		Dictionary<Node, Node> came_from = new Dictionary<Node, Node>();
		Dictionary<Node, float> cost_so_far = new Dictionary<Node, float>();

		List<Node> path = new List<Node>();

		SimplePriorityQueue<Node> frontier = new SimplePriorityQueue<Node>();
		frontier.Enqueue(start, 0);

		came_from.Add(start, start);
		cost_so_far.Add(start, 0);

		Node current = new Node(0,0);
		while (frontier.Count > 0)
		{
			current = frontier.Dequeue();
			if (current == goal) break; // Early exit

			foreach (Node next in graph.Neighbours(current, true))
			{
				float new_cost = cost_so_far[current] + graph.Cost(next);
				if (!cost_so_far.ContainsKey(next) || new_cost < cost_so_far[next])
				{
					cost_so_far[next] = new_cost;
					came_from[next] = current;
					float priority = new_cost + Heuristic(next, goal);
					frontier.Enqueue(next, priority);
					next.Priority = new_cost;
				}
			}
		}

		while (current != start)
		{
			path.Add(current);
			current = came_from[current];
		}
        
		if(makeEndWalkable && !came_from.ContainsKey(goal))
		{
			path.Add(goal);
		}
        
		path.Reverse();

		return path;
	}
	
    /// <summary>
    /// Returns the best path as a List of Nodes
    /// </summary>
	public static List<Node> Search(GridGraph graph, Node start, Node goal, bool makeEndWalkable = false)
    {
        Dictionary<Node, Node> came_from = new Dictionary<Node, Node>();
        Dictionary<Node, float> cost_so_far = new Dictionary<Node, float>();

        List<Node> path = new List<Node>();

        SimplePriorityQueue<Node> frontier = new SimplePriorityQueue<Node>();
        frontier.Enqueue(start, 0);

        came_from.Add(start, start);
        cost_so_far.Add(start, 0);

        Node current = new Node(0,0);
        while (frontier.Count > 0)
        {
            current = frontier.Dequeue();
            if (current == goal) break; // Early exit

            foreach (Node next in graph.Neighbours(current))
            {
                float new_cost = cost_so_far[current] + graph.Cost(next);
                if (!cost_so_far.ContainsKey(next) || new_cost < cost_so_far[next])
                {
                    cost_so_far[next] = new_cost;
                    came_from[next] = current;
                    float priority = new_cost + Heuristic(next, goal);
                    frontier.Enqueue(next, priority);
                    next.Priority = new_cost;
                }
            }
        }

        while (current != start)
        {
            path.Add(current);
            current = came_from[current];
        }
        
	    if(makeEndWalkable && !came_from.ContainsKey(goal))
	    {
		    path.Add(goal);
	    }
        
        path.Reverse();

        return path;
    }
    
	public static List<Node> FindAllPassable(GridGraph graph, Node start, int distance, out List<Node> _obstacles)
	{
		Dictionary<Node, Node> came_from = new Dictionary<Node, Node>();
		Dictionary<Node, float> cost_so_far = new Dictionary<Node, float>();
		_obstacles = new List<Node>();

		List<Node> path = new List<Node>();

		SimplePriorityQueue<Node> frontier = new SimplePriorityQueue<Node>();
		frontier.Enqueue(start, 0);

		came_from.Add(start, start);
		cost_so_far.Add(start, 0);

		Node current = new Node(0,0);
		while (frontier.Count > 0)
		{
			current = frontier.Dequeue();
			if (distance == 0) break; // Early exit
			
			List<Node> obstacles;
			foreach (Node next in graph.Neighbours(current, out obstacles))
			{
				float new_cost = cost_so_far[current] + graph.Cost(next);
				
				if (!cost_so_far.ContainsKey(next) || new_cost < cost_so_far[next])
				{
					cost_so_far[next] = new_cost;
					float priority = new_cost;
					frontier.Enqueue(next, priority);
					next.Priority = new_cost;
				}
				
				if(new_cost <= distance + 1 && !path.Contains(current))
				{
					path.Add(current);
				}
				
				foreach(Node n2 in obstacles)
				{
					if(new_cost <= distance + 1 && !_obstacles.Contains(n2))
					{
						_obstacles.Add(n2);
					}
				}
			}
		}
		return path;
	}

    public static float Heuristic(Node a, Node b)
    {
        return Mathf.Abs(a.Position.x - b.Position.x) + Mathf.Abs(a.Position.y - b.Position.y);
    }
}