using UnityEngine;

namespace MainGame.Raycasts
{
    public interface IMouseRaycastDirectionProvider
    {
        Vector3 GetDirectionToRaycastHit(Vector3 fromPosition);
    }
}