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
    public class PlayerAnimator : MonoBehaviour, IStatsReader
    {
        private readonly int _walkingSpeed = Animator.StringToHash("SpeedPercent");
        private readonly int _velocityX = Animator.StringToHash("VelocityX");
        private readonly int _velocityY = Animator.StringToHash("VelocityY");
        private readonly int _jumpTrigger = Animator.StringToHash("Jump");
        private readonly int _isGrounded = Animator.StringToHash("IsGrounded");
        
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private IsGroundProvider _groundProvider;

        private ICharacterStatHolder _characterStatHolder;
        private IPlayerInputService _inputService;
        
        private MovementSpeedStat _movementSpeed;

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
        }

        private void Update()
        {
            if(_movementSpeed == null || _inputService == null) return;
            _animator.SetBool(_isGrounded, _groundProvider.IsGround);
            float speedPercent = GetSpeedPercent();
            SetWalkDirectionFromAngle(AngleBetweenLookAndVelocity(), speedPercent);
            _animator.SetFloat(_walkingSpeed, speedPercent , 0.1f, Time.deltaTime);
        }

        private void OnDestroy()
        {
            _inputService.OnJumpInputPerformed -= StartJumpAnimation;
        }

        public void InitializeStats() => 
            _characterStatHolder.GetStat(out _movementSpeed);

        private void StartJumpAnimation(InputAction.CallbackContext context)
        {
            if(_groundProvider != null && _groundProvider.IsGround)
                _animator.SetTrigger(_jumpTrigger);
        }

        private float AngleBetweenLookAndVelocity() => 
            Vector3.SignedAngle(transform.forward.FlatY(), _inputService.GetInputDirection(), Vector3.up);

        private float GetSpeedPercent() => 
            _rigidbody.velocity.FlatY().magnitude / _movementSpeed.Value;

        private void SetWalkDirectionFromAngle(float angle, float speed)
        {
            var velocity = Quaternion.AngleAxis(-angle, Vector3.forward) * Vector3.up * speed;
            _animator.SetFloat(_velocityX, velocity.x);
            _animator.SetFloat(_velocityY, velocity.y);
        }
    }
}
