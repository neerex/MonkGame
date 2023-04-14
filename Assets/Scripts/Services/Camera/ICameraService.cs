using MainGame.Camera;
using UnityEngine;

namespace MainGame.Services.Camera
{
    public interface ICameraService
    {
        CameraRig GetCameraRig();
        CameraRig SpawnCameraRig(Vector3 pos);
        void SetFollow(Transform transformToFollow);
    }
}