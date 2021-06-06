using System.Collections.Generic;
using UnityEngine;

public class PF_AStar : MonoBehaviour
{
	private Map _map;

	[HideInInspector]
	public Map map
	{
		get
		{
			if (_map == null) _map = Map.Instance;
			return _map;
		}
	}
	
	private void Start()
	{
		_map = Map.Instance;
	}
	
	public List<Node> FindPathFly(Vector3 _from, Vector3 _to)
	{
		if(!map.nodemap.InBounds(_to) || !map.nodemap.InBounds(_from))
			return null;
		
		Node nodeFrom = map.nodemap.WorldToNode(_from);
		Node nodeTo = map.nodemap.WorldToNode(_to);
		List<Node> path = AStar.SearchIgnoreObstacle(map.nodemap, nodeFrom, nodeTo, false);
		return path;
	}
	
	public List<Node> FindPath(Vector3 _from, Vector3 _to, bool makeEndWalkable = false)
	{
		if(!map.nodemap.InBounds(_to) || !map.nodemap.InBounds(_from))
			return null;
		Node nodeFrom = map.nodemap.WorldToNode(_from);
		Node nodeTo = map.nodemap.WorldToNode(_to);
		List<Node> path = AStar.Search(map.nodemap, nodeFrom, nodeTo, makeEndWalkable);
		return path;
	}
	
	public List<Node> FindPossibleMovement(Vector3 _from, int distance, out List<Node> _obstacles)
	{
		Vector2Int from = map.nodemap.WorldToIndex(_from);
		Node nodeFrom = map.nodemap.Grid[from.x, from.y];
		List<Node> path = AStar.FindAllPassable(map.nodemap, nodeFrom, distance, out List<Node> obstacles);
		_obstacles = obstacles;
		return path;
	} 
}
