using Leopotam.EcsLite;

public interface ISystemFactory
{
    T Create<T>() where T : IEcsSystem;
    T CreateFeature<T>() where T : Feature;
}
