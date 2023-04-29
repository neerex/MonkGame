using UnityEngine;

namespace MainGame.Entities.Player
{
    public class IsGroundProvider : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _footTransform;
        [SerializeField] private Vector3 _groundCheckBoxSize = new Vector3(0.6f, 0.3f, 0.4f);
        
        public bool IsGround { get; private set; }

        private void Update()
        {
            IsGround = Physics.CheckBox(_footTransform.position, 
                _groundCheckBoxSize / 2, 
                _footTransform.rotation,
                _groundLayer);
        }

        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(_footTransform.localPosition, _groundCheckBoxSize);
        }
    }
}