using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Glasses : Item
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
            isaac.renderer[2].enabled = true;
        }
    }
    public override void FireUse()
    {
        base.FireUse();
        if (isaac.attackInputDir.x > 0)           // �������� �ٶ󺻴�
        {
            GetTearsPlus(new Vector3(0, 0, 0), isaac.tears);
        }
        else if (isaac.attackInputDir.x < 0)      // ����
        {
            GetTearsPlus(new Vector3(0, 180, 0), isaac.tears);
        }

        if (isaac.attackInputDir.y > 0)           // ��
        {
            GetTearsPlus(new Vector3(0, 0, 90f), isaac.tears);
        }
        else if (isaac.attackInputDir.y < 0)      // �Ʒ�
        {
            GetTearsPlus(new Vector3(0, 0, -90f), isaac.tears);
        }
    }

    public override void MoveUse()
    {
        base.MoveUse();
    }
}
