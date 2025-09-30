using UnityEngine;
using Mirror;

namespace Game.GameMode
{
    public class PlayerRopeController : NetworkBehaviour
    {
        [SerializeField] private ConfigurableJoint spring;
        [SerializeField] private float springForce;

        public void Start()
        {
            if (!isOwned) return;
            DiactivateRope();
        }

        public void ActivateRope(GameObject other)
        {
            Debug.Log("activate");
            spring.connectedBody = other.GetComponent<Rigidbody>();
        }

        public void DiactivateRope()
        {
            spring.connectedBody = null;
        }
    }
}
