using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearsPop : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        anim.SetTrigger("IsTouch");
    }
}
