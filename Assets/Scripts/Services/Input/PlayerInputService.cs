using System;
using MainGame.Services.Input.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MainGame.Services.Input
{
    public class PlayerInputService : IPlayerInputService
    {
        private readonly GameInput _gameInput;
        public event Action<InputAction.CallbackContext> OnJumpInputPerformed;

        public PlayerInputService()
        {
            _gameInput = new GameInput();
            
            _gameInput.Enable();
            SubscribeOnInputCallbacks();
        }
        
        public Vector3 GetInputDirection()
        {
            var movementVector = _gameInput.Gameplay.Movement.ReadValue<Vector2>();
            var direction = new Vector3(movementVector.x, 0, movementVector.y);
            direction.Normalize();
            return direction;
        }

        public Vector2 GetMousePosition() => 
            _gameInput.Gameplay.MousePosition.ReadValue<Vector2>();

        public void SubscribeOnInputCallbacks()
        {
            _gameInput.Gameplay.Jump.performed += JumpPerformed;
        }

        public void UnsubscribeFromInputCallbacks()
        {
            _gameInput.Gameplay.Jump.performed -= JumpPerformed;
        }

        private void JumpPerformed(InputAction.CallbackContext callback) => 
            OnJumpInputPerformed?.Invoke(callback);
    }
}