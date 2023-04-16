using MainGame.Collisions.Triggers;
using UnityEngine;

namespace MainGame.Pickups
{
    public class PickupCollector : MonoBehaviour
    {
        [SerializeField] private SphereTriggerObserver _pickupTrigger;

        private void OnEnable()
        {
            _pickupTrigger.TriggerEnter += PerformPickup;
        }

        private void OnDisable()
        {
            _pickupTrigger.TriggerEnter -= PerformPickup;
        }

        private void PerformPickup(Collider pickupCollider)
        {
            if (pickupCollider.TryGetComponent<Pickup>(out var pickup))
            {
                pickup.PerformPickup(gameObject);
            }
        }
    }
}
