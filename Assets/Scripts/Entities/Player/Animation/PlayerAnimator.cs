using MainGame.Infrastructure.Services.Input.Interfaces;
using MainGame.Stats.ConcreteStat;
using MainGame.Stats.Interfaces;
using MainGame.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace MainGame.Entities.Player.Animation
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(IStatHolder))]
    [RequireComponent(typeof(IsGroundProvider))]
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour, IStatsReader
    {
        private readonly int _walkingSpeed = Animator.StringToHash("SpeedPercent");
        private readonly int _velocityX = Animator.StringToHash("VelocityX");
        private readonly int _velocityY = Animator.StringToHash("VelocityY");
        private readonly int _isGrounded = Animator.StringToHash("IsGrounded");
        
        private readonly int _jumpAnimationHash = Animator.StringToHash("Jump");
        
        private readonly int _mainAttackAnimationHash = Animator.StringToHash("MainAttack");
        private readonly int _secondaryAttackAnimationHash = Animator.StringToHash("SecondaryAttack");
        private readonly int _attack1AnimationHash = Animator.StringToHash("Attack1");
        private readonly int _attack2AnimationHash = Animator.StringToHash("Attack2");
        private readonly int _attack3AnimationHash = Animator.StringToHash("Attack3");
        private readonly int _attack4AnimationHash = Animator.StringToHash("Attack4");
        
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private IsGroundProvider _groundProvider;

        [SerializeField] [Range(0.01f, 0.3f)] private float _animationSmooth = 0.07f;

        private IStatHolder _statHolder;
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
            _statHolder = GetComponent<IStatHolder>();
            _groundProvider = GetComponent<IsGroundProvider>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if(_movementSpeed == null || _inputService == null) return;
            _animator.SetBool(_isGrounded, _groundProvider.IsGround);
            float smoothSpeedPercent = GetSmoothSpeedPercent();
            SetWalkDirectionFromAngle(smoothSpeedPercent);
            _animator.SetFloat(_walkingSpeed, smoothSpeedPercent , 0.1f, Time.deltaTime);
        }

        private void OnDestroy()
        {
            _inputService.OnJumpInputPerformed -= StartJumpAnimation;
        }

        public void PlayMainAttack() => PlayAnimationWithHash(_mainAttackAnimationHash);
        public void PlaySecondaryAttack() => PlayAnimationWithHash(_secondaryAttackAnimationHash);
        public void PlayAttackSlot1() => PlayAnimationWithHash(_attack1AnimationHash);
        public void PlayAnimationWithHash(int hash) => _animator.CrossFade(hash, _animationSmooth);
        
        public void InitializeStats() => _statHolder.GetStat(out _movementSpeed);

        private void StartJumpAnimation(InputAction.CallbackContext context)
        {
            if (_groundProvider.IsGround)
            {
                _animator.CrossFade(_jumpAnimationHash, _animationSmooth);
            }
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

        private void SetWalkDirectionFromAngle(float speed)
        {
            Vector3 relativeVelocity = transform.InverseTransformVector(_inputService.GetInputXZDirection());
            Vector2 desiredVelocity = new Vector2(relativeVelocity.x, relativeVelocity.z)  * speed;
            
            _currentVelocityBlendValue = Vector2.SmoothDamp(_currentVelocityBlendValue, 
                desiredVelocity, 
                ref _refVelocity,
                _animationSmooth);
            
            _animator.SetFloat(_velocityX, _currentVelocityBlendValue.x);
            _animator.SetFloat(_velocityY, _currentVelocityBlendValue.y);
        }
    }
}
