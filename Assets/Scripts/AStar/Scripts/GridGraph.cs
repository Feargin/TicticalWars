using System.Collections.Generic;
using UnityEngine;

public class GridGraph
{
    public int Width;
	public int Height;
	public float GridSize;
    
	private Map _map;
    public Node[,] Grid;

	public GridGraph(int w, int h, float gridSize, Map map)
    {
        Width = w;
	    Height = h;
	    GridSize = gridSize;
	    _map = map;
	    
        Grid = new Node[w, h];

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                Grid[x, y] = new Node(x, y);
            }
        }
    }

    /// <summary>
    /// Checks whether the neighbouring Node is within the grid bounds or not
    /// </summary>
    public bool InBounds(Vector2 v)
    {
        if (v.x >= 0 && v.x < this.Width &&
            v.y >= 0 && v.y < this.Height)
            return true;
        else
            return false;
    }
    
	public bool InBounds(Vector3 v)
	{
		if (v.x >= 0 && v.x < this.Width &&
			v.z >= 0 && v.z < this.Height)
			return true;
		else
			return false;
	}

    /// <summary>
    /// Checks whether the neighbouring Node is a wall or not
    /// </summary>
	public bool Passable(Vector2Int pos, bool withEntity = false)
	{
		return _map.Passable(pos, withEntity);
	}

    /// <summary>
    /// Returns a List of neighbouring Nodes
    /// </summary>
	public List<Node> Neighbours(Node n, out List<Node> obstacles, int distance = 1)
    {
        List<Node> results = new List<Node>();
	    obstacles = new List<Node>();

        List<Vector2> directions = new List<Vector2>()
        {
            new Vector2( -distance, 0 ), // left
	        //new Vector2(-1, 1 ),  // top-left, comment it out for 4-direction movement
            new Vector2( 0, distance ),  // top
	        //new Vector2( 1, 1 ),  // top-right, comment it out for 4-direction movement
            new Vector2( distance, 0 ),  // right
	        //new Vector2( 1, -1 ), // bottom-right, comment it out for 4-direction movement
            new Vector2( 0, -distance ), // bottom
	        //new Vector2( -1, -1 ) // bottom-left, comment it out for 4-direction movement
        };

        foreach (Vector2 v in directions)
        {
	        Vector2 newVector = v + n.Position;
	        if (InBounds(newVector) && Passable(WorldToIndex(newVector)))
            {
		        results.Add(Grid[Mathf.RoundToInt(newVector.x), Mathf.RoundToInt(newVector.y)]);
            }
	        if (InBounds(newVector) && Passable(WorldToIndex(newVector), true))
	        {
	        	if(!obstacles.Contains(Grid[Mathf.RoundToInt(newVector.x), Mathf.RoundToInt(newVector.y)]))
		        	obstacles.Add(Grid[Mathf.RoundToInt(newVector.x), Mathf.RoundToInt(newVector.y)]);
	        }
        }

        return results;
    }
    
	public List<Node> Neighbours(Node n, bool ignoreObstacle = false)
	{
		List<Node> results = new List<Node>();

		List<Vector2> directions = new List<Vector2>()
		{
			new Vector2( -1, 0 ), // left
			//new Vector2(-1, 1 ),  // top-left, comment it out for 4-direction movement
			new Vector2( 0, 1 ),  // top
			//new Vector2( 1, 1 ),  // top-right, comment it out for 4-direction movement
			new Vector2( 1, 0 ),  // right
			//new Vector2( 1, -1 ), // bottom-right, comment it out for 4-direction movement
			new Vector2( 0, -1 ), // bottom
			//new Vector2( -1, -1 ) // bottom-left, comment it out for 4-direction movement
        };

		foreach (Vector2 v in directions)
		{
			Vector2 newVector = v + n.Position;
			if (InBounds(newVector) && (ignoreObstacle || Passable(WorldToIndex(newVector), false)))
			{
				results.Add(Grid[Mathf.RoundToInt(newVector.x), Mathf.RoundToInt(newVector.y)]);
			}
		}

		return results;
	}
    
	public Vector2Int WorldToLocal(Vector3 pos)
	{
		int newX = Mathf.RoundToInt((pos.x / GridSize) * GridSize);
		int newY = Mathf.RoundToInt((pos.z / GridSize) * GridSize);
		Vector2Int local = new Vector2Int(newX, newY);
		return local;
	}
	
	public Vector2Int WorldToIndex(Vector3 pos)
	{
		int newX = Mathf.RoundToInt(pos.x / GridSize);
		int newY = Mathf.RoundToInt(pos.z / GridSize);
		Vector2Int local = new Vector2Int(newX, newY);
		return local;
	}
	
	public Vector2Int WorldToIndex(Vector2 pos)
	{
		int newX = Mathf.RoundToInt(pos.x / GridSize);
		int newY = Mathf.RoundToInt(pos.y / GridSize);
		Vector2Int local = new Vector2Int(newX, newY);
		return local;
	}
	
	public Node WorldToNode(Vector3 pos)
	{
		Vector2Int index = WorldToIndex(pos);
		if(index.x < 0 || index.x > Grid.GetLength(0) - 1) return null;
		if(index.y < 0 || index.y > Grid.GetLength(1) - 1) return null;
		
		return Grid[index.x, index.y];
	}

    public int Cost(Node b)
    {
        // If Node 'b' is a Forest return 2, otherwise 1
        //if (Forests.Contains(b.Position)) return 2;
	    //else return 1;
	    return 1;
    }
}
