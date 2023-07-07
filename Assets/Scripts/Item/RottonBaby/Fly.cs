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
        shortDis = Vector3.Distance(gameObject.transform.position, monsters[0].transform.position); // ù��° ���͸� ����

        firstMonster = monsters[0];       // ù��° ���͸� ����

        foreach (GameObject found in monsters)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);     // ���ӿ�����Ʈ�� ã�� ù��° ������Ʈ�� �Ÿ�
            
            if (Distance < shortDis)    // ���ؼ� �� ������ 
            {
                firstMonster = found;   // ù��° Ÿ���� �̸��ͷ�
                
                shortDis = Distance;    // �Ÿ��� Distance��
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
            ar = Vector3.SqrMagnitude(firstMonster.transform.position - transform.position);        // SqrMagnitude �� ������Ʈ ������ �Ÿ��� ����� �����ش�.
        else
            Destroy(gameObject);

        if (ar > 0.1f)
        {
            monster = firstMonster.transform.position;
        }
        

    }

    private void Attack()
    {
        transform.position = Vector3.MoveTowards(transform.position, monster, speed);   // Lerp �ε巴�� �̵��ϵ��� �����ش�.
        
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
