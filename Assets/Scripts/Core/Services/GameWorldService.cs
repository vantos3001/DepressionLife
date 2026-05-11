using Leopotam.EcsLite.Di;
using Zenject;

#if UNITY_EDITOR
using Leopotam.EcsLite.UnityEditor;
#endif

public class GameWorldService : EcsWorldService
{
    private readonly ISystemFactory _systemFactory;
    private readonly SceneData _sceneData;
    private readonly GameSettings _settings;

    public GameWorldService(
        DiContainer container,
        ISystemFactory systemFactory,
        SceneData sceneData,
        GameSettings settings) : base(container)
    {
        _systemFactory = systemFactory;
        _sceneData = sceneData;
        _settings = settings;
    }

    protected override void SetupSystems()
    {
        _updateSystems
            .Add(_systemFactory.CreateFeature<InputFeature>())
            .Add(_systemFactory.CreateFeature<PlayerFeature>())
#if UNITY_EDITOR
            .Add(new EcsWorldDebugSystem())
            .Add(new EcsSystemsDebugSystem())
#endif
            .Add(_sharedData.EventsBus.GetDestroyEventsSystem())
            .Inject(_sceneData, _settings);

        _fixedUpdateSystems
            .Inject();

        _lateUpdateSystems
            .Inject();
    }
}
