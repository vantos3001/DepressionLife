using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class PlayerInputSystem : IEcsRunSystem
{
    private EcsCustomInject<GameSettings> _settings;

    public void Run(IEcsSystems systems)
    {
        var sharedData = systems.GetShared<SharedData>();

        sharedData.PlayerInputData.PreviousTouchPosition = sharedData.PlayerInputData.CurrentTouchPosition;

        sharedData.PlayerInputData.CurrentTouchPosition = GetCurrentTouchPosition();

        UpdateClickTime(sharedData);

        UpdateWasSwap(sharedData.PlayerInputData);

        UpdateIsJustClick(sharedData.PlayerInputData);


        sharedData.PlayerInputData.IsPressed = sharedData.PlayerInputData.PreviousTouchPosition != null &&
                                               sharedData.PlayerInputData.CurrentTouchPosition != null;
    }

    private void UpdateClickTime(SharedData sharedData)
    {
        if (sharedData.PlayerInputData.CurrentTouchPosition != null)
        {
            sharedData.PlayerInputData.PressedTime += Time.deltaTime;
            return;
        }

        if (sharedData.PlayerInputData.PreviousTouchPosition != null)
        {
            return;
        }

        sharedData.PlayerInputData.PressedTime = 0f;
    }

    private void UpdateIsJustClick(PlayerInputData data)
    {
        data.IsJustClick = _settings.Value.MaxJustClickTime >= data.PressedTime && !data.WasSwap &&
                           data.PreviousTouchPosition != null && data.CurrentTouchPosition == null;
    }

    private void UpdateWasSwap(PlayerInputData data)
    {
        if (data.CurrentTouchPosition != null && data.PreviousTouchPosition != null)
        {
            data.WasSwap = data.WasSwap ||
                           Vector3.Magnitude(data.CurrentTouchPosition.Value - data.PreviousTouchPosition.Value) >=
                           _settings.Value.SwapTolerance;
            return;
        }

        if (data.CurrentTouchPosition == null && data.PreviousTouchPosition == null)
        {
            data.WasSwap = false;
            return;
        }
    }

    private Vector2? GetCurrentTouchPosition()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        else
        {
            return null;
        }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        if (Input.touchCount > 0)
        {
            return Input.GetTouch(0).position;
        }
        else
        {
            return null;
        }
#endif

        throw new NotImplementedException("GetCurrentTouchPosition");
    }
}