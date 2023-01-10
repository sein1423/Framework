using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormholeRotate : MonoBehaviour
{
    public float rotateSpeed = 1f;
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
    }
}
