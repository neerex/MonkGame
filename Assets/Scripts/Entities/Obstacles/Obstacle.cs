using MainGame.Damage.Effects;
using UnityEngine;

namespace MainGame.Entities.Obstacles
{
    public class Obstacle : MonoBehaviour, IPushable
    {
        [SerializeField] private Rigidbody _rigidbody;
        public Transform Transform => transform;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            
            //temporary
            transform.rotation = Quaternion.Euler(Random.Range(0, 180f), Random.Range(0, 180f), Random.Range(0, 180f));
        }


        public void Push(Vector3 direction, float force)
        {
            _rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}
