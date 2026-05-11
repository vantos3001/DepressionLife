using Leopotam.EcsLite;
using Zenject;

public class SystemFactory : ISystemFactory
{
    private readonly DiContainer _container;

    public SystemFactory(DiContainer container) =>
        _container = container;

    public T Create<T>() where T : IEcsSystem =>
        _container.Instantiate<T>();

    public T CreateFeature<T>() where T : Feature =>
        _container.Instantiate<T>();
}
