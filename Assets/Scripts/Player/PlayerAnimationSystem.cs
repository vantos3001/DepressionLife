using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class PlayerAnimationSystem : IEcsRunSystem{
    
    private static readonly int IsIdleHash = Animator.StringToHash("IsIdle");
    private static readonly int IsWalkHash = Animator.StringToHash("IsWalk");

    private EcsFilterInject<Inc<PlayerTag, AnimatorLink, NavMeshAgentLink>> _players = default;

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _players.Value)
        {
            ref var animator = ref _players.Pools.Inc2.Get(entity);
            ref var agent = ref _players.Pools.Inc3.Get(entity);
            
            if (agent.Value.velocity.magnitude < 0.1f)
            {
                animator.Value.SetBool(IsIdleHash, true);
                animator.Value.SetBool(IsWalkHash, false);
            }
            else
            {
                animator.Value.SetBool(IsIdleHash, false);
                animator.Value.SetBool(IsWalkHash, true);
            }
        }
    }
}