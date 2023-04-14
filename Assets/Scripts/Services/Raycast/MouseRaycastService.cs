using MainGame.Input;
using MainGame.Services.Camera;
using MainGame.Utilities;
using UnityEngine;

namespace MainGame.Services.Raycast
{
    public class MouseRaycastService : IMouseRaycastService
    {
        private ICameraService _cameraService;
        private IPlayerInputService _inputService;
        private Plane _plane;
        private UnityEngine.Camera _camera; 

        public MouseRaycastService(IPlayerInputService inputService, ICameraService cameraService)
        {
            _inputService = inputService;
            _cameraService = cameraService;
            
            _plane = new Plane(Vector3.up, Vector3.zero);
        }
        
        public Vector3 GetDirectionToRaycastHit(Vector3 fromPosition)
        {
            _camera ??= _cameraService.GetCameraRig().Camera;
            if (_camera is null) return Vector3.zero;
            
            Ray ray = _camera.ScreenPointToRay(_inputService.GetMousePosition());
            _plane.Raycast(ray, out float enter);
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 direction = (hitPoint.FlatY() - fromPosition.FlatY()).normalized;
            return direction;
        }
    }
}