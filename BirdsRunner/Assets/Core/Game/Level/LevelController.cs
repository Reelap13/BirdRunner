using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Level
{
    public class LevelController : MonoBehaviour
    {
        [NonSerialized] public UnityEvent OnInitialized = new();

        public void Initialize()
        {
            OnInitialized.Invoke();
        }
    }
}