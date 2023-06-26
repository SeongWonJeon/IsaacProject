using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    [SerializeField] private GameObject tears;
    [SerializeField] private GameObject leftEye;
    [SerializeField] private GameObject rightEye;

    private bool left = true;
    private bool right = false;

    public void Fire()
    {
        StartCoroutine(TearsRoutine());
    }

    IEnumerator TearsRoutine()
    {
        GameManager.Pool.Get(tears);

        if (left == true && right == false)
        {
            tears.transform.position = leftEye.transform.position;
            tears.transform.rotation = Quaternion.identity;
        }
        else
        {
            tears.transform.position = rightEye.transform.position;
            tears.transform.rotation = Quaternion.identity;
        }



        yield return null;
    }
}
