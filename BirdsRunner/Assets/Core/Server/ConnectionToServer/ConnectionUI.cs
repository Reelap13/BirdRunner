using System.Collections;
using Scripts.UI.Scene;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionUI : MonoBehaviour
{
    [SerializeField] private ConnectorToServer _connector;
    [SerializeField] private Button _start_new_game;
    [SerializeField] private Button _continue;
    [SerializeField] private Button _connection;
    [SerializeField] private TMP_InputField _api_field;
    [SerializeField] private Button _exit;
    [SerializeField] private string _address = "localhost";

    private string SAVE_KEY = "LEVEL_ID";

    private void Awake()
    {
        _connector.OnError.AddListener(FadeIn);

        _start_new_game.onClick.AddListener(StartNewGame);
        _continue.onClick.AddListener(Continue);
        _connection.onClick.AddListener(Connect);
        _exit.onClick.AddListener(Exit);

        _api_field.text = _address;
        SetContinueButtonState();
    }

    private void SetContinueButtonState()
    {
        bool has_save = PlayerPrefs.HasKey(SAVE_KEY) && PlayerPrefs.GetInt(SAVE_KEY) != 0;
        if (!has_save)
        {
            _continue.interactable = false;

            var colors = _continue.colors;
            colors.normalColor = new Color(colors.normalColor.r, colors.normalColor.g, colors.normalColor.b, 0.5f);
            colors.disabledColor = new Color(colors.disabledColor.r, colors.disabledColor.g, colors.disabledColor.b, 0.3f);
            _continue.colors = colors;
        }
    }

    private void FadeIn()
    {
        IEnumerator Start()
        {
            yield return SceneUI.Instance.Fader.FadeIn();
        }

        StopAllCoroutines();
        StartCoroutine(Start());
    }

    private void StartNewGame()
    {
        IEnumerator Start()
        {
            yield return SceneUI.Instance.Fader.FadeOut();
            PlayerPrefs.SetInt(SAVE_KEY, 1);
            _connector.CreateHostConnection();
        }

        StopAllCoroutines();
        StartCoroutine(Start());
    }

    private void Continue()
    {
        IEnumerator Start()
        {
            yield return SceneUI.Instance.Fader.FadeOut();
            _connector.CreateHostConnection();
        }

        StopAllCoroutines();
        StartCoroutine(Start());
    }

    private void Connect()
    {
        IEnumerator Start()
        {
            yield return SceneUI.Instance.Fader.FadeOut();
            _connector.CreateClientConnection(_api_field.text);
        }

        StopAllCoroutines();
        StartCoroutine(Start());
    }

    private void Exit()
    {
        Application.Quit();
    }
}
