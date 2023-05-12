using Cysharp.Threading.Tasks;
using MainGame.Camera;
using UnityEngine;

namespace MainGame.Infrastructure.Services.Camera.Interfaces
{
    public interface ICameraService
    {
        CameraRig CameraRig { get; }
        UniTask<CameraRig> SpawnCameraRig(Vector3 pos);
        void SetFollow(Transform transformToFollow);
    }
}