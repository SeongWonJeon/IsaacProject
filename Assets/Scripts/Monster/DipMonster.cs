using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UIElements;

class DipMonster : Monster
{
    private Coroutine attack;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        moveSpeed = 3f;
        hp = 3;
    }
    private void OnEnable()
    {
        attack = StartCoroutine(MoevingTiming());
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    IEnumerator MoevingTiming()
    {
        while (true)
        {
            Vector3 vec3 = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);    // 랜덤으로 위치를 생성한다.
            yield return new WaitForSeconds(1f);
            rb.AddForce(vec3 * moveSpeed, ForceMode2D.Impulse);         // ForceMode2D.Impulse로 시작부터 한번에 훅 가도록
            if (vec3.x < 0)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                transform.rotation = Quaternion.Euler(0, 0, 0);

            anim.SetBool("IsMove", true);
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
            rb.velocity = Vector2.zero;
            anim.SetBool("IsMove", false);
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
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
