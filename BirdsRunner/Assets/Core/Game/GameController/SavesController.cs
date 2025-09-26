using System;
using System.Runtime.InteropServices.WindowsRuntime;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class SavesController : Singleton<SavesController>
    {
        [NonSerialized] public UnityEvent OnLoadingStarted = new();
        [NonSerialized] public UnityEvent OnSavingStarted = new();

        public void LoadGame() => OnLoadingStarted.Invoke();
        public void SaveGame() => OnSavingStarted.Invoke(); 

        public bool IsHasSave() => false;
    }
}