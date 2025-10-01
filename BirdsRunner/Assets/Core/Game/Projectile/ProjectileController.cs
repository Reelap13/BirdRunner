using Mirror;
using UnityEngine;

namespace Game.Projectile
{
    public class ProjectileController : NetworkBehaviour
    {
        private Rigidbody _rb;
        private float _speed;

        public void Initialize(float speed)
        {
            _speed = speed;
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (!isServer && _rb != null)
                return;

            _rb.MovePosition(transform.position + transform.forward * _speed * Time.fixedDeltaTime);
        }
    }
}