using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.AI;

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

        var hasRaycast = Physics.Raycast(ray, out var hit, 50f, _settings.Value.GroundMask);
        
        foreach (var playerEntity in _players.Value)
        {
            ref var agent = ref _players.Pools.Inc2.Get(playerEntity);
            if (hasRaycast)
            {
                agent.Value.destination = hit.point;
            }
            else
            {
                agent.Value.ResetPath();
            }
        }
    }
}