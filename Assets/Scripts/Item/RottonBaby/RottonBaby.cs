using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RottonBaby : Item
{
    private float lastCreat = 1f;
    private float creatDelay = 1.5f;
    // TODO : 적의 위치로 파리를 보내는 방식 구현하기
    public GameObject flyPatPrefab;
    public GameObject flyPrefab;

    protected override void Awake()
    {
        base.Awake();
        flyPatPrefab = GameManager.Resource.Load<GameObject>("Prefab/RottonBabyPat");
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Body")
        {
            GameManager.Resource.Instantiate(flyPatPrefab, isaac.transform.position - (transform.right * -1f), Quaternion.identity, isaac.transform);
        }
    }
    public override void FireUse()
    {
        base.FireUse();
        if (isaac.attackInputDir.x > 0)           // 오른쪽을 바라본다
        {
            // TODO : 범위안에 오브젝트의 tag가 == Monster인 옵젝이 있으면 공격
            StartCoroutine(GoFly());
        }
        else if (isaac.attackInputDir.x < 0)      // 왼쪽
        {
            StartCoroutine(GoFly());
        }

        if (isaac.attackInputDir.y > 0)           // 위
        {
            StartCoroutine(GoFly());
        }
        else if (isaac.attackInputDir.y < 0)      // 아래
        {
            StartCoroutine(GoFly());
        }
    }
    public override void MoveUse()
    {
        base.MoveUse();
    }

    
    IEnumerator GoFly()
    {
        yield return null;
        if (Time.time > lastCreat + creatDelay)
        {
            GameObject flyPrefab = GameManager.Resource.Instantiate<GameObject>("Prefab/Fly");
            flyPrefab.transform.position = isaac.transform.position - (transform.right * -1f);
            flyPrefab.transform.rotation = Quaternion.identity;
            lastCreat = Time.time;
        }
        yield return new WaitForSeconds(2.5f);
    }
}
