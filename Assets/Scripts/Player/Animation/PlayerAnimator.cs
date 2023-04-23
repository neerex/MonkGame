using System.Collections.Generic;
using MainGame.Services.Input.Interfaces;
using MainGame.Stats;
using MainGame.Stats.Interfaces;
using MainGame.Utilities;
using UnityEngine;
using Zenject;
using Logger = MainGame.Utilities.Logger;

namespace MainGame.Player.Animation
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(ICharacterStatHolder))]
    public class PlayerAnimator : MonoBehaviour, IStatsReader
    {
        private readonly int _walkingSpeedHash = Animator.StringToHash("SpeedPercent");
        
        private readonly int _canGoForward = Animator.StringToHash("CanGoForward");
        private readonly int _canGoBackward = Animator.StringToHash("CanGoBackward");
        private readonly int _canGoLeft = Animator.StringToHash("CanGoLeft");
        private readonly int _canGoRight = Animator.StringToHash("CanGoRight");
        
        private readonly int _canGoForwardRight = Animator.StringToHash("CanGoForwardRight");
        private readonly int _canGoForwardLeft = Animator.StringToHash("CanGoForwardLeft");
        private readonly int _canGoBackwardLeft = Animator.StringToHash("CanGoBackwardLeft");
        private readonly int _canGoBackwardRight = Animator.StringToHash("CanGoBackwardRight");

        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;

        private MovementSpeedStat _movementSpeed;
        private ICharacterStatHolder _characterStatHolder;
        private IPlayerInputService _inputService;

        private List<int> _directionHashes;
        
        [Inject]
        public void Construct(IPlayerInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            _characterStatHolder = GetComponent<ICharacterStatHolder>();
            PopulateDirectionHashList();
        }

        private void Update()
        {
            if(_movementSpeed == null && _inputService == null) return;
            SetWalkDirectionFromAngle(AngleBetweenLookAndVelocity());
            
            _animator.SetFloat(_walkingSpeedHash, GetSpeedPercent(), 0.1f, Time.deltaTime);
        }

        public void InitializeStats() => 
            _characterStatHolder.GetStat(out _movementSpeed);

        private void PopulateDirectionHashList()
        {
            _directionHashes = new List<int>
            {
                _canGoForward,
                _canGoRight,
                _canGoBackward,
                _canGoLeft,
                _canGoForwardLeft,
                _canGoForwardRight,
                _canGoBackwardLeft,
                _canGoBackwardRight
            };
        }

        private float AngleBetweenLookAndVelocity() => 
            Vector3.SignedAngle(transform.forward.FlatY(), _inputService.GetInputDirection(), Vector3.up);

        private float GetSpeedPercent() => 
            _rigidbody.velocity.magnitude / _movementSpeed.Value;

        private void SetWalkDirectionFromAngle(float angle)
        {
            ResetDirectionBooleans();
            if (angle == 0) return;
            if (angle >= -23 && angle <= 23) _animator.SetBool(_canGoForward, true);
            else if (angle >= -68 && angle < -23) _animator.SetBool(_canGoForwardLeft, true);
            else if (angle >= -113 && angle < -68) _animator.SetBool(_canGoLeft, true);
            else if (angle >= -158 && angle < -113) _animator.SetBool(_canGoBackwardLeft, true);
            else if (angle < -158 || angle > 158) _animator.SetBool(_canGoBackward, true);
            else if (angle > 113 && angle <= 158) _animator.SetBool(_canGoBackwardRight, true);
            else if (angle > 68 && angle <= 113) _animator.SetBool(_canGoRight, true);
            else if (angle > 23 && angle <= 68) _animator.SetBool(_canGoForwardRight, true);
        }
        
        private void ResetDirectionBooleans() => 
            _directionHashes.ForEach(d => _animator.SetBool(d, false));
    }
}
