using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cheats : MonoBehaviour
{
	private void Update()
    {
	    if(Keyboard.current.rKey.IsPressed())
	    {
	    	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	    }
    }
}
