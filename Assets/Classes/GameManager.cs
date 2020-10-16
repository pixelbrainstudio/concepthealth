using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region Monobehaviour calls
    void Start()
    {
        CommunicationManager.Instance.OnInternetAvabilityChangedEvent += InternetAvabilityChanged;
        CommunicationManager.Instance.Init();
    }
    void Update()
    {
    }
    void OnDestroy()
    {
        CommunicationManager.Instance.OnInternetAvabilityChangedEvent -= InternetAvabilityChanged;
    }
    #endregion

    #region private methods
    private void InternetAvabilityChanged(bool available)
    {
        UIManager.Instance.SetConnectionErrorVisibility(!available);
    }
    #endregion

}
