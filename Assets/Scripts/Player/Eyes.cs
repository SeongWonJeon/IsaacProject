using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    [SerializeField] private GameObject tears;
    [SerializeField] private GameObject leftEye;
    [SerializeField] private GameObject rightEye;

    /*private bool left = true;
    private bool right = false;



    public void Fire()
    {
        StartCoroutine(TearsRoutine());
    }

    IEnumerator TearsRoutine()
    {
        yield return new WaitForSeconds(1f);
        left = false;
        right = true;
        yield return null;
    }*/
}
