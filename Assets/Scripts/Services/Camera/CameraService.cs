using MainGame.Camera;
using MainGame.Services.Asset;
using MainGame.StaticData;
using UnityEngine;

namespace MainGame.Services.Camera
{
    public class CameraService : ICameraService
    {
        private readonly IAssetProvider _assetProvider;
        private CameraRig _cameraRig;

        public CameraService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public CameraRig GetCameraRig()
        {
            if (_cameraRig == null)
            {
                Debug.LogError("There is no camera rig, but I'll spawn new one at default position");
                SpawnCameraRig(Vector3.zero);
            }

            return _cameraRig;
        }

        public CameraRig SpawnCameraRig(Vector3 pos)
        {
            _cameraRig = _assetProvider.Instantiate(PrefabAddresses.CameraRig, pos)
                .GetComponent<CameraRig>();

            return _cameraRig;
        }

        public void SetFollow(Transform transformToFollow)
        {
            if (_cameraRig == null)
            {
                Debug.LogError("There is no camera rig");
                return;
            }
            
            _cameraRig.GetComponent<CameraFollow>().SetFollow(transformToFollow);
        }
    }
}