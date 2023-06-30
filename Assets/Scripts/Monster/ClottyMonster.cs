using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClottyMonster : MonoBehaviour, IDamagable
{
    public float moveSpeed;
    private float hp = 8f;

    GameObject monsterTears;
    private Rigidbody2D rb;

    private Animator anim;

    private Coroutine attack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        monsterTears = GameManager.Resource.Load<GameObject>("Monster/MonsterTears");
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
        yield return new WaitForSeconds(1f);
        while (true)
        {
            Vector3 vect3 = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), 0);
            rb.AddForce(vect3 * moveSpeed, ForceMode2D.Impulse);
            anim.SetBool("IsAttack", true);
            if (vect3.x < 0)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                transform.rotation = Quaternion.Euler(0, 0, 0);
            yield return new WaitForSeconds(1.2f);
            anim.SetBool("IsAttack", false);

            yield return null;
            for (int i = 0; i < 360; i += 90)
            {
                GameObject attackboll = GameManager.Pool.Get(monsterTears);
                attackboll.transform.position = transform.position;
                attackboll.transform.rotation = Quaternion.Euler(0, 0, i);
            }
            

            yield return null;
        }
    }
    public void StopMove()
    {
        rb.velocity = Vector3.zero;
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
        StopCoroutine(attack);

    }

    public void Dead()
    {
        gameObject.SetActive(false);
        anim.SetBool("IsDead", false);
    }
}
