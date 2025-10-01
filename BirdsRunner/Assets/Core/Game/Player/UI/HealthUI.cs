using System.Collections.Generic;
using UnityEngine;
using static Game.PlayerSide.DataSynchronizer;

namespace Game.PlayerSide.UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private DataSynchronizer _synchronizer;
        [SerializeField] private List<ImageUI> _healths;

        private void Awake()
        {
            _synchronizer.OnDataSynchronized.AddListener(UpdateUI);
        }

        private void UpdateUI(SynchronizeData data)
        {
            if (data == null) return;
            for (int i = 0; i < _healths.Count; ++i)
            {
                _healths[i].SetState(data.Health >= i + 1);
            }
        }
    }
}