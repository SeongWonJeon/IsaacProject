using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ClottyMonster : Monster
{
    GameObject monsterTears;

    private Coroutine attack;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        monsterTears = GameManager.Resource.Load<GameObject>("Monster/MonsterTears");
        render = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        moveSpeed = 2.5f;
        hp = 8;
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
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        base.Die();
        StopCoroutine(attack);
    }

    protected override void Dead()
    {
        base.Dead();
    }
}
