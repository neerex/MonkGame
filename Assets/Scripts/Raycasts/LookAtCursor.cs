using MainGame.Input;
using MainGame.Utilities;
using UnityEngine;

namespace MainGame.Raycasts
{
    public class LookAtCursor : MonoBehaviour, IMouseRaycastDirectionProvider
    {
        private UnityEngine.Camera _camera;
        private Plane _plane;
        private PlayerInputController _inputController;

        private void Awake()
        {
            _inputController = GetComponent<PlayerInputController>();
            _camera = UnityEngine.Camera.main;
            _plane = new Plane(Vector3.up, Vector3.zero);
        }

        public Vector3 GetDirectionToRaycastHit(Vector3 fromPosition)
        {
            Ray ray = _camera.ScreenPointToRay(_inputController.GetMousePosition());
            _plane.Raycast(ray, out float enter);
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 direction = (hitPoint.FlatY() - fromPosition.FlatY()).normalized;
            return direction;
        }
    }
}