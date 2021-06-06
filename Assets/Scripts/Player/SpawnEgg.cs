using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnEgg : MonoBehaviour
{
	[Inject, HideInInspector] public Map map;
	[Inject, HideInInspector] public Spawn spawn;
	[Inject, HideInInspector] public SpawnEgg spawnEgg;
	[Inject, HideInInspector] public SoundController soundController;
	[Inject] private IEntityFactory _entityFactory;
	
	[SerializeField] private Egg _egg;
    [SerializeField] private int _hpNewKaujy;
    [SerializeField] private GameObject panelEggInfo;
    private bool eggInfoReg = false;
    
    public void Spawner(Vector3 position)
	{
		Entity egg = _entityFactory.Create(_egg, position);
		spawn.Players.Add(egg as PlayerEntity);
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
