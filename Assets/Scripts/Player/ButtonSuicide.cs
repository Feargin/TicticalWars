using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSuicide : MonoBehaviour
{
    [SerializeField] private Button _suicideButton;
    private Entity entitySecelt;
    protected void OnEnable()
    {
        PlayerSelector.OnPlayerSelect += SetButtonActive;
        PlayerSelector.OnPlayerDeselect += UnsetButtonActive;
        Movement.EndMove += SetMoveEnd;
        ChangeTurn.TheNextTurn += NextTurn;
    }

    protected void OnDisable()
    {
        PlayerSelector.OnPlayerSelect -= SetButtonActive;
        PlayerSelector.OnPlayerDeselect -= UnsetButtonActive;
        Movement.EndMove -= SetMoveEnd;
        ChangeTurn.TheNextTurn += NextTurn;
    }

    private void NextTurn(bool b)
	{
		if(_suicideButton != null)
        	_suicideButton.gameObject.SetActive(false);
    }

    private void Start()
    {
        _suicideButton.gameObject.SetActive(false);
    }

    private void SetMoveEnd(Entity e)
    {
        if (entitySecelt is { } && e == entitySecelt) SetButtonActive(entitySecelt);
    }

    private void SetButtonActive(Entity e)
    {
        entitySecelt = e;
        if(e.transform.GetComponent<Suicide>()._ready) _suicideButton.gameObject.SetActive(true);
        else _suicideButton.gameObject.SetActive(false);;
    }
    private void UnsetButtonActive(Entity e)
    {
        entitySecelt = null;
        _suicideButton.gameObject.SetActive(false);
    }

    public void SuicideSelectEntity()
    {
        entitySecelt.DealDamage(1000);
    }
}
