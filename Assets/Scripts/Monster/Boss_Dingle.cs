using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

class Boss_Dingle : Monster
{
    protected override void Awake()
    {
        base.Awake();
        render = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        moveSpeed = 20f;
        hp = 150;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        

    }

    protected override void Dead()
    {
        
    }
}
