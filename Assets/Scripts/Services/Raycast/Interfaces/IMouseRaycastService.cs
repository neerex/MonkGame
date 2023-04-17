using UnityEngine;

namespace MainGame.Services.Raycast.Interfaces
{
    public interface IMouseRaycastService
    {
        Vector3 GetDirectionToRaycastHit(Vector3 fromPosition);
    }
}