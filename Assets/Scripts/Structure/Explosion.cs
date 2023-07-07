using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Animator anim;
    GameObject tnt;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        tnt = GameObject.Find("TNT");
    }
    public void ExplosionTNT()
    {
        anim.enabled = true;
    }
}
