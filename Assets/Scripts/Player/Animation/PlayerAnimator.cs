using System;
using MainGame.Services.Input.Interfaces;
using MainGame.Stats;
using MainGame.Stats.Interfaces;
using MainGame.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace MainGame.Player.Animation
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(ICharacterStatHolder))]
    [RequireComponent(typeof(IsGroundProvider))]
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour, IStatsReader
    {
        private readonly int _walkingSpeed = Animator.StringToHash("SpeedPercent");
        private readonly int _velocityX = Animator.StringToHash("VelocityX");
        private readonly int _velocityY = Animator.StringToHash("VelocityY");
        private readonly int _isGrounded = Animator.StringToHash("IsGrounded");
        
        private readonly int _jumpAnimationHash = Animator.StringToHash("Jump");
        
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private IsGroundProvider _groundProvider;

        [SerializeField] [Range(0.01f, 0.3f)] private float _animationSmooth = 0.07f;
        

        private ICharacterStatHolder _characterStatHolder;
        private IPlayerInputService _inputService;
        
        private MovementSpeedStat _movementSpeed;

        private float _currentPercentSpeedBlendValue;
        private float _refSpeedPercent;
        
        private Vector2 _currentVelocityBlendValue;
        private Vector2 _refVelocity;
        
        private float _directionAngle;

        [Inject]
        public void Construct(IPlayerInputService inputService)
        {
            _inputService = inputService;
            _inputService.OnJumpInputPerformed += StartJumpAnimation;
        }

        private void Awake()
        {
            _characterStatHolder = GetComponent<ICharacterStatHolder>();
            _groundProvider = GetComponent<IsGroundProvider>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if(_movementSpeed == null || _inputService == null) return;
            _animator.SetBool(_isGrounded, _groundProvider.IsGround);
            float smoothSpeedPercent = GetSmoothSpeedPercent();
            SetWalkDirectionFromAngle(AngleBetweenLookAndVelocity(), smoothSpeedPercent);
            _animator.SetFloat(_walkingSpeed, smoothSpeedPercent , 0.1f, Time.deltaTime);
        }

        private void OnDestroy()
        {
            _inputService.OnJumpInputPerformed -= StartJumpAnimation;
        }

        public void InitializeStats() => 
            _characterStatHolder.GetStat(out _movementSpeed);

        private void StartJumpAnimation(InputAction.CallbackContext context)
        {
            if (_groundProvider.IsGround)
            {
                _animator.CrossFade(_jumpAnimationHash, 0.15f);
            }
        }

        private float AngleBetweenLookAndVelocity()
        {
            var inputDirection = _inputService.GetInputDirection();
            if (inputDirection == Vector3.zero) return _directionAngle;
            _directionAngle = Vector3.SignedAngle(transform.forward.FlatY(), inputDirection, Vector3.up);
            return _directionAngle;
        }

        private float GetSmoothSpeedPercent()
        {
            float target = _rigidbody.velocity.FlatY().magnitude / _movementSpeed.Value;
            
            _currentPercentSpeedBlendValue = Mathf.SmoothDamp(_currentPercentSpeedBlendValue, 
                target, 
                ref _refSpeedPercent, 
                _animationSmooth);
            
            return _currentPercentSpeedBlendValue;
        }

        private void SetWalkDirectionFromAngle(float angle, float speed)
        {
            Vector2 desiredVelocity = Quaternion.AngleAxis(-angle, Vector3.forward) * Vector3.up * speed;
            
            _currentVelocityBlendValue = Vector2.SmoothDamp(_currentVelocityBlendValue, 
                desiredVelocity, 
                ref _refVelocity,
                _animationSmooth);
            
            _animator.SetFloat(_velocityX, _currentVelocityBlendValue.x);
            _animator.SetFloat(_velocityY, _currentVelocityBlendValue.y);
        }
    }
}
