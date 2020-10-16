using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    #region Unity references
    [SerializeField]
    private GameObject textCoordinates;

    private TMP_Text tmpTextCoordinates;


    [SerializeField]
    private GameObject textConnectionError;
    private TMP_Text tmpTextConnectionError;
    #endregion

    #region Public methods
    public void SetConnectionErrorVisibility(bool visibility)
    {
        textConnectionError?.SetActive(visibility);
    }

    public void ShowCoordinates(string text)
    {
        if (!ReferenceEquals(tmpTextCoordinates, null))
        {
            StartCoroutine(DelayedShowCoordinates(text));
        }
    }
    #endregion

    #region Monobehaviour calls
    void Awake()
    {
        tmpTextCoordinates = textCoordinates?.GetComponent<TMP_Text>();
        tmpTextConnectionError = textConnectionError?.GetComponent<TMP_Text>();
        textCoordinates?.SetActive(false);
        SetConnectionErrorVisibility(false);
    }
    #endregion

    #region Private Methods
    IEnumerator DelayedShowCoordinates(string text)
    {
        tmpTextCoordinates.text = text;
        textCoordinates.SetActive(true);
        yield return new WaitForSeconds(Config.Message_delay);
        textCoordinates.SetActive(false);
    }
    #endregion
}
