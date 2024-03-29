﻿using MainGame.Camera;
using MainGame.Infrastructure.Services.Camera.Interfaces;
using MainGame.Infrastructure.Services.Input.Interfaces;
using MainGame.Infrastructure.Services.Raycast.Interfaces;
using MainGame.Utilities;
using UnityEngine;

namespace MainGame.Infrastructure.Services.Raycast
{
    public class MouseRaycastService : IMouseRaycastService
    {
        private readonly ICameraService _cameraService;
        private readonly IPlayerInputService _inputService;
        private CameraRig _cameraRig; 

        public MouseRaycastService(IPlayerInputService inputService, ICameraService cameraService)
        {
            _inputService = inputService;
            _cameraService = cameraService;
        }
        
        public Vector3 GetDirectionToRaycastHit(Vector3 fromPosition)
        {
            _cameraRig ??= _cameraService.CameraRig;
            if (_cameraRig is null) return Vector3.zero;
            
            Ray ray = _cameraRig.Camera.ScreenPointToRay(_inputService.GetMousePosition());
            Plane plane = new Plane(Vector3.up, fromPosition);
            plane.Raycast(ray, out float enter);
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 direction = (hitPoint.FlatY() - fromPosition.FlatY()).normalized;
            return direction;
        }

        public RaycastHit GetPointOnSurface(LayerMask layerMask)
        {
            _cameraRig ??= _cameraService.CameraRig;
            if (_cameraRig is null) return default;
            
            Ray ray = _cameraRig.Camera.ScreenPointToRay(_inputService.GetMousePosition());
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, layerMask))
            {
                return hitInfo;
            }

            return default;
        }
    }
}