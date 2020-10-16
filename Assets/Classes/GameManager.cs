using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region Unity references
    [SerializeField]
    private GameObject ball;
    private Rigidbody ballRigidbody;
    #endregion


    #region Monobehaviour calls
    void Start()
    {
        CommunicationManager.Instance.OnInternetAvabilityChangedEvent += InternetAvabilityChanged;
        CommunicationManager.Instance.Init();
        if (!ReferenceEquals(ball, null))
        {
            ballRigidbody = ball.GetComponent<Rigidbody>();
        }
        StartCoroutine(CoordinatePooling());
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckBallHit() && !ReferenceEquals(ballRigidbody, null))
            {
                ballRigidbody.velocity = new Vector3(Random.Range(-1f, 1f), Config.HitPower, 0);
            }

        }
    }
    void OnDestroy()
    {
        StopAllCoroutines();
        CommunicationManager.Instance.OnInternetAvabilityChangedEvent -= InternetAvabilityChanged;
    }
    #endregion

    #region private methods
    public void BallCollision(Vector3 position)
    {
        SoundManager.Instance.PlaySfx();
        UIManager.Instance.ShowCoordinates("X:" + position.x + " Y:" + position.y + " Z: " + position.z);
    }
    #endregion

    #region private methods
    private void InternetAvabilityChanged(bool available)
    {
        UIManager.Instance.SetConnectionErrorVisibility(!available);
    }

    private bool CheckBallHit()
    {
        bool isHit = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray.origin, ray.direction, out raycastHit, Config.TouchMaxDistance) && raycastHit.transform.CompareTag("Ball"))
        {
            isHit = true;
        }
        return isHit;
    }
    IEnumerator CoordinatePooling()
    {
        while (true)
        {
            if (!ReferenceEquals(ball, null))
            {
                string coordinateText = JsonUtility.ToJson(ball.transform.position);
                CommunicationManager.Instance.TryToSendToAPI(coordinateText);
                yield return new WaitForSeconds(Config.ApiPoolingTime);
            }
        }
    }
    #endregion

}
