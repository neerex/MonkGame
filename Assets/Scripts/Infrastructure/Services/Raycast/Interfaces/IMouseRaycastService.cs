using UnityEngine;

namespace MainGame.Infrastructure.Services.Raycast.Interfaces
{
    public interface IMouseRaycastService
    {
        Vector3 GetDirectionToRaycastHit(Vector3 fromPosition);
        RaycastHit GetPointOnSurface(LayerMask layerMask);
    }
}