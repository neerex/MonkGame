namespace MainGame.Infrastructure.Services.Timer
{
    public enum TimerChangingSourceType
    {
        TimerStarted = 0,
        TimerFinished = 1,
        TimerPaused = 2,
        TimerUnpaused = 3,
        TimePassed = 4,
        TimeForceChanged = 5
    }
}