using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerAttak : MonoBehaviour
{
	[Inject] private SoundController soundController;
	private PlayerSelector _playerSelect;
    
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
