using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Attak : MonoBehaviour
{
	public bool _needVisiable = false;
	public int _damage = 1;
	public List<Vector3Int> RangeAttak;
	private TweenCallback _onAttackEnd;
	[SerializeField] private Projectile _projectile;
	
	public void Attack(Entity entity, TweenCallback onAttackEnd = null)
	{
		_onAttackEnd = onAttackEnd;
		if(_projectile != null)
		{
			Projectile proj = Instantiate(_projectile, transform.position, Quaternion.identity);
			proj.Init(entity.transform.position);
			proj.OnExplode += () => { entity.DealDamage(_damage); _onAttackEnd?.Invoke();};
		}
		else
		{
			entity.DealDamage(_damage);
		}

		if(gameObject.GetComponent<Entity>().TypeEnemy != 0) SoundController.Instance.SetClip(1);
		else SoundController.Instance.SetClip(4);
	}
	
	public bool CanAttack(Entity entity)
	{
		GridGraph grid = entity.movement.pathfinding.map.nodemap;
		
		//_needVisiable
		
		return CheckInFireRadius(entity, grid);
	}
	
	//public bool 
    
	public bool CheckInFireRadius(Entity entity, GridGraph grid)
	{
		foreach(Vector3Int attackPos in RangeAttak)
		{
			Node node = grid.WorldToNode(transform.position + attackPos);
			if(node == grid.WorldToNode(entity.transform.position))
			{
				return true;
			}
		}
		return false;
	}
	
	public Node FindClosestNode(Entity entity)
	{
		GridGraph grid = entity.movement.pathfinding.map.nodemap;
		Node main = grid.WorldToNode(transform.position);
		Node entityNode = grid.WorldToNode(entity.transform.position);
		int closestDistance = AStar.GetWalkDistance(grid, main, entityNode, true);
		Node closest = null;
		foreach(Vector3Int attackPos in RangeAttak)
		{
			Node node = grid.WorldToNode(entity.transform.position + attackPos);
			if(node == null)
				continue;
			int distance = AStar.GetWalkDistance(grid, main, node, true);
			if(distance < closestDistance)
			{
				closestDistance = distance;
				closest = node;
			}
		}
		if(closest == null)
		{
			Vector2Int v2i = new Vector2Int();
			foreach(var n in grid.Neighbours(entityNode))
			{
				v2i.x = (int)entityNode.Position.x;
				v2i.y = (int)entityNode.Position.y;
				if(grid.Passable(v2i))
				{
					closest = n;
					break;
				}
			}
		}
		return closest;
	}
}
