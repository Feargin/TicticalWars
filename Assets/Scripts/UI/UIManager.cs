using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public sealed class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _initialPanel;
    private GameObject _lastPanel;
    public static UIManager Instance;
    
    

    private void Awake() 
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        EnablePanel(_initialPanel);
        Camera.main.GetComponent<AudioSource>().Play();
    }

    public void EnablePanel(GameObject panel)
    {
        if (_lastPanel != null)
            _lastPanel.SetActive(false);
        _lastPanel = panel;
        _lastPanel.SetActive(true);
    }
}
