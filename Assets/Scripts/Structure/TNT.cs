using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TNT : Structure
{
    Animator anima;
    public override void Awake()
    {
        base.Awake();
        hp = 7f;

        anima = GameObject.Find("Explosion").GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();
    }
    public void LateUpdate()
    {
        if (hp <= 0)
        {
            anima.enabled = true;
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (hp <= 0)
        {
            
            Explosion();
        }
    }

    public void Explosion()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, 2.5f);
        StartCoroutine(Delay());
        foreach (Collider2D collider in colliders)
        {
            if (collider == GetComponent<Collider2D>())
                continue;

            if (collider.gameObject.tag == "Monster")
            {
                collider.gameObject.GetComponent<IDamagable>().TakeDamage(10);
            }
            else if (collider.gameObject.tag == "Player")
            {
                collider.gameObject.GetComponent<IDamagable>().TakeDamage(1);
            }
            else if (collider.gameObject.tag == "Structure")
            {
                collider.gameObject.GetComponent<IDamagable>()?.TakeDamage(10);
            }
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        yield return null;
    }
}
