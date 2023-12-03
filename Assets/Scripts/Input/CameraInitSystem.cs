using Cinemachine;
using Leopotam.EcsLite;
using UnityEngine;

public class CameraInitSystem : IEcsInitSystem
{
    public void Init(IEcsSystems systems)
    {
        systems.GetShared<SharedData>().Camera = Object.FindObjectOfType<CinemachineVirtualCamera>();
    }
}