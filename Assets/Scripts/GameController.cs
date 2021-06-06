using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	[Zenject.Inject, HideInInspector] public Spawn spawn;
	[Zenject.Inject, HideInInspector] public ChangeTurn turnSystem;
	
	[SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _failPanel;
    private float _timer = 0;
    private void Start()
    {
        _winPanel.SetActive(false);
        _failPanel.SetActive(false);
    }

    
    private void FixedUpdate()
    {
	    if ((spawn.Enemyes.Count <= 0 || spawn.Players.Count <= 0) && turnSystem.CountTurn >= 2) _timer += Time.deltaTime;

        if (_timer >= 2f)
        {
	        if (spawn.Enemyes.Count <= 0 && turnSystem.CountTurn >= 4)
            {
                UIManager.Instance.EnablePanel(_winPanel);
                turnSystem.CountTurn = 0;
                Camera.main.GetComponent<AudioSource>().Stop();
            }
	        else if (spawn.Players.Count <= 0)
            {
                UIManager.Instance.EnablePanel(_failPanel);
                turnSystem.CountTurn = 0;
                Camera.main.GetComponent<AudioSource>().Stop();
            }

            _timer = 0;
        }
    }

    public void Restart() => SceneManager.LoadScene(0);
    public void Exit() => Application.Quit();
}
