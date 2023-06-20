using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UseCharge : Charge
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }



}
