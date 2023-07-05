using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Trisagion : Item
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
            GameObject trisagionTears = GameManager.Resource.Load<GameObject>("Attack/TrisagionTears");
            isaac.tears = trisagionTears;
        }
    }
    public override void FireUse()
    {
        base.FireUse();
        if (isaac.attackInputDir.x > 0)           // �������� �ٶ󺻴�
        {
            
        }
        else if (isaac.attackInputDir.x < 0)      // ����
        {
            
        }

        if (isaac.attackInputDir.y > 0)           // ��
        {
            
        }
        else if (isaac.attackInputDir.y < 0)      // �Ʒ�
        {
            
        }
    }

    public override void MoveUse()
    {
        base.MoveUse();
        if (isaac.moveInputDir.x > 0)
        {
            if (isaac.turnHead == true)
            {
                
            }
        }
        else if (isaac.moveInputDir.x < 0)
        {
            if (isaac.turnHead == true)
            {
                
            }
        }
        else if (isaac.moveInputDir.x == 0)
        {
            if (isaac.turnHead == true)
            {
                
            }

        }

        if (isaac.moveInputDir.y > 0)
        {
            if (isaac.turnHead == true)
            {
                
            }
        }
    }

    
}
