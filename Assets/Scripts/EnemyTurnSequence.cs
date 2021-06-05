using System.Collections;
using UnityEngine;

public class EnemyTurnSequence : MonoBehaviour
{
	public Spawn _spawn;
	private Coroutine _turnProcess;
	
	private void OnEnable() => ChangeTurn.TheNextTurn += TurnBegin;
	private void OnDisable() => ChangeTurn.TheNextTurn -= TurnBegin;
	
	public static event System.Action OnNpcEndTurn;
	
	private void TurnBegin(bool npc_turn)
	{
		if(npc_turn)
		{
			if(_turnProcess != null)
				StopCoroutine(_turnProcess);
			_turnProcess = StartCoroutine(TurnProcess());
		}
	}
	
	private IEnumerator TurnProcess()
	{
		for(int i = 0; i < _spawn.Enemyes.Count; i++)
		{
			_spawn.Enemyes[i].EnableAI();
			yield return new WaitUntil(() =>
			{
				return _spawn.Enemyes.Count > i && _spawn.Enemyes[i].TurnPassed == true;
			});
		}
		OnNpcEndTurn?.Invoke();
	}
}
