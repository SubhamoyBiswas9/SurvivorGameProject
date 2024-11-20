using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (playerTransform == null) return;

        mainCam.transform.position = new Vector3(playerTransform.position.x,mainCam.transform.position.y,playerTransform.position.z);
    }
}
