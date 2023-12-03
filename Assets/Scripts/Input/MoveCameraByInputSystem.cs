using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class MoveCameraByInputSystem : IEcsRunSystem
{
    private EcsCustomInject<GameSettings> _settings = default;
    private EcsCustomInject<SceneData> _sceneData = default;

    public void Run(IEcsSystems systems)
    {
        var sharedData = systems.GetShared<SharedData>();

        if (sharedData.PlayerInputData.CurrentTouchPosition == null)
        {
            return;
        }

        if (sharedData.PlayerInputData.PreviousTouchPosition == null)
        {
            return;
        }

        var deltaPosition = sharedData.PlayerInputData.CurrentTouchPosition.Value -
                            sharedData.PlayerInputData.PreviousTouchPosition.Value;

        deltaPosition *= _settings.Value.MoveCameraMultiplier;

        var newCameraPosition = sharedData.Camera.transform.position + new Vector3(0f, 0f, deltaPosition.x);

        newCameraPosition = _sceneData.Value.ClampCameraCollider.bounds.ClosestPoint(newCameraPosition);

        sharedData.Camera.transform.position = newCameraPosition;
    }
}