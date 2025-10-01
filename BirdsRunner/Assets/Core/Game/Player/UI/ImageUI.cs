using UnityEngine;

namespace Game.PlayerSide.UI
{
    public class ImageUI : MonoBehaviour
    {
        [SerializeField] private GameObject _active_image;
        [SerializeField] private GameObject _disactive_image;

        public void SetState(bool state)
        {
            _active_image.SetActive(false);
            _disactive_image.SetActive(false);

            if (state)
                _active_image.SetActive(true);
            else _disactive_image.SetActive(true);
        }
    }
}