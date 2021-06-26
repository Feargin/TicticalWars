using UnityEngine;
using SoundSteppe.JsonSS;

public class SettingsManager : SaveableMono
{
	public JsonSS jsonSS;
	
	[Header("Settings")]
	[Saveable] public int LevelAmount = 7;
	[Saveable] public float SoundVolume = 0.73f;
	[Saveable] public string PlayerName = "";
	
	private void Start()
	{
		string json = jsonSS.LoadObject("Settings");
		if(string.IsNullOrEmpty(json) == false)
		{
			this.LoadGameObject(json);
		}
	}
	
	private void OnApplicationQuit()
	{
		jsonSS.SaveObject("Settings", this);
	}
}
