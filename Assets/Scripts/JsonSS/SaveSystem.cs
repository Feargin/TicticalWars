using UnityEngine.InputSystem;
using SoundSteppe.JsonSS;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
	//public BinnarySS binnarySaver;
	public FabricGO entityFabric;
	public Entity2[] entities;
	
	private void Update()
	{
		if(Keyboard.current.sKey.wasPressedThisFrame)
		{
			//binnarySaver.Save("Entities", entities[0]);
			entityFabric.SaveObjects("Entities", entities);
		}
		
		if(Keyboard.current.lKey.wasPressedThisFrame)
		{
			//entities[0] = binnarySaver.Load("Entities") as Entity2;
			entityFabric.LoadObjects("Entities");
		}
	}
}
