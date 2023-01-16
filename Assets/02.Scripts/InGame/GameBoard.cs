using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameBoard : MonoBehaviour
{
    private bool rotating;
    public float rotateSpeed = 1;
    private float angle;
    public float maxDegree = 30;
    private Vector3 mousePos;
    private Vector3 offset;
    private Vector3 rotation;

    private void Awake()
    {
        angle = transform.eulerAngles.z;
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
}
