using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void OnValidate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + cameraOffset;
            transform.LookAt(playerTransform.position);
        }

    }
}
