using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private List<GameObject> monsters;
    GameObject firstMonster;
    GameObject secondMonster;

    Rigidbody2D rb;
    public float speed;
    private float shortDis;
    private float ar;

    Vector3 monster;
    Vector3 startPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        monsters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Monster"));
        shortDis = Vector3.Distance(gameObject.transform.position, monsters[0].transform.position); // 첫번째 몬스터를 기준

        firstMonster = monsters[0];       // 첫번째 몬스터를 저장

        foreach (GameObject found in monsters)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);     // 게임오브젝트와 찾은 첫번째 오브젝트의 거리
            
            if (Distance < shortDis)    // 비교해서 더 작으면 
            {
                firstMonster = found;   // 첫번째 타겟을 이몬스터로
                
                shortDis = Distance;    // 거리를 Distance로
            }
        }
        
    }
    private void Update()
    {
        Watch();
    }
    private void FixedUpdate()
    {
        Attack();

    }

    private void Watch()
    {
        if (firstMonster != null)
            ar = Vector3.SqrMagnitude(firstMonster.transform.position - transform.position);        // SqrMagnitude 두 오브젝트 사이의 거리를 결과로 내어준다.
        else
            Destroy(gameObject);

        if (ar > 0.1f)
        {
            monster = firstMonster.transform.position;
        }
        

    }

    private void Attack()
    {
        transform.position = Vector3.MoveTowards(transform.position, monster, speed);   // Lerp 부드럽게 이동하도록 도와준다.
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            collision.gameObject.GetComponent<IDamagable>().TakeDamage(1f);
            Destroy(gameObject);
        }
    }
}
