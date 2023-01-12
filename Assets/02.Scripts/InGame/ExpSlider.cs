using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpSlider : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    private void Update()
    {
        slider.value = (float)GameManager.instance.expTotal / GameManager.instance.expMax;
    }
}
