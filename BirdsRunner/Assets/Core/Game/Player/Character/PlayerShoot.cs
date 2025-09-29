using UnityEngine;
using Mirror;
using System.Collections;

namespace Game.PlayerSide.Character
{

    public class PlayerShoot : NetworkBehaviour
    {
        [SerializeField] private GameObject featherPrefab;
        [SerializeField] private Transform shootingPoint;
        [SerializeField] private float shootingForce;
        [SerializeField] private ForceMode forceMode;

        private float coolDown = 0.2f;
        private float readyBufferTime = 0.1f;

        private float lastClickTime = 0f;

        private bool isShooting;

        public void Start()
        {
            if (!isOwned) return;
            InputManager.Instance.GetControls().Player.Attack.started += _ => HandleFire();
        }

        public void HandleFire()
        {
            if (!isShooting || Time.time - lastClickTime <= readyBufferTime)
            {
                Fire();
            }
            else
            {
                lastClickTime = Time.time;
            }
        }

        private void Fire()
        {
            isShooting = true;
            CmdFire();
            StartCoroutine(CoolDownCoroutine());
        }

        [Command]
        private void CmdFire()
        {
            GameObject feather = NetworkUtils.NetworkInstantiate(featherPrefab, shootingPoint);
            feather.GetComponent<Rigidbody>().AddForce(transform.forward * shootingForce, forceMode);
        }

        private IEnumerator CoolDownCoroutine()
        {
            yield return new WaitForSeconds(coolDown);
            isShooting = false;
            lastClickTime = 0f;
        }

        public bool IsShooting()
        {
            return isShooting;
        }
        public float CoolDown { get { return coolDown; } }

    }
}