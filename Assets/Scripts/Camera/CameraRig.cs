using UnityEngine;

namespace MainGame.Camera
{
    [RequireComponent(typeof(CameraFollow))]
    public class CameraRig : MonoBehaviour
    {
        [field:SerializeField] public Transform Distance { get; private set; }
        [field:SerializeField] public Transform Rotation { get; private set; }
        [field:SerializeField] public UnityEngine.Camera Camera { get; private set; }
    }
}