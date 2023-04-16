using UnityEngine;

namespace MainGame.Pickups
{
    public abstract class PickupEffect : MonoBehaviour
    {
        public abstract void ApplyEffect(GameObject go);
    }
}