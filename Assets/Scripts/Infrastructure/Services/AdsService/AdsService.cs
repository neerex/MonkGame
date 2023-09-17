using System;
using Logger = MainGame.Utilities.Logger;

namespace MainGame.Infrastructure.Services.AdsService
{
    public class AdsService : IAdsService
    {
        public event Action OnRewardedVideoReady;
        public bool IsRewardedVideoReady { get; }
        
        public void Initialize()
        {
            Logger.LogWarning("Ads service isn't implemented yet");
        }

        public void ShowRewardedVideo(Action onVideoFinished)
        {
            
        }
    }
}