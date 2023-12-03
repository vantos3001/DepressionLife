using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game/Settings/GameSettings")]
public class GameSettings : ScriptableObject
{
    public float MoveCameraMultiplier = 1f;
    public float MaxJustClickTime = 0.3f;
    public float SwapTolerance = 0.3f;

    public GameObject PlayerPrefab;

    public LayerMask GroundMask;
}