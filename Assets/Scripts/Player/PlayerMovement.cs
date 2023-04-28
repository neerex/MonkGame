using MainGame.Services.Input.Interfaces;
using MainGame.Services.Raycast.Interfaces;
using MainGame.Stats;
using MainGame.Stats.ConcreteStat;
using MainGame.Stats.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace MainGame.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(IStatHolder))]
    [RequireComponent(typeof(IsGroundProvider))]
    public class PlayerMovement : MonoBehaviour, IStatsReader
    {
        [SerializeField] private float _jumpSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 10f;
        
        private Rigidbody _rigidbody;
        private IStatHolder _statHolder;
        private IsGroundProvider _isGroundProvider;
        
        private IPlayerInputService _inputService;
        private IMouseRaycastService _mouseRaycastService;

        private MovementSpeedStat _movementSpeed;

        [Inject]
        public void Construct(IPlayerInputService inputService, IMouseRaycastService mouseRaycastService)
        {
            _inputService = inputService;
            _mouseRaycastService = mouseRaycastService;
            
            _inputService.OnJumpInputPerformed += Jump;
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _statHolder = GetComponent<IStatHolder>();
            _isGroundProvider = GetComponent<IsGroundProvider>();
        }
        
        private void OnDestroy()
        {
            _inputService.OnJumpInputPerformed -= Jump;
        }
        
        private void FixedUpdate()
        {
            Move();
            LookAtCursor();
        }

        public void InitializeStats() => 
            _statHolder.GetStat(out _movementSpeed);

        private void Move()
        {
            if(_movementSpeed is null) return;
            var direction = _inputService.GetInputDirection();
            var velocity = _movementSpeed.Value * direction;
            velocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = velocity;
        }

        private void Jump(InputAction.CallbackContext callbackContext)
        {
            if(_isGroundProvider.IsGround)
                _rigidbody.velocity += Vector3.up * _jumpSpeed;
        }

        private void LookAtCursor()
        {
            var directionToLookAt = _mouseRaycastService.GetDirectionToRaycastHit(transform.position);
            var cross = Vector3.Cross(transform.forward, directionToLookAt);
            _rigidbody.angularVelocity = new Vector3(0, cross.y * _rotationSpeed, 0);
        }
    }
}