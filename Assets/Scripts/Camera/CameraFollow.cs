using UnityEngine;

namespace MainGame.Camera
{
    [RequireComponent(typeof(CameraRig))]
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _objectToFollow;
        [SerializeField] private CameraRig _cameraRig;
        [SerializeField] private float _followSpeed = 5;
        
        private void Awake()
        {
            _cameraRig = GetComponent<CameraRig>();
        }

        private void LateUpdate()
        {
            _cameraRig.transform.position = Vector3.Lerp(transform.position,
                _objectToFollow.position,
                Time.deltaTime * _followSpeed);
        }
    }
}