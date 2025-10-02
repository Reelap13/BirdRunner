using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Scene
{
    public class SceneFader : MonoBehaviour
    {
        [SerializeField] private Image _fade_image;
        [SerializeField] private float _fade_speed = 1f;

        private float _elapsed;

        private void Awake()
        {
            _elapsed = 0f;
            _fade_image.gameObject.SetActive(false);
        }

        public IEnumerator FadeOut()
        {
            _fade_image.gameObject.SetActive(true);
            float elapsed = _elapsed;
            Color color = _fade_image.color;

            while (elapsed < 1)
            {
                elapsed += Time.deltaTime / _fade_speed;
                _elapsed = elapsed;
                color.a = Mathf.Clamp01(elapsed);
                _fade_image.color = color;
                yield return null;
            }
            _elapsed = 1;
        }

        public IEnumerator FadeIn() 
        {
            float elapsed = _elapsed;
            Color color = _fade_image.color;

            while (elapsed > 0)
            {
                elapsed -= Time.deltaTime / _fade_speed;
                _elapsed = elapsed;
                color.a = Mathf.Clamp01(elapsed);
                _fade_image.color = color;
                yield return null;
            }
            _elapsed = 0;
            _fade_image.gameObject.SetActive(false);
        }

    }
}