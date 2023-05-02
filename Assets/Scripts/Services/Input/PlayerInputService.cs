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
        public event Action<InputAction.CallbackContext> OnMainAttackPerformed;
        public event Action<InputAction.CallbackContext> OnSecondaryAttackPerformed;
        public event Action<InputAction.CallbackContext> OnAttack1Performed;
        public event Action<InputAction.CallbackContext> OnAttack2Performed;
        public event Action<InputAction.CallbackContext> OnAttack3Performed;
        public event Action<InputAction.CallbackContext> OnAttack4Performed;

        public PlayerInputService()
        {
            _gameInput = new GameInput();
            
            _gameInput.Enable();
            SubscribeOnInputCallbacks();
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public Vector3 GetInputXZDirection()
        {
            var movementVector = _gameInput.Gameplay.Movement.ReadValue<Vector2>();
            var direction = new Vector3(movementVector.x, 0, movementVector.y);
            direction.Normalize();
            return direction;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public Vector2 GetMousePosition() => 
            _gameInput.Gameplay.MousePosition.ReadValue<Vector2>();

        public void SubscribeOnInputCallbacks()
        {
            _gameInput.Gameplay.Jump.performed += JumpPerformed;
            
            _gameInput.Gameplay.MainAttack.performed += MainAttackPerformed;
            _gameInput.Gameplay.SecondaryAttack.performed += SecondaryAttackPerformed;
            
            _gameInput.Gameplay.Attack1.performed += Attack1Performed;
            _gameInput.Gameplay.Attack2.performed += Attack2Performed;
            _gameInput.Gameplay.Attack3.performed += Attack3Performed;
            _gameInput.Gameplay.Attack4.performed += Attack4Performed;
        }

        public void UnsubscribeFromInputCallbacks()
        {
            _gameInput.Gameplay.Jump.performed -= JumpPerformed;
            
            _gameInput.Gameplay.MainAttack.performed -= MainAttackPerformed;
            _gameInput.Gameplay.SecondaryAttack.performed -= SecondaryAttackPerformed;
            
            _gameInput.Gameplay.Attack1.performed -= Attack1Performed;
            _gameInput.Gameplay.Attack2.performed -= Attack2Performed;
            _gameInput.Gameplay.Attack3.performed -= Attack3Performed;
            _gameInput.Gameplay.Attack4.performed -= Attack4Performed;
        }

        private void JumpPerformed(InputAction.CallbackContext callback) => 
            OnJumpInputPerformed?.Invoke(callback);
        
        private void MainAttackPerformed(InputAction.CallbackContext callback) => 
            OnMainAttackPerformed?.Invoke(callback);
        
        private void SecondaryAttackPerformed(InputAction.CallbackContext callback) => 
            OnSecondaryAttackPerformed?.Invoke(callback);
        
        private void Attack1Performed(InputAction.CallbackContext callback) => 
            OnAttack1Performed?.Invoke(callback);
        
        private void Attack2Performed(InputAction.CallbackContext callback) => 
            OnAttack2Performed?.Invoke(callback);

        private void Attack3Performed(InputAction.CallbackContext callback) =>
            OnAttack3Performed?.Invoke(callback);
        
        private void Attack4Performed(InputAction.CallbackContext callback) => 
            OnAttack4Performed?.Invoke(callback);
    }
}