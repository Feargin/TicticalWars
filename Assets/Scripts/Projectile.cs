using UnityEngine;
using DG.Tweening;

public class Projectile : MonoBehaviour
{
	[SerializeField] private ParticleSystem _explosion;
	[SerializeField] private float _speed = 1f;
	[SerializeField] private bool _hinged = false;
	public event System.Action OnExplode;
	
	public void Init(Vector3 target)
	{
		if(_hinged)
			transform.DOJump(target, 1f, 1, _speed).SetEase(Ease.Linear).OnComplete(Explosion);
		else
			transform.DOMove(target, _speed).SetEase(Ease.Linear).OnComplete(Explosion);
    }
    
	private void Explosion()
	{
		_explosion.Play();
		OnExplode?.Invoke();
		Destroy(gameObject, 0.1f);
	}
}
