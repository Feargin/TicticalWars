using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
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
        if ((Spawn.Instance.Enemyes.Count <= 0 || Spawn.Instance.Players.Count <= 0) && ChangeTurn.Instance.CountTurn >= 2) _timer += Time.deltaTime;

        if (_timer >= 2f)
        {
            if (Spawn.Instance.Enemyes.Count <= 0 && ChangeTurn.Instance.CountTurn >= 4)
            {
                UIManager.Instance.EnablePanel(_winPanel);
                ChangeTurn.Instance.CountTurn = 0;
                Camera.main.GetComponent<AudioSource>().Stop();
            }
            else if (Spawn.Instance.Players.Count <= 0)
            {
                UIManager.Instance.EnablePanel(_failPanel);
                ChangeTurn.Instance.CountTurn = 0;
                Camera.main.GetComponent<AudioSource>().Stop();
            }

            _timer = 0;
        }
    }

    public void Restart() => SceneManager.LoadScene(0);
    public void Exit() => Application.Quit();
}
