using UnityEngine;

namespace Scripts.UI.Scene
{
    public class SceneUI : Singleton<SceneUI>
    {
        [field: SerializeField]
        public SceneFader Fader { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}