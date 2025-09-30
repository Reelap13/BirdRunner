using UnityEngine;
using Mirror;
using Game.PlayerSide.Character;

namespace Game.Level.Obstacles
{
    public class BigSmallPortal : NetworkBehaviour
    {
        [SerializeField] private bool isSeting;
        private bool isTriggered;
        private bool isBig;

        private void OnTriggerEnter(Collider other)
        {
            if (!isServer) return;
            if (!other.TryGetComponent(out CharacterMovement controller)) return;
            Debug.Log("trigger");
            if (isSeting)
            {
                if (isTriggered)
                {
                    controller.SetSize(!isBig);
                }
                else
                {
                    if (Random.value > 0.5f)
                    {
                        isBig = true;
                        controller.SetSize(isBig);
                    }
                    else
                    {
                        isBig = false;
                        controller.SetSize(isBig);
                    }
                }
                isTriggered = true;
            }
            else
            {
                controller.ResetSize();
            }


        }
    }
}
