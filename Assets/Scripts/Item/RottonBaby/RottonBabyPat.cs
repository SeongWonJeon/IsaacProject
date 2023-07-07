using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RottonBabyPat : MonoBehaviour
{
    public float speed;
    public Vector3 followPos;
    public int followDelay;
    public Transform parent;
    public float ar;

    private void Awake()
    {
        parent = GameObject.Find("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        Watch();
    }
    private void FixedUpdate()
    {
        Follow();
        
    }

    private void Watch()
    {
        ar = Vector3.SqrMagnitude(parent.position - transform.position);        // SqrMagnitude �� ������Ʈ ������ �Ÿ��� ����� �����ش�.

        if (ar > 1)
        {
            followPos = parent.position;
        }
        
    }

    private void Follow()
    {
        if (ar > 1)
            transform.position = Vector3.Lerp(transform.position, followPos, Time.deltaTime * speed);   // Lerp �ε巴�� �̵��ϵ��� �����ش�.
    }
}
