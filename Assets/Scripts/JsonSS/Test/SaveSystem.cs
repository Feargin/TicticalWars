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
			// Save gameObject fields to file
			JsonSS.SaveGameObject("ent", entity);
			
			// Save array of gameObjects to file
			JsonSS.SaveGameObjects("params", entities);
			
			// Save gameObject fields to file (equil to JsonSS.SaveGameObject())
			entityFabric.SaveObject("Entity", entity);
			
			// Save array of gameObjects to file (equil to JsonSS.SaveGameObjects())
			entityFabric.SaveObjects("Entities", entities);
		}
		
		if(Keyboard.current.lKey.wasPressedThisFrame)
		{
			// Load gameObject fields from file
			JsonSS.LoadGameObject("ent", entity);
			
			// Load gameObjects fields from file
			JsonSS.LoadGameObjects("params", entities);
			
			// Instantiate gameObject and load fields from file
			entityFabric.LoadObject("Entity");
			
			// Instantiate array of gameObject and load fields from file
			entityFabric.LoadObjects("Entities");
		}
	}
}
