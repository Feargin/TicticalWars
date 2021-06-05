using UnityEngine;

public class AI_FindTarget : AI
{
	public Entity Target;
	public Entity.Type PriorityTarget;
	
	private Spawn _spawn;
	protected Spawn Spawn
	{
		get 
		{
			if(_spawn == null)
				_spawn = Spawn.Instance;
			return _spawn;
		}
	}
    
	public override void BeginState()
	{
		//print(111);
		SelectPlayerTarget();
		ExitState();
	}
	
	private void SelectPlayerTarget()
	{
		Target = null;
		foreach(var p in Spawn.Players)
		{
			if(p != null && Target != null)
			{
				if((p.transform.position - transform.position).magnitude 
					< (Target.transform.position - transform.position).magnitude
					&& p.type != PriorityTarget
					|| p.type == PriorityTarget)
				{
					Target = p.GetComponent<Entity>();
				}
			}
			else
			{
				Target = p.GetComponent<Entity>();
			}
		}
	}
}
