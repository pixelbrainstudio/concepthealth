using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
//using System.Net.NetworkInformation;
public class CommunicationManager : SingletonMonoBehaviour<CommunicationManager>
{
    #region Events and Delegates
    public delegate void OnInternetAvabilityChangedDelegate(bool available);
    public event OnInternetAvabilityChangedDelegate OnInternetAvabilityChangedEvent;
    #endregion

    #region private members
    bool isInternetReachable = false;
    #endregion

    #region Monobehaviour calls
    void Update()
    {
        bool currentNetworkState = GetReachabilityStatus();
        if (currentNetworkState != isInternetReachable)
        {
            isInternetReachable = currentNetworkState;
            OnInternetAvabilityChangedEvent?.Invoke(isInternetReachable);
        }
    }
    #endregion

    #region Public methods
    public void Init()
    {
        isInternetReachable = GetReachabilityStatus();
        OnInternetAvabilityChangedEvent?.Invoke(isInternetReachable);
    }
    public void TryToSendToAPI(string content)
    {
        if (!isInternetReachable)
        {
            return;
        }
        StartCoroutine(SendToAPI(content));
    }
    #endregion

    #region Private methods
    private bool GetReachabilityStatus()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }
    public IEnumerator SendToAPI(string content)
    {
        var request = new UnityWebRequest(Config.ApiURL, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(content);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            //Eating the errors
        }
    }
    #endregion
}
