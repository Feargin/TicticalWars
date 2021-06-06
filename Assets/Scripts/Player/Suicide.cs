using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Suicide : MonoBehaviour
{
    private bool _atteked;
    public bool _ready;

    
    private void OnEnable()
    {
        Movement.EndMove += CheckSuicide;
        ChangeTurn.TheNextTurn += ChangeAttak;
    }

    private void OnDisable()
    {
        Movement.EndMove -= CheckSuicide;
        ChangeTurn.TheNextTurn -= ChangeAttak;
    }

    private void ChangeAttak(bool b)
    {
        _atteked = false;
        _ready = false;
    }

    private void CheckSuicide(Entity e)
    {
        if (!_atteked && e == GetComponent<Entity>()) _ready = true;
    }
}
