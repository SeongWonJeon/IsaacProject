using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftEyeAttack : MonoBehaviour
{
    [SerializeField] private GameObject tears;

    private Transform pos;

    private void Awake()
    {
        pos = GetComponent<Transform>();
    }
}
