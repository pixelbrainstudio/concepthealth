using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviourScript : MonoBehaviour
{
    #region Monobehaviour calls
    void OnCollisionEnter(Collision collision)
    {
        bool isPlaneHit = false;
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.CompareTag(Config.TagPlane))
            {
                isPlaneHit = true;
                break;
            }
        }

        if (collision.relativeVelocity.magnitude > 0.2f && isPlaneHit)
        {
            GameManager.Instance.BallCollision(this.transform.position);
        }
    }
    #endregion

}
