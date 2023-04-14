using UnityEngine;

namespace MainGame.Camera
{
    public class CameraRig : MonoBehaviour
    {
        [SerializeField] private Transform _distance;
        [SerializeField] private Transform _rotation;
        [SerializeField] private UnityEngine.Camera _camera;
    }
}