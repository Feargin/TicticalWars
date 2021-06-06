using UnityEngine;

public class CollideAttack : MonoBehaviour
{
	public int Damage = 10;
	private bool _enabled = false;
	
	[Zenject.Inject, HideInInspector] public SoundController soundController;
	
	public event System.Action OnDealDamage;
	
	private void OnCollisionEnter(Collision col)
	{
		if (_enabled && col.transform.TryGetComponent(out PlayerEntity player))
		{
			soundController.SetClip(4);
			player.DealDamage(Damage);
			OnDealDamage?.Invoke();
		}
	}
	
	public void Enable()
	{
		_enabled = true;
	}
	
	public void Disable()
	{
		_enabled = false;
	}
}
