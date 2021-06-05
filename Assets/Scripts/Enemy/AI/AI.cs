using UnityEngine;

public class AI : MonoBehaviour
{
	public bool InitialState = false;
	public bool FinalState = false;
	public AI NextState;
	public Enemy Owner;
	
	private void OnEnable()
	{
		if(InitialState)
		{
			BeginState();
		}
	}
	
	public virtual void BeginState()
	{
		
	}
	
	public virtual void ExitState()
	{
		//print("ex:" + this);
		if(NextState != null)
		{
			NextState.BeginState();
		}
		
		if(FinalState == true || NextState == null)
		{
			Owner.DisableAI();
		}
	}
	private void OnDisable()
	{
		if(FinalState)
		{
			Owner.DisableAI();
		}
	}
}
