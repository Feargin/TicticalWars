using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEgg : MonoBehaviour
{
	[Zenject.Inject, HideInInspector] public Map map;
	[Zenject.Inject, HideInInspector] public Spawn spawn;
	[Zenject.Inject, HideInInspector] public SpawnEgg spawnEgg;
	[Zenject.Inject, HideInInspector] public SoundController soundController;
	
	[SerializeField] private Egg _egg;
    [SerializeField] private int _hpNewKaujy;
    [SerializeField] private GameObject panelEggInfo;
    private bool eggInfoReg = false;
    
    public void Spawner(Vector3 position)
    {
	    var egg = Instantiate(_egg, position, Quaternion.identity);
	    egg.Init(map, spawn, soundController, spawnEgg);
	    spawn.Players.Add(egg);
        if (!eggInfoReg)
        {
            Time.timeScale = 0;
            panelEggInfo.SetActive(true);
        }
        eggInfoReg = true; 
    }

    public void SetTimeScale()
    {
        Time.timeScale = 1;
    }

    public void SpawnEpickKaujy(Vector3 position)
    {
	    var kaujy = Instantiate(spawn.EpicKaujy, position, Quaternion.identity);
        kaujy.name = "Super Kaiju";
	    spawn.Players.Add(kaujy);
        soundController.SetClip(3);
        kaujy.GetComponent<PlayerEntity>()._health = _hpNewKaujy;
    }

    
}
