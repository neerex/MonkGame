namespace MainGame.Infrastructure.StateMachine.Core
{
    public interface IPayLoadedState<in TPayload> : IExitable
    {
        void Enter(TPayload payload);
    }
}