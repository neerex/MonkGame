using UnityEngine;

namespace MainGame.Camera
{
    [RequireComponent(typeof(CameraRig))]
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _followSpeed = 5;
        private CameraRig _cameraRig;
        private Transform _transformToFollow;
        
        private void Awake()
        {
            _cameraRig = GetComponent<CameraRig>();
        }

        private void LateUpdate()
        {
            if(_transformToFollow is null) 
                return;
            
            _cameraRig.transform.position = Vector3.Lerp(transform.position,
                _transformToFollow.position,
                Time.deltaTime * _followSpeed);
        }

        public void SetFollow(Transform transformToFollow) => 
            _transformToFollow = transformToFollow;
    }
}