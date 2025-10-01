using System;
using System.Collections;
using JetBrains.Annotations;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Game.PlayerSide
{
    public class DataSynchronizer : NetworkBehaviour
    {
        [NonSerialized] public UnityEvent<SynchronizeData> OnDataSynchronized = new();

        [field: SerializeField]
        public PlayerController Controller { get; private set; }

        private SynchronizeData _data;

        private void Awake()
        {
            Controller.OnServerInitialized.AddListener(Initialize);
        }

        private void Initialize()
        {
            StartCoroutine(Synchronize());
        }

        private IEnumerator Synchronize()
        {
            while (true)
            {
                yield return null;
                var data = PrepareSynchronizeData();
                TargetSynchronizeData(data);
            }

        }

        private SynchronizeData PrepareSynchronizeData()
        {
            return new SynchronizeData()
            {
                Health = Controller.Health.Health,
                Feathers = Controller.Feathers.Amount,
            };
        }

        [TargetRpc]
        public void TargetSynchronizeData(SynchronizeData data)
        {
            _data = data;
            OnDataSynchronized.Invoke(data);
        }

        [Serializable]
        public class SynchronizeData
        {
            public int Health;
            public float Feathers;
        }

        public SynchronizeData Data { get; private set; }
    }
}