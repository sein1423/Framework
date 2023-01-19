using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMove : MonoBehaviour
{
    private Vector3 cameraPos;
    private float xPos, yPos;
    public Transform targetTransform;
    public Transform feverTransform;
    public Transform goalTransform;

    public void SetFeverFocus()
    {
        transform.position = feverTransform.position + new Vector3(0, 0, -10);
    }

    public void SetPlayerFocus()
    {
        xPos = targetTransform.position.x;
        yPos = targetTransform.position.y - 2;
        if (yPos < -22.5f)
            yPos = -22.5f;
        transform.position = new Vector3(xPos, yPos, -10);
    }
}
