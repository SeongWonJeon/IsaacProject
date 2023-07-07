using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TrisagionTears : BaseTears
{
    protected override void Awake()
    {
        base.Awake();

    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    public override void SetStartPos(Vector3 pos)
    {
        base.SetStartPos(pos);
    }
    protected override void Disappear()
    {
        base.Disappear();


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster" )//|| collision.gameObject.tag == "Structure")
        {
            collision.gameObject.GetComponent<IDamagable>().TakeDamage(GameManager.Data.AttackDamage);
        }
    }
}
