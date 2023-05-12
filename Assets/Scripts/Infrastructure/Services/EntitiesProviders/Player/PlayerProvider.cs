using Cysharp.Threading.Tasks;
using MainGame.Infrastructure.Services.Asset.Interfaces;
using MainGame.Infrastructure.Services.EntitiesProviders.Player.Interfaces;
using MainGame.StaticData;
using UnityEngine;
using Logger = MainGame.Utilities.Logger;

namespace MainGame.Infrastructure.Services.EntitiesProviders.Player
{
    using Player = Entities.Player.Player;
    public class PlayerProvider : IPlayerProvider
    {
        private readonly IInjectedAssetProvider _injectedAssetProvider;
        private Player _player;

        public PlayerProvider(IInjectedAssetProvider injectedAssetProvider)
        {
            _injectedAssetProvider = injectedAssetProvider;
        }

        public bool TryGetPlayer(out Player player)
        {
            if (_player != null)
            {
                player = _player;
                return true;
            }

            Logger.Log("There is no Player in the scene", Color.red);
            player = default;
            return false;
        }

        public async UniTask<Player> SpawnPlayer(Vector3 pos)
        {
            GameObject playerGameObject = await _injectedAssetProvider.Instantiate(PrefabAddresses.Player, pos);
            _player = playerGameObject.GetComponent<Player>();
            return _player;
        }
    }
}