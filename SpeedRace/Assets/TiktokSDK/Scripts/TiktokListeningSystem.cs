using Kuhpik;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class TiktokListeningSystem : GameSystem
{
    private const string hostname = "leaderteamsolution.com";
    //private const string hostname = "leaderteamsolutiossn.com";
    //private const string hostname = "tiktok.inxo.ru";
    private const int port = 9003;
    private const float serverConnectionTimeout = 5f;
    private const float streamConnectionTimeout = 20f;

    private ClientTcp tcp;

    private Task serverTask;
    private Task getDataTask;

    private SDKUIScreen sdkUI;

    public override void OnInit()
    {
        sdkUI = Bootstrap.Instance.GetScreen<SDKUIScreen>();
        sdkUI.OnConnectButtonClick += ConnectToStream;
        ConnectToServer();
        CheckServerConnection();
    }

    private void CheckServerConnection()
    {
        Debug.Log("<color=yellow>TiktokSDK:</color><color=yellow>Trying connect to server...</color>");
        sdkUI.AddLog("Trying connect to server...");
        StartCoroutine(ConnectServerCheckRoutine(0, connect =>
        {
            if (connect)
            {
                Debug.Log("<color=yellow>TiktokSDK:</color><color=green>Tiktok server connection successful! </color>");
                sdkUI.AddLog("Tiktok server connection successful!");
                sdkUI.SetConnectButtonState(true);
            }
            else
            {
                Debug.Log("<color=yellow>TiktokSDK:</color><color=red>Tiktok server connection failed! </color>");
                sdkUI.AddLog("Tiktok server connection failed!");
                sdkUI.SetConnectButtonState(false);
            }
        }));
    }

    private void CheckStreamConnection()
    {
        Debug.Log("<color=yellow>TiktokSDK:</color><color=yellow>Trying connect to stream...</color>");
        sdkUI.AddLog("Trying connect to stream...");
        StartCoroutine(ConnectStreamCheckRoutine(0, connect =>
        {
            if (connect)
            {
                Debug.Log("<color=yellow>TiktokSDK:</color><color=green>Tiktok stream connection successful!</color>");
                sdkUI.AddLog("Tiktok stream connection successful!");
                Bootstrap.Instance.ChangeGameState(GameStateID.Loading);
            }
            else
            {
                Debug.Log("<color=yellow>TiktokSDK:</color><color=red>Tiktok stream connection failed!</color>");
                sdkUI.AddLog("Tiktok stream connection failed!");
                sdkUI.SetConnectButtonState(true);
            }
        }));
    }

    private IEnumerator ConnectServerCheckRoutine(float time, Action<bool> OnConnect)
    {
        yield return new WaitForSeconds(0.1f);
        time += 0.1f;
        if (tcp.ServerConnection)
        {
            OnConnect.Invoke(true);
        }
        else
        {
            if (time >= serverConnectionTimeout)
            {
                OnConnect.Invoke(false);
            }
            else
            {
                StartCoroutine(ConnectServerCheckRoutine(time, OnConnect));
            }
        }
    }

    private IEnumerator ConnectStreamCheckRoutine(float time, Action<bool> OnConnect)
    {
        yield return new WaitForSeconds(0.1f);
        time += 0.1f;
        if (tcp.StreamConnection)
        {
            OnConnect.Invoke(true);
        }
        else
        {
            if (time >= streamConnectionTimeout)
            {
                OnConnect.Invoke(false);
            }
            else
            {
                StartCoroutine(ConnectStreamCheckRoutine(time, OnConnect));
            }
        }
    }

    private void ConnectToServer()
    {
        tcp = new ClientTcp(hostname, port);
        tcp.OnServerAnswered += WorkingServerResponse;

        serverTask = new Task((async () => await tcp.Observ()));
        serverTask.Start();
    }

    private void ConnectToStream(string uniqId)
    {
        if (tcp.ServerConnection)
        {
            CheckStreamConnection();
            sdkUI.SetConnectButtonState(false);
            var streamData = new StreamData("switchStream", "tiktok", uniqId);
            string streamDataJSON = JsonUtility.ToJson(streamData);
            getDataTask = new Task(async () => await tcp.SendData(streamDataJSON));
            getDataTask.Start();
        }
    }

    private void WorkingServerResponse(string answer)
    {
        object serverRespondedObject = JsonParser.ParseJSON(answer);
        if (serverRespondedObject is ConnectionData)
        {
            var connectionData = (ConnectionData)serverRespondedObject;
            if (connectionData.data.state == "connected")
            {
                tcp.StreamConnection = true;
            }
            else
            {
                tcp.StreamConnection = false;
            }
        }
        if (tcp.StreamConnection)
        {
            if (serverRespondedObject is LikeData)
            {
                game.ttEvents.AddLikeEvent((LikeData)serverRespondedObject);
            }
            else if (serverRespondedObject is GiftData)
            {
                game.ttEvents.AddGiftEvent((GiftData)serverRespondedObject);
            }
            else if (serverRespondedObject is CommentData)
            {
                game.ttEvents.AddCommentEvent((CommentData)serverRespondedObject);
            }
        }
    }

    private void OnDestroy()
    {
        tcp.OnServerAnswered -= WorkingServerResponse;
        tcp?.Dispose();
        sdkUI.OnConnectButtonClick -= ConnectToStream;
    }
}
