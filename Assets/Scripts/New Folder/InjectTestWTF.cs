using UnityEngine.InputSystem;
using UnityEngine;
using Zenject;

public class InjectTestWTF : MonoBehaviour
{
	[Inject]
	public IService _singleton;
	
	//public void Construct(IService singleton)
	//{
	//	_singleton = singleton;
	//}
    
	private void Update()
	{
		if(Keyboard.current.fKey.wasPressedThisFrame)
		{
			_singleton.DoThis();
		}
	}
}
