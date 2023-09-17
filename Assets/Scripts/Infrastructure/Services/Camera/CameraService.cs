using Cysharp.Threading.Tasks;
using MainGame.Camera;
using MainGame.Infrastructure.Services.Asset.Interfaces;
using MainGame.Infrastructure.Services.Camera.Interfaces;
using MainGame.StaticData;
using UnityEngine;

namespace MainGame.Infrastructure.Services.Camera
{
    public class CameraService : ICameraService
    {
        private readonly IAssetProvider _assetProvider;
        public CameraRig CameraRig { get; private set; }

        public CameraService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async UniTask<CameraRig> SpawnCameraRig(Vector3 pos)
        {
            var rig = await _assetProvider.InstantiateAsync(PrefabAddresses.CameraRig, pos);
            CameraRig = rig.GetComponent<CameraRig>();
            return CameraRig;
        }

        public void SetFollow(Transform transformToFollow)
        {
            if (CameraRig == null)
            {
                Debug.LogError("There is no camera rig");
                return;
            }
            
            CameraRig.GetComponent<CameraFollow>().SetFollow(transformToFollow);
        }
    }
}