using System;

namespace MainGame.Infrastructure.Services.Timer.Interfaces
{
    public interface ITimeTickProvider
    {
        event Action<float> OnUpdateTimerTicked;
        event Action<float> OnUnscaledUpdateTimerTicked;
        event Action OnOneSecondUpdateTimerTicked;
        event Action OnOneSecondUnscaledUpdateTimerTicked;
    }
}