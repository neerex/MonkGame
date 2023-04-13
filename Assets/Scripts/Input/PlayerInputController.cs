using UnityEngine;
using UnityEngine.InputSystem;

namespace MainGame.Input
{
    public class PlayerInputController : MonoBehaviour
    {
        private GameInput _gameInput;
        private IControllable _controllable;

        private void Awake()
        {
            _gameInput = new GameInput();
            _controllable = GetComponent<IControllable>();
            _gameInput.Enable();
        }

        private void OnEnable()
        {
            _gameInput.Gameplay.Jump.performed += JumpPerformed;
        }

        private void OnDisable()
        {
            _gameInput.Gameplay.Jump.performed -= JumpPerformed;
        }

        private void Update()
        {
            var movementVector = _gameInput.Gameplay.Movement.ReadValue<Vector2>();
            var direction = new Vector3(movementVector.x, 0, movementVector.y);
            direction.Normalize();
            _controllable.Move(direction);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public Vector2 GetMousePosition() => 
            _gameInput.Gameplay.MousePosition.ReadValue<Vector2>();

        private void JumpPerformed(InputAction.CallbackContext callback) => 
            _controllable.Jump();
    }
}