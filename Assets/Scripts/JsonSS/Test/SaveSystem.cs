using UnityEngine.InputSystem;
using SoundSteppe.JsonSS;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
	public FabricGO entityFabric;
	public MonoBehaviour entity;
	public MonoBehaviour[] entities;
	
	private void Update()
	{
		if(Keyboard.current.sKey.wasPressedThisFrame)
		{
			JsonSS.SaveGameObject("ent", entity);
			//entityFabric.SaveObjects("Entities", entities);
		}
		
		if(Keyboard.current.lKey.wasPressedThisFrame)
		{
			JsonSS.LoadGameObject("ent", entity);
			//entityFabric.LoadObjects("Entities");
		}
	}
}
