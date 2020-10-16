using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.NetworkInformation;
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

        // ToDo webrequest calls
    }
    #endregion

    #region Private methods
    private bool GetReachabilityStatus()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }
    #endregion
}
