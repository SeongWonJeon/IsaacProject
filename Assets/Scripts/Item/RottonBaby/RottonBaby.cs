using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

class RottonBaby : Item
{
    private float lastCreat = 1f;
    private float creatDelay = 1.5f;
    // TODO : 적의 위치로 파리를 보내는 방식 구현하기
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
        
        if (isaac.attackInputDir.x > 0)           // 오른쪽을 바라본다
        {
            // TODO : 범위안에 오브젝트의 tag가 == Monster인 옵젝이 있으면 공격
            if (monster != null)
            {
                StartCoroutine(GoFly("Right"));
            }
        }
        else if (isaac.attackInputDir.x < 0)      // 왼쪽
        {
            if (monster != null)
            {
                StartCoroutine(GoFly("Left"));
            }
        }

        if (isaac.attackInputDir.y > 0)           // 위
        {
            if (monster != null)
            {
                StartCoroutine(GoFly("Up"));
            }
        }
        else if (isaac.attackInputDir.y < 0)      // 아래
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
