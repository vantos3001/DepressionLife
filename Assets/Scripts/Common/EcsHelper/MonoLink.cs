using Leopotam.EcsLite;

public class MonoLink<T> : MonoLinkBase where T : struct
{
    public T Value;

    public override void Make(int entity, EcsWorld world)
    {
        if (world.GetPool<T>().Has(entity))
        {
            return;
        }

        ref var component = ref world.GetPool<T>().Add(entity);
        component = Value;
    }
}