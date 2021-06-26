using UnityEngine;
using SoundSteppe.JsonSS;

public class SettingsManager : SaveableMono
{
	[Header("Settings")]
	[Saveable] public int LevelAmount = 7;
	[Saveable] public float SoundVolume = 0.73f;
	[Saveable] public string PlayerName = "";
	
	private void Start()
	{
		string json = JsonSS.LoadObject("Settings");
		if(string.IsNullOrEmpty(json) == false)
		{
			this.LoadGameObject(json);
		}
	}
	
	private void OnApplicationQuit()
	{
		JsonSS.SaveObject("Settings", this);
	}
}
