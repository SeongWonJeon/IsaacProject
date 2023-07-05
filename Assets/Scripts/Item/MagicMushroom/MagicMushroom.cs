using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MagicMushroom : Item
{
    protected override void Awake()
    {
        base.Awake();

    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Body")
        {
            collision.gameObject.transform.localScale
            = new Vector3(collision.gameObject.transform.localScale.x + 0.5f, collision.gameObject.transform.localScale.y + 0.5f, 0);
            GameManager.Data.AttackDamage += 1.5f;
            GameManager.Data.MoveSpeed += 1f;
            GameManager.Data.AttackRange += 1f;
            GameManager.Data.AttackSpeed += 0.3f;
            isaac.tearsScale += 0.5f;
        }
    } 
    public override void FireUse()
    {
        base.FireUse();
        
    }

    public override void MoveUse()
    {
        base.MoveUse();
        
    }


}
