using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

public class PlayerInitSystem : IEcsInitSystem
{
    private EcsCustomInject<SceneData> _sceneData;
    private EcsCustomInject<GameSettings> _settings;

    public void Init(IEcsSystems systems)
    {
        EcsFactory.Spawn(_settings.Value.PlayerPrefab, systems.GetWorld());
    }
}
