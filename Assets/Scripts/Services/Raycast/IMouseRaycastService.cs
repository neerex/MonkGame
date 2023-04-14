using UnityEngine;

namespace MainGame.Services.Raycast
{
    public interface IMouseRaycastService
    {
        Vector3 GetDirectionToRaycastHit(Vector3 fromPosition);
    }
}