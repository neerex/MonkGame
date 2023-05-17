using System;
using MainGame.Infrastructure.Services.Timer.Interfaces;
using UnityEngine;
using Zenject;

namespace MainGame.Infrastructure.Services.Timer
{
    public class TimeTickProvider : ITimeTickProvider, ITickable
    {
        private float _oneSecondTimer;
        private float _oneSecondUnscaledTimer;

        public event Action<float> OnUpdateTimerTicked;
        public event Action<float> OnUnscaledUpdateTimerTicked;
        public event Action OnOneSecondUpdateTimerTicked;
        public event Action OnOneSecondUnscaledUpdateTimerTicked;
        
        public void Tick()
        {
            ProcessTimeTick();
            ProcessUnscaledTimeTick();
        }

        private void ProcessTimeTick()
        {
            _oneSecondTimer += Time.deltaTime;
            OnUpdateTimerTicked?.Invoke(Time.deltaTime);
            if (_oneSecondTimer >= 1)
            {
                _oneSecondTimer -= 1;
                OnOneSecondUpdateTimerTicked?.Invoke();
            }
        }

        private void ProcessUnscaledTimeTick()
        {
            _oneSecondUnscaledTimer += Time.unscaledDeltaTime;
            OnUnscaledUpdateTimerTicked?.Invoke(Time.unscaledDeltaTime);
            if (_oneSecondUnscaledTimer >= 1)
            {
                _oneSecondUnscaledTimer -= 1;
                OnOneSecondUnscaledUpdateTimerTicked?.Invoke();
            }
        }
    }
}