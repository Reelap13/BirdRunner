using System;
using Game.Level;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Cutscene
{
    public class CutsceneController : GameSystem
    {
        [NonSerialized] public UnityEvent OnCutsceneFinished = new();

        protected override void Initialize()
        {
            Game.Level.OnLoaded.AddListener(ShowCutscene);
        }

        private void ShowCutscene()
        {
            OnCutsceneFinished.Invoke();
        }
    }
}