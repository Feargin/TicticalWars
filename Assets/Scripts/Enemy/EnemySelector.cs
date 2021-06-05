using UnityEngine;

public class EnemySelector : MonoBehaviour
{
    /*[Header("------------- Info ----------------------")]
    [ReadOnly] public Enemy SelectedEnemy;
	
    [Header("------------- Dependencies --------------")]
    [SerializeField] private LayerMask _playerMask;
    private Enemy _selectedEnemy;
	
    #region Events
    public static event System.Action<Enemy> OnEnemySelect;
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
            SelectEnemy();
        }
    }
    
    private void SelectEnemy()
    {
        if(Physics.Raycast(_main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.PositiveInfinity, _playerMask))
        {
            SelectedEnemy = hit.transform.gameObject.GetComponent<Enemy>();
            OnEnemySelect?.Invoke(SelectedEnemy);
        }
    }
    
    private void OnEnable() => OnEnemySelect += OnEnemySelect;
    private void OnDisable() => OnEnemySelect -= OnEnemySelect;
    
	
    private void OnEnemySelect(Enemy enemy)
    {
        _selectedEnemy = enemy;
    }*/
}