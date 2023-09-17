using System;

namespace MainGame.Infrastructure.Services.AdsService
{
    public interface IAdsService
    {
        public event Action OnRewardedVideoReady;
        public bool IsRewardedVideoReady { get; }
        public void Initialize();
        public void ShowRewardedVideo(Action onVideoFinishedCallback);
    }
}