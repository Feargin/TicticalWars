using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeTurn : MonoBehaviour
{
	[Zenject.Inject, HideInInspector] public SoundController soundController;
	
	[SerializeField] private int [] _numTurn;
	[SerializeField] private int [] _countEnemy;
	[SerializeField] private int [] _indexEnemy;

	[SerializeField] private GameObject _playerController;
	[SerializeField] private Button _nextTurn;
	[SerializeField] private AudioSource GameGontrollerAudio;
	[SerializeField] private TMP_Text _countTurnText;
	private float _timer;
    public int CountTurn = 0;
    public int ToSpawn;

	[Zenject.Inject] public Spawn spawn;
	
    #region Events
	public static event System.Action<bool> TheNextTurn;
	#endregion

	private void Start()
	{
		ToSpawn = _numTurn[0];
		_countTurnText.text = "" + ToSpawn;
	}

	protected void OnEnable()
	{
		EnemyTurnSequence.OnNpcEndTurn += FinishEnemyTurn;
	}
	
	protected void OnDisable()
	{
		EnemyTurnSequence.OnNpcEndTurn -= FinishEnemyTurn;
	}

    public void NextTurn()
    {
	    _nextTurn.interactable = false;
	    _nextTurn.GetComponent<Animator>().enabled = false;
	    _playerController.SetActive(false);
	    GameGontrollerAudio.Play();
        //Spawn.Instance.PlayerControler.GetComponent<PlayerMovement>().enabled = false;
        TheNextTurn?.Invoke(true);
    }
    
    public void FinishEnemyTurn()
    {
	    _nextTurn.interactable = true;
	    _nextTurn.GetComponent<Animator>().enabled = true;
	    _playerController.SetActive(true);
	    //Spawn.Instance.PlayerControler.GetComponent<PlayerMovement>().enabled = true;
	    TheNextTurn?.Invoke(false);
	    CountTurn += 1;
	    if (ToSpawn != 0)
	    {
		    ToSpawn -= 1;
		    _countTurnText.text = "" + ToSpawn;
	    }
	    else _countTurnText.text = "Already arrived";
	    GameGontrollerAudio.Stop();
	    
	    foreach (var v in _numTurn)
	    {
		    if (v == CountTurn)
		    {
			    soundController.SetClip(2);
			    for (int i = 0; i < _indexEnemy.Length; i++)
			    {
				    spawn.Creator(_indexEnemy[i], _countEnemy[i]);
			    }
			    
		    }
	    }
    }

    void Update()
    {
        
    }
}
