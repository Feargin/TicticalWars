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
            SpawnEgg.Instance.SpawnEpickKaujy(transform.position);
	        Spawn.Instance.Players.Remove(this);
            Destroy(gameObject);
        }
    }
}