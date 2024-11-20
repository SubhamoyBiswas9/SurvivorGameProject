using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWorld : MonoBehaviour
{
    [SerializeField] Transform lookAt;
    [SerializeField] Vector3 offset;

    Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    public void SetLookAt(Transform transform)
    {
        if (lookAt == null || lookAt != transform)
            lookAt = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAt == null) return;

        Vector3 pos = mainCam.WorldToScreenPoint(lookAt.position + offset);

        if (transform.position != pos)
            transform.position = pos;
    }
}
