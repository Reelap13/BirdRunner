using Game.PlayerSide.Character;
using Mirror;
using UnityEngine;

namespace Game.Level.Obstacles
{
    public class DamageDealer : NetworkBehaviour
    {
        /*private void OnTriggerEnter(Collider other)
        {
            if (!isServer)
                return;

            if (other.TryGetComponent<DamageTaker>(out DamageTaker damage_taker))
                damage_taker.TakeDamage();
        }*/

        private void OnCollisionEnter(Collision collision)
        {
            if (!isServer)
                return;

            if (collision.collider.TryGetComponent<DamageTaker>(out DamageTaker damage_taker))
                damage_taker.TakeDamage();
        }
    }
}