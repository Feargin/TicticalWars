using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttak : MonoBehaviour
{
    private PlayerSelector _playerSelect;
    public static event System.Action AttakPlayer;
    void Start()
    {
        _playerSelect = GetComponent<PlayerSelector>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.GetComponent<Enemy>())
        {
            SoundController.Instance.SetClip(0);
            col.transform.GetComponent<Enemy>().DealDamage(100);
        }
    }
}
