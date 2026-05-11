using System;
using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;
using Zenject;

public abstract class EcsWorldService : IInitializable, ITickable, ILateTickable, IFixedTickable, IDisposable
{
    protected readonly DiContainer _container;

    protected IEcsSystems _updateSystems;
    protected IEcsSystems _fixedUpdateSystems;
    protected IEcsSystems _lateUpdateSystems;
    protected SharedData _sharedData;

    protected EcsWorldService(DiContainer container)
    {
        _container = container;
        CreateWorld();
    }

    public void Initialize()
    {
        SetupSystems();

        _updateSystems?.Init();
        _fixedUpdateSystems?.Init();
        _lateUpdateSystems?.Init();
    }

    public void Tick() => _updateSystems?.Run();
    public void LateTick() => _lateUpdateSystems?.Run();
    public void FixedTick() => _fixedUpdateSystems?.Run();

    private void CreateWorld()
    {
        var world = new EcsWorld();
        _sharedData = new SharedData { World = world, EventsBus = new EventsBus() };

        _container.Resolve<SharedDataProvider>().SetSharedData(_sharedData);

        _updateSystems = new EcsSystems(world, _sharedData);
        _fixedUpdateSystems = new EcsSystems(world, _sharedData);
        _lateUpdateSystems = new EcsSystems(world, _sharedData);
    }

    protected abstract void SetupSystems();

    public void Dispose()
    {
        if (_updateSystems != null)
        {
            _updateSystems.Destroy();
            _updateSystems = null;
        }

        if (_fixedUpdateSystems != null)
        {
            _fixedUpdateSystems.Destroy();
            _fixedUpdateSystems = null;
        }

        if (_lateUpdateSystems != null)
        {
            _lateUpdateSystems.Destroy();
            _lateUpdateSystems = null;
        }

        if (_sharedData != null)
        {
            _sharedData.EventsBus?.Destroy();
            _sharedData.World?.Destroy();
            _sharedData = null;
        }
    }
}
