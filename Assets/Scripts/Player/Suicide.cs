using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Suicide : MonoBehaviour
{
    private bool _atteked;
    public bool _ready;

    private void Start()
    {
        
    }
    private void OnEnable()
    {
        Movement.EndMove += CheckSuicide;
        PlayerAttak.AttakPlayer += SetAttak;
        ChangeTurn.TheNextTurn += ChangeAttak;
    }

    private void OnDisable()
    {
        Movement.EndMove -= CheckSuicide;
        PlayerAttak.AttakPlayer -= SetAttak;
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
    private void SetAttak()
    {
        _atteked = true;
    }
}
