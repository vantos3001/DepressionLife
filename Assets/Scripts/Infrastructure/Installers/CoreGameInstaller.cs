using UnityEngine;
using Zenject;

public class CoreGameInstaller : MonoInstaller
{
    [SerializeField] private SceneData _sceneData;
    [SerializeField] private GameSettings _settings;

    public override void InstallBindings()
    {
        Container.Bind<SceneData>().FromInstance(_sceneData).AsSingle();
        Container.Bind<GameSettings>().FromInstance(_settings).AsSingle();

        Container.Bind<SharedDataProvider>().AsSingle();
        Container.Bind<ISystemFactory>().To<SystemFactory>().AsSingle();

        Container.BindInterfacesTo<GameWorldService>().AsSingle();
    }
}
