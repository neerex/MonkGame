using System;
using MainGame.Input;
using MainGame.Raycasts;
using UnityEngine;

namespace MainGame.Player
{
    public class Player : MonoBehaviour, IControllable
    {
        [SerializeField] private float _speed;
        private CharacterController _characterController;
        private IMouseRaycastDirectionProvider _lookAtDirection;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _lookAtDirection = GetComponent<IMouseRaycastDirectionProvider>();
        }

        private void Update()
        {
            LookAtCursor();
        }

        public void Move(Vector3 direction)
        {
            var speed = Time.deltaTime * _speed;
            _characterController.Move(direction * speed);
        }

        public void Jump()
        {
            Debug.Log("Jump");
        }

        private void LookAtCursor()
        {
            var directionToLookAt = _lookAtDirection.GetDirectionToRaycastHit(transform.position);
            transform.rotation = Quaternion.LookRotation(directionToLookAt, Vector3.up);
        }
    }
}
