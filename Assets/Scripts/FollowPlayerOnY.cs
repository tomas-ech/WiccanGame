using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerOnY : MonoBehaviour
{
    public float minimapCameraOffset = 10f;
    private Transform playerReference;

    void Update()
    {
        playerReference = GameObject.FindWithTag("Player").transform;

        if (playerReference != null)
        {
            transform.position = new Vector3(playerReference.position.x, playerReference.position.y + minimapCameraOffset, playerReference.position.z);

            transform.rotation = Quaternion.Euler(90f, playerReference.eulerAngles.y, 0f);
        }
    }
}
