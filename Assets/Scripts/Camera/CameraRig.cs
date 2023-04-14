using UnityEngine;

namespace MainGame.Camera
{
    public class CameraRig : MonoBehaviour
    {
        [field:SerializeField] public Transform Distance { get; private set; }
        [field:SerializeField] public Transform Rotation { get; private set; }
        [field:SerializeField] public UnityEngine.Camera Camera { get; private set; }
    }
}