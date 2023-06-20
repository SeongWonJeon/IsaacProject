using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Dice : Charge
{
    private Outline outline;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Gauge = 6;
    }

    private void OnActiveItem(InputValue value)
    {
        if (Gauge == 6)
        {
            UseActive(0);
            Gauge = 0;
        }
    }
    protected override void ClearChargeGauge(int number)
    {
        base.ClearChargeGauge(1);
        
    }
}
