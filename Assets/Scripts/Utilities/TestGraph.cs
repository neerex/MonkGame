using UnityEngine;

namespace MainGame.Utilities
{
    public class TestGraph : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;

        private void Update()
        {
            Keyframe keyframe = new Keyframe(Time.time, transform.rotation.y, 0, 0, 0, 0);
            _curve.AddKey(keyframe);
        }
    }
}
