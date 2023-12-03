using Leopotam.EcsLite;
using UnityEngine;

public static class EcsFactory
{
    public static void Spawn(GameObject prefab, EcsWorld world)
    {
        GameObject gameObject = Object.Instantiate(prefab);

        var monoEntity = gameObject.GetComponent<MonoEntity>();
        if (monoEntity == null)
        {
            return;
        }

        var ecsEntity = world.NewEntity();
        monoEntity.Make(ecsEntity, world);
    }
}