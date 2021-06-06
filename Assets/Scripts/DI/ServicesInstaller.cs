using UnityEngine;
using Zenject;

public class ServicesInstaller : MonoInstaller
{
	public SoundController _audioManager;
	public ChangeTurn _turnSystem;
	public MoveHelper _moveHelper;
	public SpawnEgg _spawnEgg;
	public Spawn _spawn;
	public Map _map;
	
    public override void InstallBindings()
	{
		Container.Bind<IEntityFactory>().To<EntityFactory>().AsSingle();
		Container.Bind<SoundController>().FromInstance(_audioManager);
		Container.Bind<ChangeTurn>().FromInstance(_turnSystem);
		Container.Bind<MoveHelper>().FromInstance(_moveHelper);
		Container.Bind<SpawnEgg>().FromInstance(_spawnEgg);
		Container.Bind<Spawn>().FromInstance(_spawn);
		Container.Bind<Map>().FromInstance(_map);
    }
}