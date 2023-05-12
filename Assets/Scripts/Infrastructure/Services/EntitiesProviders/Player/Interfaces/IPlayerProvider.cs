using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MainGame.Infrastructure.Services.EntitiesProviders.Player.Interfaces
{
    using Player = Entities.Player.Player;
    
    public interface IPlayerProvider
    {
        bool TryGetPlayer(out Player player);
        UniTask<Player> SpawnPlayer(Vector3 pos);
    }
}