using UnityEngine;

public class AI_MovingFly : AI
{
	public AI_FindTarget AI_Target;
	public CollideAttack Attack;
	public Movement Movement;
	private Vector3 _basePosition;
	private bool IsDealDamage = false;
	
	protected void OnEnable()
	{
		Attack.OnDealDamage += OnDealDamage;
	}
	
	protected void OnDisable()
	{
		Attack.OnDealDamage -= OnDealDamage;
	}
	
	private void OnDealDamage()
	{
		IsDealDamage = true;
		Attack.Disable();
		//print("Damage!");
	}
	
	public override void BeginState()
	{
		if (AI_Target.Target != null)
		{
			AttackArea();
		}
		else
		{
			ExitState();
		}
	}
	
	public override void ExitState()
	{
		IsDealDamage = false;
		Owner._immortal = false;
		base.ExitState();
	}
	
	Vector3 targetPos;
	private void AttackArea()
	{	
		_basePosition = transform.position;
		Owner._immortal = true;
		Attack.Enable();
		Movement.MoveTo(AI_Target.Target.transform.position, true, OnCompleteAttack);
	}
	
	private void OnCompleteAttack()
	{
		if(IsDealDamage)
		{
			//print("goBack");
			Owner._currentActionPoints = Owner.MaxActionPoints;
			Movement.MoveTo(_basePosition, true, OnCompleteRunAway);
		}
		else
		{
			ExitState();
		}
	}
	
	private void OnCompleteRunAway()
	{
		//print("Awayy");
		ExitState();
	}
}
