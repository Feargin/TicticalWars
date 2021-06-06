using UnityEngine;
using Zenject;

public class AI_FindTarget : AI
{
	public Entity Target;
	public Entity.Type PriorityTarget;
	
	[Inject] private Spawn spawn;
    
	public override void BeginState()
	{
		SelectPlayerTarget();
		ExitState();
	}
	
	private void SelectPlayerTarget()
	{
		Target = null;
		foreach(var p in spawn.Players)
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
