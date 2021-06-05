using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
	public GameObject AI_Behavoiur;
	public bool TurnPassed;
	
	public void EnableAI()
	{
		TurnPassed = false;
		AI_Behavoiur.SetActive(true);
	}
	
	public void DisableAI()
	{
		TurnPassed = true;
		AI_Behavoiur.SetActive(false);
	}
}
