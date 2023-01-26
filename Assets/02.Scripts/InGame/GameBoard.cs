using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameBoard : MonoBehaviour
{
    private bool rotating;
    private float rotateSpeed = 0.5f;
    private float angle;
    private float maxDegree = 25;
    private Vector3 mousePos;
    private Vector3 offset;
    private Vector3 rotation;

    private float gyroAngle;
    private float gyroRotateSpeed = 13f;
    private float gyroValue;

    private void Start()
    {
        angle = transform.eulerAngles.z;
        Input.gyro.enabled = true;
    }

    private void OnMouseDown()
    {
        rotating = true;
        mousePos = Input.mousePosition;

    }

    private void OnMouseUp()
    {
        rotating = false;
    }

    private void Update()
    {
        if (rotating && GameManager.instance.b_gameStart)
        {
            RotateWithDrag();
        }

        if (GameManager.instance.b_gameStart)
        {
            RotateWithGyro();
        }
    }

    /// <summary>
    /// 드래그로 회전하기
    /// </summary>
    public void RotateWithDrag()
    {
        offset = (Input.mousePosition - mousePos);
        angle -= (offset.x) * Time.deltaTime * rotateSpeed;
        angle = Mathf.Clamp(angle, -maxDegree, maxDegree);
        transform.eulerAngles = new Vector3(0, 0, angle);
        mousePos = Input.mousePosition;
    }

    public void RotateWithGyro()
    {
        gyroValue = Input.gyro.rotationRateUnbiased.y * Time.deltaTime * gyroRotateSpeed;
        gyroValue = Mathf.Clamp(gyroValue, -1f, 1f);
        angle -= gyroValue;
        angle = Mathf.Clamp(angle, -maxDegree, maxDegree);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}