using System.Collections.Generic;
using Leopotam.EcsLite;

public abstract class Feature
{
    public List<IEcsSystem> GetSystems { get; } = new List<IEcsSystem>();
}
