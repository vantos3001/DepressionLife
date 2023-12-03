using Leopotam.EcsLite;
using UnityEngine;

[RequireComponent(typeof(MonoEntity))]
public abstract class MonoLinkBase : MonoBehaviour
{
    public abstract void Make(int entity, EcsWorld world);
}