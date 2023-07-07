using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Poop : Structure
{

    public override void Awake()
    {
        base.Awake();
        hp = 8f;
    }

    public override void Update()
    {
        base.Update();
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
}
