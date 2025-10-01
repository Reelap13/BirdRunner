using System;
using Game.PlayerSide.Character;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Game.PlayerSide
{
    public class PlayerHealth : MonoBehaviour
    {
        [field: SerializeField]
        public PlayerController Controller { get; private set; }

        [NonSerialized] public UnityEvent OnDied = new();

        [SerializeField] private int _start_health = 3;

        [SerializeField] private float invincibilityTime = 1f;

        private int _health;

        private bool isInvincible;

        private void Awake()
        {
            Controller.CharacterCreator.OnCharacterCreated.AddListener(InitializeNewCharacter);
        }

        private void InitializeNewCharacter(PlayerCharacterController character)
        {
            character.DamageTaker.OnDamageTaker.AddListener(TakeDamage);
        }

        public void RestoreHealth()
        {
            isInvincible = false;
            _health = _start_health;
        }

        private void TakeDamage(int damage)
        {
            if (!IsAlive)
                return;

            if (isInvincible) return;

            _health -= damage;
            if (!IsAlive)
            {
                Controller.CharacterCreator.Character.State = CharacterState.DEAD;
                OnDied.Invoke();
            }
            else
            {
                StartCoroutine(InvincibilityPeriod());
            }
        }

        private IEnumerator InvincibilityPeriod()
        {
            isInvincible = true;
            yield return new WaitForSeconds(invincibilityTime);
            isInvincible = false;
        }

        public bool IsAlive => _health > 0;
        public int Health { get { return _health; } }
    }
}