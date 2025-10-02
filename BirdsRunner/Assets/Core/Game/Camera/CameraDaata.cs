using UnityEngine;

public class CameraDaata : MonoBehaviour
{
    [SerializeField] private GameObject _effects;

    private void Awake()
    {
        _effects.SetActive(false);
    }

    public void SetActiveEffects(bool active)
    {
        _effects.SetActive(active);
    }
}
