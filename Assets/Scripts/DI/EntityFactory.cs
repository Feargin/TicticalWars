using UnityEngine;
using Zenject;

public interface IEntityFactory
{
	public Entity Create(Entity entity, Vector3 position);
}

public class EntityFactory : IEntityFactory
{
	private readonly DiContainer _diContainer;
	
	[Inject]
	public EntityFactory(DiContainer diContainer)
	{
		_diContainer = diContainer;
	}

	public Entity Create(Entity entity, Vector3 position)
    {
	    return _diContainer.InstantiatePrefabForComponent<Entity>(entity, position, Quaternion.identity, null);
    }
}