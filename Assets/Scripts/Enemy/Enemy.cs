using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
	public GameObject AI_Behavoiur;
	public Attak attack;
	public CollideAttack collideAttack;
	public bool TurnPassed;
	
	public void Init(Spawn spawn, Map map, SoundController audioManager, SpawnEgg spawnEgg)
	{
		base.Init(map, spawn, audioManager, spawnEgg);
		AI_FindTarget ai = AI_Behavoiur.GetComponentInChildren<AI_FindTarget>();
		if(attack != null)
			attack.soundController = audioManager;
		if(collideAttack != null)
			collideAttack.soundController = audioManager;
		ai.Init(spawn);
	}
	
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
