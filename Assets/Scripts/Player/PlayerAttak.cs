using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttak : MonoBehaviour
{
	private SoundController soundController;
	
	private PlayerSelector _playerSelect;
	
	public void Init(SoundController _soundController)
	{
		soundController = _soundController;
	}
    
    void Start()
    {
        _playerSelect = GetComponent<PlayerSelector>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.GetComponent<Enemy>())
        {
	        soundController.SetClip(0);
            col.transform.GetComponent<Enemy>().DealDamage(100);
        }
    }
}
