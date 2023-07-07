using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Monster : MonoBehaviour, IDamagable
{
    protected float moveSpeed;
    protected float hp;
    protected float damage;

    Color colorG = new Color(1, 0.1f, 0.1f, 1);
    Color colorB = new Color(1, 1, 1, 1);

    public Animator anim;
    public Rigidbody2D rb;

    public SpriteRenderer render;

    protected virtual void Awake()
    {
        damage = GameManager.Data.MonsterDamage;
        
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
        if (collision.gameObject.tag == "Tears")
        {
            TakeDamage(GameManager.Data.AttackDamage);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        hp -= damage;

        StartCoroutine(Flash());

        if (hp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        anim.SetBool("IsDead", true);
        rb.velocity = Vector3.zero;
    }

    protected virtual void Dead()
    {
        gameObject.SetActive(false);
        anim.SetBool("IsDead", false);
        Destroy(gameObject);
    }
    IEnumerator Flash()
    {
        yield return null;
        render.color = colorG;
        yield return new WaitForSeconds(0.2f);
        render.color = colorB;
        yield return null;
    }
}
