using System;
using UnityEngine;

namespace MainGame.Collisions.Triggers
{
    public abstract class TriggerObserver : MonoBehaviour
    {
        [SerializeField] protected Color _triggerZoneColor;
        
        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;

        protected virtual void Awake() {}

        protected virtual void OnValidate() {}

        private void OnTriggerEnter(Collider other) => 
            TriggerEnter?.Invoke(other);

        private void OnTriggerExit(Collider other) => 
            TriggerExit?.Invoke(other);

        protected virtual void OnDrawGizmos() {}
    }
}