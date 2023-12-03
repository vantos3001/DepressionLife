using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

public class GameEcsStartup : MonoBehaviour
{
    [SerializeField] SceneData _sceneData;
    [SerializeField] GameSettings _settings;

    private EcsWorld _world;
    public EcsWorld World => _world;
    
    private SharedData _sharedData;
    public SharedData SharedData => _sharedData;
    
    private IEcsSystems _updateSystems;
    
    public static GameSettings Settings => Instance._settings;
    public static GameEcsStartup Instance { get; private set; }

    private void Start()
    {
        _world = new EcsWorld();
        _sharedData = new SharedData(){EventsBus = new EventsBus()};
        _updateSystems = new EcsSystems(_world);
        
        _updateSystems
#if UNITY_EDITOR
            .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
            .Add(_sharedData.EventsBus.GetDestroyEventsSystem())
            .Inject (_sceneData)
            .Init ();
    }
    
    private void Update () {
        _updateSystems?.Run ();
    }
    
    private void OnDestroy () {
        if (_updateSystems != null) {
            _updateSystems.Destroy ();
            _updateSystems = null;
        }
        
        if (_world != null) {
            _world.Destroy ();
            _world = null;
        }
        
        _sharedData.EventsBus.Destroy();
    }
}