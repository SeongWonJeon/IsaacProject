using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Charge : MonoBehaviour
{
    private int gauge;
    private RectTransform rect;
    private ClearRoom clear;

    public int Gauge { get { return gauge; } set { gauge = value; } }
    public RectTransform Rect { get { return rect; } }

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    protected virtual void ClearChargeGauge(int number) { }

    protected virtual void UseActive(float gauge)
    {
        
    }
}
