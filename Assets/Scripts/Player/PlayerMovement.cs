using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[Header("------------- Dependencies --------------")]
	[SerializeField] private LayerMask _walkableMask;
	
	private Entity _selectedPlayer;
	[Zenject.Inject] private MoveHelper _moveHelper;
	
	private Camera _main;
	
	private void OnEnable() 
	{
		PlayerSelector.OnPlayerSelect += OnPlayerSelect;
		PlayerSelector.OnPlayerDeselect += OnPlayerDeselect;
	}
	
	private void OnDisable() 
	{
		PlayerSelector.OnPlayerSelect -= OnPlayerSelect;
		PlayerSelector.OnPlayerDeselect -= OnPlayerDeselect;
	}
	
	private void Start()
	{
		_main = Camera.main;
	}
	
	private void OnPlayerSelect(Entity player)
	{
		_selectedPlayer = player;
	}
	
	private void OnPlayerDeselect(Entity player)
	{
		_selectedPlayer = null;
	}
	
	public void TryMove()
	{
		if(_selectedPlayer != null)
		{
			SelectTile();
		}
	}
    
	private void SelectTile()
	{
		if(Physics.Raycast(_main.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit, float.PositiveInfinity, _walkableMask, QueryTriggerInteraction.Ignore))
		{
			_selectedPlayer.movement.MoveTo(hit.transform.position);
			_moveHelper.UnlockSelect();
		}
	}
}
