using UnityEngine.InputSystem;
using SoundSteppe.JsonSS;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
	public FabricGO entityFabric;
	public Entity2[] entities;
	
	private void Update()
	{
		if(Keyboard.current.sKey.wasPressedThisFrame)
		{
			entityFabric.SaveObjects("Entities", entities);
		}
		
		if(Keyboard.current.lKey.wasPressedThisFrame)
		{
			entityFabric.LoadObjects("Entities");
		}
	}
}
