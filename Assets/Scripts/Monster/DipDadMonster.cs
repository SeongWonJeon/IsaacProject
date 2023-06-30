using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class DipDadMonster : MonoBehaviour, IDamagable
{
    [SerializeField] GameObject leftSummonPoint;
    [SerializeField] GameObject rightSummonPoint;
    GameObject child;
    public float moveSpeed;
    private float hp = 6;

    private Rigidbody2D rb;
    private Transform endPoint;
    private Animator anim;

    private Coroutine attack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
    private void OnEnable()
    {
        attack = StartCoroutine(Moving());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(GameManager.Data.MonsterDamage);
        }
        if (collision.gameObject.tag == "Tears")
        {
            TakeDamage(GameManager.Data.AttackDamage);
        }
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

    public void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetBool("IsDead", true);
        rb.velocity = Vector3.zero;
        GameManager.Resource.Instantiate<GameObject>("Monster/Dip_Monster", leftSummonPoint.transform.position, Quaternion.identity);
        GameManager.Resource.Instantiate<GameObject>("Monster/Dip_Monster", rightSummonPoint.transform.position, Quaternion.identity);
        StopCoroutine(attack);
        
    }

    public void Dead()
    {
        gameObject.SetActive(false);
        anim.SetBool("IsDead", false);
    }
}
