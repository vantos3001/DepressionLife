using Leopotam.EcsLite;

public static class ProxyFeature
{
    public static IEcsSystems Add(this IEcsSystems systems, Feature feature)
    {
        foreach (var system in feature.GetSystems)
            systems.Add(system);

        return systems;
    }

    public static Feature Add(this Feature feature, IEcsSystem system)
    {
        feature.GetSystems.Add(system);
        return feature;
    }

    public static Feature Add(this Feature feature, Feature otherFeature)
    {
        feature.GetSystems.AddRange(otherFeature.GetSystems);
        return feature;
    }
}
