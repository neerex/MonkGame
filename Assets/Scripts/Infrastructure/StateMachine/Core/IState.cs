namespace MainGame.Infrastructure.StateMachine.Core
{
    public interface IState : IExitable
    {
        void Enter();
    }
}