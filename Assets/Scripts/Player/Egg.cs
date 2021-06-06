using UnityEngine;

public class Egg : PlayerEntity
{
	public int _liveCount = 0;
    public int _CountTurn = 0;
    private void OnEnable() => ChangeTurn.TheNextTurn += StartCountLive;
    private void OnDisable() => ChangeTurn.TheNextTurn -= StartCountLive;
    
    private void StartCountLive(bool go)
    {
        _liveCount += 1;
        if (_liveCount >= _CountTurn)
        {
            spawnEgg.SpawnEpickKaujy(transform.position);
	        spawn.Players.Remove(this);
            Destroy(gameObject);
        }
    }
}