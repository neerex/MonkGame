using UnityEngine;

namespace MainGame.Pickups
{
    public class Pickup : MonoBehaviour
    {
        public void PerformPickup(GameObject pickupRequester)
        {
            var pickupEffects = GetComponents<PickupEffect>();
            foreach (var effect in pickupEffects)
            {
                effect.ApplyEffect(pickupRequester);
            }
            Destroy(gameObject);
        }
    }
}
