using UnityEngine;
using Mirror;
using System.Collections;
using Game.Projectile;

namespace Game.PlayerSide.Character
{

    public class PlayerShoot : NetworkBehaviour
    {
        [SerializeField] private PlayerCharacterController _controller;
        [SerializeField] private ProjectileController featherPrefab;
        [SerializeField] private Transform shootingPoint;
        [SerializeField] private float projectileSpeed = 10f;

        private float coolDown = 0.2f;
        private bool isShooting = false;

        public void TryToFire()
        {
            if (!isShooting)
                Fire();
        }

        private void Fire()
        {
            if (!_controller.PlayerController.Feathers.TryToSpendFeather())
                return;

            isShooting = true;
            ProjectileController feather = NetworkUtils.NetworkInstantiate(featherPrefab, shootingPoint);
            feather.Initialize(projectileSpeed);
            StartCoroutine(CoolDownCoroutine());
        }

        private IEnumerator CoolDownCoroutine()
        {
            yield return new WaitForSeconds(coolDown);
            isShooting = false;
        }

    }
}