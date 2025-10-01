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
        [SerializeField] private int _damagable_layer;
        [SerializeField] private int _undamagable_layer;

        public void TakeDamage(int damage = 1)
        {
            if (Character.State != CharacterState.DEAD)
                OnDamageTaker.Invoke(damage);
        }


        public void SetUndamageState()
        {
            gameObject.layer = _undamagable_layer;
        }

        public void UnSetUndamageState()
        {
            gameObject.layer = _damagable_layer;
        }
    }
}