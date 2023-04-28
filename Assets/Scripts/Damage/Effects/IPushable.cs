using UnityEngine;

namespace MainGame.Damage.Effects
{
    public interface IPushable
    {
        void Push(Vector3 direction, float force);
    }
}