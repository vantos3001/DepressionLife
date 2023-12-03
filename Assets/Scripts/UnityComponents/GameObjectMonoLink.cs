using Leopotam.EcsLite;

public class GameObjectMonoLink : MonoLink<GameObjectLink>
{
    public override void Make(int entity, EcsWorld world)
    {
        ref var component = ref world.GetPool<GameObjectLink>().Add(entity);
        component.Value = gameObject;
    }
}