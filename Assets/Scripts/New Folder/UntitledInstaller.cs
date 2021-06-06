using UnityEngine;
using Zenject;

public class UntitledInstaller : MonoInstaller
{
	public TestSingletonnNO imOne;
	
    public override void InstallBindings()
	{
		Container.Bind<IService>().FromInstance(imOne).AsSingle();
    }
}
