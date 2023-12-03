using UnityEngine;

public class SceneData : MonoBehaviour
{
    [SerializeField] private BoxCollider _clampCameraCollider;
    public BoxCollider ClampCameraCollider => _clampCameraCollider;

    [SerializeField] private Transform _playerSpawnPoint;
    public Transform PlayerSpawnPoint => _playerSpawnPoint;
}