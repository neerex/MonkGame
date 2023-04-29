using UnityEngine;

namespace MainGame.Damage.Effects
{
    public interface IPushable
    {
        Transform Transform { get; }
        void Push(Vector3 direction, float force);
    }
}