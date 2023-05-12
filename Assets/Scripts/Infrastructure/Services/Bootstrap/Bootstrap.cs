using MainGame.Infrastructure.StateMachine.BootstrapStates;
using UnityEngine;
using Zenject;

namespace MainGame.Infrastructure.Services.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        private GameStatemachine _gameStatemachine;

        [Inject]
        public void Construct(GameStatemachine gameStatemachine)
        {
            _gameStatemachine = gameStatemachine;
        }

        private void Start()
        {
            _gameStatemachine.EnterBootStrapState();
        }
    }
}
