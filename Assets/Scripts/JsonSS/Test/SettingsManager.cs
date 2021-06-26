using UnityEngine;
using SoundSteppe.JsonSS;

public class SettingsManager : MonoBehaviour
{
	[Header("Settings")]
	[Saveable] public int LevelAmount = 7;
	[Saveable] public float SoundVolume = 0.73f;
	[Saveable] public string PlayerName = "";
	
	private void Start()
	{
		JsonSS.LoadGameObject("Settings", this);
	}
	
	private void OnApplicationQuit()
	{
		JsonSS.SaveGameObject("Settings", this);
	}
}
