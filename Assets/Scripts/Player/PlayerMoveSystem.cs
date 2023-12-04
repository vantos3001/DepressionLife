using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class PlayerMoveSystem : IEcsRunSystem
{
    private EcsFilterInject<Inc<PlayerTag, NavMeshAgentLink>> _players = default;

    private EcsCustomInject<GameSettings> _settings;

    public void Run(IEcsSystems systems)
    {
        var sharedData = systems.GetShared<SharedData>();

        if (!sharedData.PlayerInputData.IsJustClick)
        {
            return;
        }

        if (sharedData.PlayerInputData.PreviousTouchPosition == null)
        {
            return;
        }

        var raycastPosition = new Vector3(sharedData.PlayerInputData.PreviousTouchPosition.Value.x, sharedData.PlayerInputData.PreviousTouchPosition.Value.y, 10);
        var ray = Camera.main.ScreenPointToRay(raycastPosition);

        var hasRaycast = Physics.Raycast(ray, out var hit, 50f, CombineLayerMasks(_settings.Value.GroundMask, _settings.Value.ObstacleMask));
        
        foreach (var playerEntity in _players.Value)
        {
            ref var agent = ref _players.Pools.Inc2.Get(playerEntity);
            if (hasRaycast && HasMask(hit.transform.gameObject.layer, _settings.Value.GroundMask))
            {
                agent.Value.destination = hit.point;
            }
            else
            {
                agent.Value.ResetPath();
            }
        }
    }

    public LayerMask CombineLayerMasks(LayerMask layerMask1, LayerMask layerMask2)
    {
        var finalMask = layerMask1 | layerMask2;

        return finalMask;
    }

    public bool HasMask(int currentLayer, LayerMask checkingMask)
    {
        int currentLayerMask = 1 << currentLayer;

        return (checkingMask & currentLayerMask) != 0;
    }
}