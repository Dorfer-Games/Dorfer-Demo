using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TiktokSDKUI : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private TMP_InputField urlInputField;
    [SerializeField] private Button connectButton;

    public event Action<string> OnConnectButtonClick;

    private void Awake()
    {
        exitButton.onClick.AddListener(OnExitButtonClick);
        connectButton.onClick.AddListener(() => OnConnectButtonClick(urlInputField.text));
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
