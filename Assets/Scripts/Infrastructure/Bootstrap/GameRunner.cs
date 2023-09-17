using MainGame.Infrastructure.Services.Bootstrap;
using UnityEngine;
using Zenject;

namespace MainGame.Infrastructure.Bootstrap
{
    public class GameRunner : MonoBehaviour
    {
        private Bootstrapper.Factory _gameBootstrapperFactory;

        [Inject]
        void Construct(Bootstrapper.Factory bootstrapperFactory)
        {
            _gameBootstrapperFactory = bootstrapperFactory;
        }

        private void Awake()
        {
            var bootstrapper = FindObjectOfType<Bootstrapper>();
            if(bootstrapper != null) return;
            _gameBootstrapperFactory.Create();
        }
    }
}
