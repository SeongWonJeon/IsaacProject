using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

class DipDadMonster : Monster
{
    [SerializeField] GameObject leftSummonPoint;
    [SerializeField] GameObject rightSummonPoint;
    GameObject child;

    private Transform endPoint;

    private Coroutine attack;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        moveSpeed = 15f;
        hp = 6;
    }

    private void OnEnable()
    {
        attack = StartCoroutine(Moving());
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
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
            
            if (endPoint.position.x - transform.position.x < 0)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                transform.rotation = Quaternion.Euler(0, 0, 0);

            anim.SetBool("IsMove", true);
            yield return new WaitForSeconds(2f);
            anim.SetBool("IsMove", false);
            rb.velocity = Vector3.zero;
            yield return new WaitForSeconds(1f);


        }
        
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        base.Die();
        GameManager.Resource.Instantiate<GameObject>("Monster/Dip_Monster", leftSummonPoint.transform.position, Quaternion.identity);
        GameManager.Resource.Instantiate<GameObject>("Monster/Dip_Monster", rightSummonPoint.transform.position, Quaternion.identity);
        StopCoroutine(attack);
    }

    protected override void Dead()
    {
        base.Dead();
    }
}
