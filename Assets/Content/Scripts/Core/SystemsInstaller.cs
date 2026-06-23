using UnityEngine;
using Zenject;

public class SystemsInstaller : MonoInstaller
{
    [SerializeField] private GameObject _networkSpawnerObject;

    public override void InstallBindings()
    {
        Container.Bind<IEventSystem>().To<GameEventSystem>().AsSingle();
        Container.Bind<IObjectPool>().To<ObjectPool>().AsSingle();
        Container.Bind<IAudioSystem>().To<AudioSystem>().AsSingle();

        Container.Bind<ZenjectNetworkSpawner>().FromNewComponentOn(_networkSpawnerObject).AsSingle().NonLazy();
    }
}
