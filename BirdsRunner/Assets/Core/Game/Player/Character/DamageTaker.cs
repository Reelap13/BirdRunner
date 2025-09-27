using System;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Game.PlayerSide.Character
{
    public class DamageTaker : MonoBehaviour
    {
        [NonSerialized] public UnityEvent<int> OnDamageTaker = new();

        [field: SerializeField]
        public PlayerCharacterController Character { get; private set; }

        public void TakeDamage(int damage = 1)
        {
            if (Character.State != CharacterState.DEAD)
                OnDamageTaker.Invoke(damage);
        }

    }
}