using UnityEngine;
using UnityEngine.InputSystem;

namespace MainGame.Infrastructure.Services.Input.Interfaces
{
    public interface IPlayerInputService
    {
        event System.Action<InputAction.CallbackContext> OnJumpInputPerformed;
        event System.Action<InputAction.CallbackContext> OnMainAttackPerformed;
        event System.Action<InputAction.CallbackContext> OnSecondaryAttackPerformed;
        event System.Action<InputAction.CallbackContext> OnAttack1Performed;
        event System.Action<InputAction.CallbackContext> OnAttack2Performed;
        event System.Action<InputAction.CallbackContext> OnAttack3Performed;
        event System.Action<InputAction.CallbackContext> OnAttack4Performed;
        
        Vector3 GetInputXZDirection();
        Vector2 GetMousePosition();
        void SubscribeOnInputCallbacks();
        void UnsubscribeFromInputCallbacks();
    }
}