﻿using UnityEngine;
using UnityEngine.InputSystem;

namespace MainGame.Input
{
    public interface IPlayerInputService
    {
        event System.Action<InputAction.CallbackContext> OnJumpInputPerformed;
        Vector3 GetInputDirection();
        Vector2 GetMousePosition();
        void SubscribeOnInputCallbacks();
        void UnsubscribeFromInputCallbacks();
    }
}