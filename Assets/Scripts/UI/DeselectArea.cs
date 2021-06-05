using UnityEngine.EventSystems;
using UnityEngine;

public class DeselectArea : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private PlayerSelector _playerSelector;
	[SerializeField] private Spawn _spawn;
	
	public void OnPointerClick(PointerEventData eventData)
	{
		_playerSelector.Deselect();
		_spawn.ClearSpawnCoord();
	}
}
