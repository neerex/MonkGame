using UnityEngine;

namespace MainGame.Player.Animation
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerAnimator : MonoBehaviour
    {
        private readonly int _walkingSpeedHash = Animator.StringToHash("CharacterSpeed");

        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;

        private void Update()
        {
            _animator.SetFloat(_walkingSpeedHash, 
                _rigidbody.velocity.magnitude, 
                0.1f, 
                Time.deltaTime);
        }
    }
}
