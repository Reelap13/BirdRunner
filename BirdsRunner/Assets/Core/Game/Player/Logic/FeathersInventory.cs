using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game.PlayerSide
{
    public class FeathersInventory : MonoBehaviour
    {
        [NonSerialized] public UnityEvent OnUpdating = new();

        [field: SerializeField]
        public PlayerController Controller { get; private set; }

        [SerializeField] private float _max_amount = 3f;
        [SerializeField] private float _restoring_time = 2f;

        private float _amount;

        private void Awake()
        {
            Controller.OnServerInitialized.AddListener(Initailize);
        }

        private void Initailize()
        {
            _amount = _max_amount;
            StartCoroutine(Restore());
        }

        public bool TryToSpendFeather()
        {
            if (_amount < 1)
                return false;

            _amount -= 1;
            return true;
        }

        private IEnumerator Restore()
        {
            while (true)
            {
                yield return null;
                if (_amount >= _max_amount)
                    continue;

                _amount += Time.deltaTime / _restoring_time;
                _amount = Mathf.Clamp(_amount, 0, _max_amount);
            }
        }

        public float Amount { get { return _amount; } }
    }
}