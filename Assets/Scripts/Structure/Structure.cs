using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Structure : MonoBehaviour, IDamagable
{
    protected float hp;
    Animator anim;
    public virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        anim.SetFloat("CurHp", hp);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tears")
        {
            TakeDamage(GameManager.Data.AttackDamage);
        }
        else if (collision.gameObject.tag == "Bomb")
        {
            TakeDamage(GameManager.Data.BombDamage);
        }
    }
    public virtual void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
