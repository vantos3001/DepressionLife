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
            Debug.LogError("Not found MonoEntity. Name = " + gameObject.name);
            return;
        }

        var ecsEntity = world.NewEntity();
        monoEntity.Make(ecsEntity, world);
    }
}