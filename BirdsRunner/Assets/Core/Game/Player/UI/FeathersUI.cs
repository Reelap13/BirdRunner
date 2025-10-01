using System.Collections.Generic;
using UnityEngine;
using static Game.PlayerSide.DataSynchronizer;

namespace Game.PlayerSide.UI
{
    public class FeathersUI : MonoBehaviour
    {
        [SerializeField] private DataSynchronizer _synchronizer;
        [SerializeField] private List<ImageUI> _feathers;

        private void Awake()
        {
            _synchronizer.OnDataSynchronized.AddListener(UpdateUI);
        }

        private void UpdateUI(SynchronizeData data)
        {
            if (data == null) return;
            for (int i = 0; i < _feathers.Count; ++i)
            {
                _feathers[i].SetState(data.Feathers >= i + 1);
            }
        }
    }
}