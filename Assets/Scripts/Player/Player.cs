using MainGame.Input;
using MainGame.Raycasts;
using UnityEngine;

namespace MainGame.Player
{
    public class Player : MonoBehaviour, IControllable
    {
        [SerializeField] private float _movementSpeed = 10f;
        [SerializeField] private float _jumpSpeed = 2f;
        [SerializeField] private float _rotationSpeed = 10f;
        
        private Rigidbody _rigidbody;
        private IMouseRaycastDirectionProvider _lookAtDirection;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _lookAtDirection = GetComponent<IMouseRaycastDirectionProvider>();
        }

        private void FixedUpdate()
        {
            LookAtCursor();
        }

        public void Move(Vector3 direction)
        {
            var velocity = _movementSpeed * direction;
            velocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = velocity;
        }

        public void Jump()
        {
            _rigidbody.velocity += Vector3.up * _jumpSpeed;
        }

        private void LookAtCursor()
        {
            var directionToLookAt = _lookAtDirection.GetDirectionToRaycastHit(transform.position);
            var cross = Vector3.Cross(transform.forward, directionToLookAt);
            _rigidbody.angularVelocity = new Vector3(0, cross.y * _rotationSpeed, 0);
        }
    }
}
