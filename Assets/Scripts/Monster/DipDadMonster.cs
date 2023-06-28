using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class DipDadMonster : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;
    public Transform endPoint;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
    private void OnEnable()
    {
        StartCoroutine(Moving());
    }


    IEnumerator Moving()
    {
        endPoint = GameObject.FindGameObjectWithTag("Player").transform;
        while (true)
        {

            yield return new WaitForSeconds(1.5f);
            anim.SetBool("IsCharging", true);
            yield return new WaitForSeconds(1f);
            anim.SetBool("IsCharging", false);

            rb.AddForce((new Vector3(endPoint.position.x, endPoint.position.y, 0) - transform.position).normalized * moveSpeed, ForceMode2D.Impulse);

            anim.SetBool("IsMove", true);
            yield return new WaitForSeconds(2f);
            anim.SetBool("IsMove", false);
            rb.velocity = Vector3.zero;
            yield return new WaitForSeconds(1f);


        }
        
    }
}
