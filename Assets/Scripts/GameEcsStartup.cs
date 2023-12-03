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

    private void Start()
    {
        _world = new EcsWorld();
        _sharedData = new SharedData(){EventsBus = new EventsBus()};
        _updateSystems = new EcsSystems(_world, _sharedData);
        
        _updateSystems
            .Add(new CameraInitSystem())
                
            .Add(new PlayerInputSystem())
                
            .Add(new MoveCameraByInputSystem())
            
#if UNITY_EDITOR
            .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
            .Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem())
#endif
            .Add(_sharedData.EventsBus.GetDestroyEventsSystem())
            .Inject (_sceneData, _settings)
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