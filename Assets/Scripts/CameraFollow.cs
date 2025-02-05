using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    public GameObject objectToFollow;
    public float xOffset;
    public float yOffset;
    public float zOffset;
    public float floatStrength;

    protected override void Awake()
    {
        base.Awake();
        transform.position = new Vector3(objectToFollow.transform.position.x - 0.75f, objectToFollow.transform.position.y + 0.8f, objectToFollow.transform.position.z - 0.75f);
    }

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(objectToFollow.transform.position.x + xOffset, objectToFollow.transform.position.y + yOffset, objectToFollow.transform.position.z + zOffset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, floatStrength);
    }
}
