using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameBoard : MonoBehaviour
{
    private bool rotating;
    private float rotateSpeed = 0.7f;
    private float angle;
    private float maxDegree = 30f;
    private Vector3 mousePos;
    private Vector3 offset;
    private Vector3 rotation;

    private float dragValue;
    private float gyroAngle;
    private float gyroRotateSpeed = 10;
    private float gyroValue;
    private float accelValue;
    GameManager gameManager;


    private void Awake()
    {
        gameManager = GameManager.instance;
    }

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
        if (rotating && gameManager.b_GameStart)
        {
            RotateWithDrag();
        }

        if (gameManager.b_GameStart && !gameManager.b_StartFever)
        {
            RotateWithGyro();
        }

        if(gameManager.b_revive)
        {
            angle = 0;
            gameManager.b_revive = false;
        }
    }

    /// <summary>
    /// 드래그로 회전하기
    /// </summary>
    public void RotateWithDrag()
    {
        offset = (Input.mousePosition - mousePos);
        dragValue = (offset.x) * Time.deltaTime * rotateSpeed;
        dragValue = Mathf.Clamp(dragValue, -0.7f, 0.7f);
        angle -= dragValue;
        angle = Mathf.Clamp(angle, -maxDegree, maxDegree);
        transform.eulerAngles = new Vector3(0, 0, angle);
        mousePos = Input.mousePosition;
    }

    public void RotateWithGyro()
    {
        gyroValue = Input.gyro.rotationRateUnbiased.y * Time.deltaTime * gyroRotateSpeed;
        gyroValue = Mathf.Clamp(gyroValue, -0.7f, 0.7f);
        accelValue = Input.acceleration.x * Time.deltaTime * gyroRotateSpeed;
        angle -= (gyroValue + accelValue);
        angle = Mathf.Clamp(angle, -maxDegree, maxDegree);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}