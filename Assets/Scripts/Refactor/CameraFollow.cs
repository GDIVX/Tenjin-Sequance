using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    [SerializeField] float smoothing;

    [SerializeField, Range(20, 50)] float camRotation = 30;

    void Update()
    {
        Vector3 playerPos = new Vector3(playerTransform.position.x, 0, playerTransform.position.z);
        transform.position = Vector3.Lerp(transform.position, playerPos, Time.deltaTime * smoothing);
    }

    private void OnValidate()
    {
        transform.rotation = Quaternion.Euler(camRotation, 45, 0);
    }
}
