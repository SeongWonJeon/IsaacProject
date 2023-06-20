using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightEyeAttack : MonoBehaviour
{
    [SerializeField] private GameObject tears;

    private Transform pos;

    private void Awake()
    {
        pos = GetComponent<Transform>();

        //GameManager.Resource.Instantiate<Tears>("Attack/Tears", transform.position, Quaternion.identity, true);
    }


}
