using System;
using Leopotam.EcsLite;
using UnityEngine;

public class PlayerInputSystem : IEcsRunSystem
{
    public void Run(IEcsSystems systems)
    {
        var sharedData = systems.GetShared<SharedData>();

        sharedData.PlayerInputData.PreviousTouchPosition = sharedData.PlayerInputData.CurrentTouchPosition;

        sharedData.PlayerInputData.CurrentTouchPosition = GetCurrentTouchPosition();
    }

    public Vector2? GetCurrentTouchPosition()
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