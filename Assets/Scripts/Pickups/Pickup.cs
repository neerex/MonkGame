using UnityEngine;

namespace MainGame.Pickups
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _applyVfx;
        
        public void PerformPickup(GameObject pickupRequester)
        {
            PickupEffect[] pickupEffects = GetComponents<PickupEffect>();
            foreach (var effect in pickupEffects)
            {
                effect.ApplyEffect(pickupRequester);
            }

            InstantiateApplyVfs();
            Destroy(gameObject);
        }

        private void InstantiateApplyVfs()
        {
            if (_applyVfx != null)
            {
                ParticleSystem particles = Instantiate(_applyVfx, transform.position, Quaternion.identity);
                particles.transform.localScale *= 3f;
            }
        }
    }
}
