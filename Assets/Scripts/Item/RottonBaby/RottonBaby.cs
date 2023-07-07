using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

class RottonBaby : Item
{
    private float lastCreat = 1f;
    private float creatDelay = 1.5f;
    // TODO : ���� ��ġ�� �ĸ��� ������ ��� �����ϱ�
    public GameObject flyPatPrefab;
    public GameObject flyPrefab;
    public GameObject summonPoint;
    private GameObject monster;

    public Animator anim;

    protected override void Awake()
    {
        base.Awake();
        flyPatPrefab = GameManager.Resource.Load<GameObject>("Prefab/RottonBabyPat");
        summonPoint = GameObject.Find("PatSummonPoint");
        monster = GameObject.FindGameObjectWithTag("Monster");
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Body")
        {
            GameManager.Resource.Instantiate(flyPatPrefab, new Vector3(isaac.transform.position.x -0.5f, isaac.transform.position.y +0.5f, 0), Quaternion.identity);
            anim = GameObject.Find("RottonBabyPat(Clone)").GetComponent<Animator>();
        }
    }
    public override void FireUse()
    {
        base.FireUse();
        
        if (isaac.attackInputDir.x > 0)           // �������� �ٶ󺻴�
        {
            // TODO : �����ȿ� ������Ʈ�� tag�� == Monster�� ������ ������ ����
            if (monster != null)
            {
                StartCoroutine(GoFly("Right"));
            }
        }
        else if (isaac.attackInputDir.x < 0)      // ����
        {
            if (monster != null)
            {
                StartCoroutine(GoFly("Left"));
            }
        }

        if (isaac.attackInputDir.y > 0)           // ��
        {
            if (monster != null)
            {
                StartCoroutine(GoFly("Up"));
            }
        }
        else if (isaac.attackInputDir.y < 0)      // �Ʒ�
        {
            if (monster != null)
            {
                StartCoroutine(GoFly("down"));
            }
        }
    }
    public override void MoveUse()
    {
        base.MoveUse();
    }

    
    IEnumerator GoFly(string name)
    {
        yield return null;
        if (Time.time > lastCreat + creatDelay)
        {
            GameObject flyPrefab = GameManager.Resource.Instantiate<GameObject>("Prefab/Fly");
            flyPrefab.transform.position = GameObject.Find("RottonBabyPat(Clone)").transform.position;
            flyPrefab.transform.rotation = Quaternion.identity;
            anim.SetTrigger(name);
            lastCreat = Time.time;
        }
    }
}
