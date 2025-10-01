using UnityEngine;
using Game.PlayerSide;
using Game.PlayerSide.Character;
using Mirror;
using System.Collections;
using System.Collections.Generic;

namespace Game.Level.Obstacles
{
    public class LinesController : NetworkBehaviour
    {
        [SerializeField] private float interactionTime;
        [SerializeField] private ParticleSystem particles;

        [SerializeField] private Color firstColor;
        [SerializeField] private Color secondColor;
        [SerializeField] private Color doneColor;

        private float timer;
        private List<PlayerCharacterController> birds = new();
        private bool isWorking;
        private bool isTriggger;
        private ParticleSystem.MainModule mainModule;

        public void Start()
        {
            mainModule = particles.main;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (!isServer)
                return;
            if (!other.TryGetComponent(out PlayerCharacterController controller)) return;
            birds.Add(controller);
            if (birds.Count != 2) return;
            if (isTriggger) return;
            isTriggger = true;
            isWorking = true;
            timer = 0f;
            StartCoroutine(StartTimer());

        }

        private void OnTriggerExit(Collider other)
        {
            if (!isServer)
                return;
            if (!other.TryGetComponent(out PlayerCharacterController controller)) return;
            isWorking = false;
            if(timer < interactionTime)
            {
                
                foreach (var item in birds)
                {
                    item.GetComponent<DamageTaker>().TakeDamage(1);
                }
            }
        }
        private IEnumerator StartTimer()
        {
            while (true)
            {
                timer += Time.deltaTime;
                timer = Mathf.Clamp(timer, 0f, interactionTime);

                float t = timer / interactionTime;
                Color currentColor = Color.Lerp(firstColor, secondColor, t);

                RpcChangeColor(currentColor);
                if (!isWorking)
                {
                    break;
                }
                if(timer >= interactionTime)
                {
                    RpcChangeColor(doneColor);
                    break;
                }
                yield return new WaitForEndOfFrame();
            }

        }

        [ClientRpc]
        private void RpcChangeColor(Color color)
        {
            mainModule.startColor = color;
        }


    }
}
