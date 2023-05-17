using System;
using MainGame.Infrastructure.Services.Timer.Interfaces;
using UnityEngine;
using Zenject;

namespace MainGame.Infrastructure.Services.Timer
{
	public class XTimer
    {
	    public delegate void TimerValueChangedHandler(float remainingSeconds, TimerChangingSourceType changingSource);

		private readonly TimerType _type;
        private readonly ITimeTickProvider _timeTickProvider;
        
		public event TimerValueChangedHandler OnTimerValueChanged;
		public event Action OnTimerFinished;

		public bool IsActive { get; private set; }
		public bool IsPaused { get; private set; }
		public float RemainingSeconds { get; private set; }
		
		public XTimer(TimerType type, float seconds, ITimeTickProvider timeTickProvider)
		{
			_timeTickProvider = timeTickProvider;
			_type = type;
			SetTime(seconds);
		}

		public void SetTime(float seconds)
		{
			RemainingSeconds = seconds;
			OnTimerValueChanged?.Invoke(RemainingSeconds, TimerChangingSourceType.TimeForceChanged);
		}

		public void Start()
		{
			if (IsActive)
				return;

			if (Math.Abs(RemainingSeconds) < Mathf.Epsilon)
			{
				OnTimerFinished?.Invoke();
			}

			IsActive = true;
			IsPaused = false;
			SubscribeOnTimeInvokerEvents();

			OnTimerValueChanged?.Invoke(RemainingSeconds, TimerChangingSourceType.TimerStarted);
		}

		public void Start(float seconds)
		{
			if (IsActive)
				return;

			SetTime(seconds);
			Start();
		}

		public void Pause()
		{
			if (IsPaused || !IsActive)
				return;

			IsPaused = true;
			UnsubscribeFromTimeInvokerEvents();

			OnTimerValueChanged?.Invoke(RemainingSeconds, TimerChangingSourceType.TimerPaused);
		}

		public void Unpause()
		{
			if (!IsPaused || !IsActive)
				return;

			IsPaused = false;
			SubscribeOnTimeInvokerEvents();

			OnTimerValueChanged?.Invoke(RemainingSeconds, TimerChangingSourceType.TimerUnpaused);
		}

		public void Stop()
		{
			if (IsActive)
			{
				UnsubscribeFromTimeInvokerEvents();
				
				RemainingSeconds = 0f;
				IsActive = false;
				IsPaused = false;

				OnTimerValueChanged?.Invoke(RemainingSeconds, TimerChangingSourceType.TimerFinished);
				OnTimerFinished?.Invoke();
			}
		}

		private void SubscribeOnTimeInvokerEvents()
		{
			switch (_type)
			{
				case TimerType.UpdateTick:
					_timeTickProvider.OnUpdateTimerTicked += OnTicked;
					break;
				case TimerType.UpdateTickUnscaled:
					_timeTickProvider.OnUnscaledUpdateTimerTicked += OnTicked;
					break;
				case TimerType.OneSecondTick:
					_timeTickProvider.OnOneSecondUpdateTimerTicked += OnOneSecondTicked;
					break;
				case TimerType.OneSecondTickUnscaled:
					_timeTickProvider.OnOneSecondUnscaledUpdateTimerTicked += OnOneSecondTicked;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void UnsubscribeFromTimeInvokerEvents()
		{
			switch (_type)
			{
				case TimerType.UpdateTick:
					_timeTickProvider.OnUpdateTimerTicked -= OnTicked;
					break;
				case TimerType.UpdateTickUnscaled:
					_timeTickProvider.OnUnscaledUpdateTimerTicked -= OnTicked;
					break;
				case TimerType.OneSecondTick:
					_timeTickProvider.OnOneSecondUpdateTimerTicked -= OnOneSecondTicked;
					break;
				case TimerType.OneSecondTickUnscaled:
					_timeTickProvider.OnOneSecondUnscaledUpdateTimerTicked -= OnOneSecondTicked;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void CheckFinish()
		{
			if (RemainingSeconds <= 0f)
			{
				Stop();
			}
		}

		private void NotifyAboutTimePassed()
		{
			if (RemainingSeconds >= 0f)
			{
				OnTimerValueChanged?.Invoke(RemainingSeconds, TimerChangingSourceType.TimePassed);
			}
		}

		private void OnTicked(float deltaTime)
		{
			RemainingSeconds -= deltaTime;
			
			NotifyAboutTimePassed();
			CheckFinish();
		}

		private void OnOneSecondTicked()
		{
			RemainingSeconds -= 1;
			
			NotifyAboutTimePassed();
			CheckFinish();
		}
		
		public class Factory : PlaceholderFactory<TimerType, float, XTimer> { }
    }
}