using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMove : MonoBehaviour
{
    private Vector3 cameraPos;
    public Transform targetTransform;
    private void Awake()
    {
        cameraPos = new Vector3(0, 0, -10);
    }
    private void Update() // 타겟 트랜스폼에 따라 움직임
    {
        transform.position = new Vector3(targetTransform.position.x, targetTransform.position.y, targetTransform.position.z - 10);
    }
}
