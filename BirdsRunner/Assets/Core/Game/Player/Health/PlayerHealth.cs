using System;
using Game.PlayerSide.Character;
using UnityEngine;
using UnityEngine.Events;

namespace Game.PlayerSide.Health
{
    public class PlayerHealth : MonoBehaviour
    {
        [field: SerializeField]
        public PlayerController Controller { get; private set; }

        [NonSerialized] public UnityEvent OnDied = new();

        [SerializeField] private int _start_health = 3;

        private int _health;

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
            _health = _start_health;
        }

        private void TakeDamage(int damage)
        {
            if (!IsAlive)
                return;

            _health -= damage;
            if (!IsAlive)
            {
                Controller.CharacterCreator.Character.State = CharacterState.DEAD;
                OnDied.Invoke();
            }
        }

        public bool IsAlive => _health > 0;
        public int Health { get { return _health; } }
    }
}