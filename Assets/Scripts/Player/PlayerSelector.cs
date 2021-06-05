using UnityEngine;

public class PlayerSelector : Singleton<PlayerSelector>
{
	[Header("------------- Info ----------------------")]
	[ReadOnly] public Entity SelectedPlayer;
	
	[Header("------------- Dependencies --------------")]
	[SerializeField] private LayerMask _playerMask;
	[SerializeField] private LayerMask _walkableMask;
	[SerializeField] private PlayerMovement _movement;
	[SerializeField] private Tile _selectedTile;

	#region Events
	public static event System.Action<Entity> OnPlayerSelect;
	public static event System.Action<Entity> OnPlayerDeselect;
	#endregion
	
	private Camera _main;
	
	private void Start()
	{
		_main = Camera.main;
	}
	
    private void Update()
    {
	    if(Input.GetMouseButtonDown(0))
	    {
	    	if(!SelectPlayerEntity())
	    	{
	    		_movement.TryMove();
	    	}
	    }
	    RaycastHit hit;

	    if (_selectedTile != null) _selectedTile.Selected = false;
		    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.PositiveInfinity,
			    _walkableMask, QueryTriggerInteraction.Ignore))
		    {
			    
			    if (hit.transform.TryGetComponent(out Tile tile) && tile.EntityIn != null)
			    {
				    _selectedTile = tile;
				    _selectedTile.Selected = true;
				    /*Map.Instance.ReloadSelectTiles();
				    hit.transform.GetComponent<Tile>().Selected = true;*/
			    }
		    
	    }

    }

	private bool SelectPlayerEntity()
	{
		if (SelectedPlayer != null)
		{
			SelectedPlayer.selectimage.SetActive(false);
		}

		if (Physics.Raycast(_main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.PositiveInfinity, _playerMask, QueryTriggerInteraction.Ignore))
		{
			if (hit.transform.gameObject.GetComponent<PlayerEntity>().type == Entity.Type.Godzilla)
			{
				SelectedPlayer = hit.transform.gameObject.GetComponent<PlayerEntity>();
				if (SelectedPlayer != null)
				{
					SelectedPlayer.selectimage.SetActive(true);
					OnPlayerSelect?.Invoke(SelectedPlayer);
					return true;
				}
			}
		}
		return false;
	}

	public void Deselect()
	{
		if(SelectedPlayer != null)
		{
			SelectedPlayer = null;
			OnPlayerDeselect?.Invoke(SelectedPlayer);
		}
	}
}
