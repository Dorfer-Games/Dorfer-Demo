using Kuhpik;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SDKUIScreen : UIScreen
{
    [SerializeField] private Button exitButton;
    [SerializeField] private TMP_InputField urlInputField;
    [SerializeField] private TextMeshProUGUI logs;
    [SerializeField] private Button connectButton;

    public event Action<string> OnConnectButtonClick;

    public override void Subscribe()
    {
        exitButton.onClick.AddListener(OnExitButtonClick);
        connectButton.onClick.AddListener(() => OnConnectButtonClick(urlInputField.text));
        connectButton.gameObject.SetActive(false);
    }

    public void SetConnectButtonState(bool state)
    {
        connectButton.gameObject.SetActive(state);
    }

    public void AddLog(string log)
    {
        logs.text += "\n" + log;
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        exitButton.onClick.RemoveAllListeners();
        connectButton.onClick.RemoveAllListeners();
    }
}
