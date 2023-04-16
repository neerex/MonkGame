using UnityEngine;

namespace MainGame.Collisions.Triggers
{
    public class BoxTriggerObserver : TriggerObserver
    {
        private BoxCollider _boxCollider;

        protected override void Awake()
        {
            base.Awake();
            _boxCollider = GetComponent<BoxCollider>();
        }
        
        protected override void OnValidate()
        {
            base.OnValidate();
            _boxCollider ??= GetComponent<BoxCollider>();
        }
        
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            if(_boxCollider == null) return;
            Gizmos.matrix = transform.localToWorldMatrix;
            
            Vector3 center = _boxCollider.center;
            Vector3 size = _boxCollider.size;
            
            Gizmos.color = _triggerZoneColor;
            Gizmos.DrawCube(center, size);
        }
    }
}