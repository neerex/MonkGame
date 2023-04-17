using MainGame.Services.Camera;
using MainGame.Services.Camera.Interfaces;
using UnityEngine;
using Zenject;

namespace MainGame.Camera
{
    public class CopyCameraRotation : MonoBehaviour
    {
        private ICameraService _cameraService;

        [Inject]
        public void Construct(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        private void Update()
        {
            if(_cameraService.CameraRig is null) return;

            transform.rotation = _cameraService.CameraRig.Camera.transform.rotation;
        }
    }
}
