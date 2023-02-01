using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameBoard : MonoBehaviour
{
    private bool rotating;
    private float rotateSpeed = 0.5f;
    private float angle;
    private float maxDegree = 30f;
    private Vector3 mousePos;
    private Vector3 offset;
    private Vector3 rotation;

    private float dragValue;
    private float gyroAngle;
    private float gyroRotateSpeed = 10;
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

        if (GameManager.instance.b_gameStart && !GameManager.instance.b_startFever)
        {
            RotateWithGyro();
        }

        if(GameManager.instance.b_revive)
        {
            angle = 0;
            GameManager.instance.b_revive = false;
        }
    }

    /// <summary>
    /// 드래그로 회전하기
    /// </summary>
    public void RotateWithDrag()
    {
        offset = (Input.mousePosition - mousePos);
        dragValue = (offset.x) * Time.deltaTime * rotateSpeed;
        dragValue = Mathf.Clamp(dragValue, -1f, 1f);
        angle -= dragValue;
        angle = Mathf.Clamp(angle, -maxDegree, maxDegree);
        transform.eulerAngles = new Vector3(0, 0, angle);
        mousePos = Input.mousePosition;
    }

    public void RotateWithGyro()
    {
        gyroValue = Input.gyro.rotationRateUnbiased.y * Time.deltaTime * gyroRotateSpeed;
        //gyroValue = Input.acceleration.z * Time.deltaTime * gyroRotateSpeed;
        gyroValue = Mathf.Clamp(gyroValue, -1f, 1f);
        angle -= gyroValue;
        angle = Mathf.Clamp(angle, -maxDegree, maxDegree);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}