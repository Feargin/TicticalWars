using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
	[Header("------------------ Настройки моба -----------------")]
	public int _health;
	public int MaxActionPoints = 4;
	public int _currentActionPoints;
	public Type type;
	public GameObject selectimage;
	public int TypeEnemy;
	public bool _immortal = false;
	[SerializeField] private int _currentHealth;
	[Space]
	[Header("--------------------- Системные --------------------")]
	[HideInInspector] public Movement movement;
	[SerializeField] private Image _healtBar;
	[SerializeField] private GameObject _vfx;
	public bool _isDead = false;
	
	private void OnEnable() => ChangeTurn.TheNextTurn += ResetActionPoints;
	private void OnDisable() => ChangeTurn.TheNextTurn -= ResetActionPoints;

	private void ResetActionPoints(bool f)
	{
		_currentActionPoints = MaxActionPoints;
	}
	private void Awake()
	{
		_currentActionPoints = MaxActionPoints;
		movement = GetComponent<Movement>();
	}

	private void Start()
	{
		_currentHealth = _health;
		if(_healtBar != null)
			_healtBar.fillAmount = 1;
	}

	public void DealDamage(int damage)
	{
		if(_isDead || _immortal) return;
		_health -= damage;
		if(_healtBar != null)
			_healtBar.fillAmount = (float)_health / (float)_currentHealth;
		if (_health <= 0) Kill();
		
	}

	private void Kill()
	{
		_isDead = true;
		if(this is Enemy) Spawn.Instance.Enemyes.Remove(this as Enemy);
		else
		{
			Spawn.Instance.Players.Remove(this as PlayerEntity);
			if (!(this is Egg))
			{
				SpawnEgg.Instance.Spawner(transform.position);
			}
		}
		
		if(_vfx != null)
		{
			var vfx = Instantiate(_vfx, transform.position, Quaternion.identity);
			Destroy(vfx, 4f);
		}
		Destroy(gameObject);
	}
	
	public enum Type
	{
		None,
		Egg,
		Godzilla
	}
}
