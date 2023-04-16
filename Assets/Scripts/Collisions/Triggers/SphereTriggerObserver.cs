using UnityEngine;

namespace MainGame.Collisions.Triggers
{
    [RequireComponent(typeof(SphereCollider))]
    public class SphereTriggerObserver : TriggerObserver
    {
        private SphereCollider _sphereCollider;

        protected override void Awake()
        {
            base.Awake();
            _sphereCollider = GetComponent<SphereCollider>();
        }
        
        protected override void OnValidate()
        {
            base.OnValidate();
            _sphereCollider ??= GetComponent<SphereCollider>();
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            if(_sphereCollider == null) return;
            Gizmos.matrix = transform.localToWorldMatrix;
            
            Vector3 center = _sphereCollider.center;
            float radius = _sphereCollider.radius;
            
            Gizmos.color = _triggerZoneColor;
            Gizmos.DrawSphere(center, radius);
        }
    }
}