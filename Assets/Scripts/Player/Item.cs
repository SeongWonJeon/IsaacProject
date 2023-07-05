using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Item : MonoBehaviour
{
    protected Player controller;
    [SerializeField] protected SpriteRenderer[] eyeRenderer;
    [SerializeField] public SpriteRenderer[] spriteRenderer;
    [SerializeField] protected BoxCollider2D[] colliders;

    public BaseTears tear;
    protected Isaac isaac;

    public GameObject playerTears;


    protected virtual void Awake()
    {
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        controller = GameObject.FindWithTag("Player").gameObject.GetComponent<Player>();
        eyeRenderer = GameObject.FindWithTag("Player").gameObject.GetComponentsInChildren<SpriteRenderer>();
        colliders = GetComponentsInChildren<BoxCollider2D>();
        playerTears = GameManager.Resource.Load<GameObject>("Attack/Tears");
        tear = GameManager.Resource.Load<GameObject>("Attack/Tears").GetComponent<BaseTears>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Body")
        {
            isaac = collision.gameObject.GetComponent<Isaac>();
            spriteRenderer[1].enabled = false;
        }
        
    }

    public virtual void FireUse()
    {
        if (isaac.attackInputDir.x > 0)           // �������� �ٶ󺻴�
        {
            isaac.renderer[2].sortingOrder = 101;
            isaac.animChild[2].SetBool("Right", true);
        }
        else if (isaac.attackInputDir.x < 0)      // ����
        {
            isaac.renderer[2].sortingOrder = 101;
            isaac.animChild[2].SetBool("Left", true);
        }

        if (isaac.attackInputDir.y > 0)           // ��
        {
            isaac.renderer[2].sortingOrder = 98;
            isaac.animChild[2].SetBool("Right", false);
            isaac.animChild[2].SetBool("Left", false);
        }
        else if (isaac.attackInputDir.y < 0)      // �Ʒ�
        {
            isaac.renderer[2].sortingOrder = 101;
        }
    }
    public virtual void MoveUse()
    {
        if (isaac.moveInputDir.x > 0)
        {
            if (isaac.turnHead == true)
            {
                isaac.renderer[2].sortingOrder = 101;
                isaac.animChild[2].SetBool("Right", true);
            }
        }
        else if (isaac.moveInputDir.x < 0)
        {
            if (isaac.turnHead == true)
            {
                isaac.renderer[2].sortingOrder = 101;
                isaac.animChild[2].SetBool("Left", true);
            }
        }
        else if (isaac.moveInputDir.x == 0)
        {
            if (isaac.turnHead == true)
            {
                isaac.renderer[2].sortingOrder = 101;
                isaac.animChild[2].SetBool("Right", false);
                isaac.animChild[2].SetBool("Left", false);
            }

        }

        if (isaac.moveInputDir.y > 0)
        {
            if (isaac.turnHead == true)
            {
                isaac.renderer[2].sortingOrder = 98;
                isaac.animChild[2].SetBool("Right", false);
                isaac.animChild[2].SetBool("Left", false);
            }
        }
    }

    [SerializeField] private float lastFire;
    [SerializeField] private float fireDelay;
    bool eyeTrunPlus;
    public virtual void GetTearsPlus(Vector3 eulerAngle, GameObject newTearsPrefab)
    {
        if (Time.time > lastFire + fireDelay)
        {
            GameObject tearsPrefab = GameManager.Pool.Get(newTearsPrefab);
            tearsPrefab.GetComponent<BaseTears>().SetStartPos(isaac.transform.position);
            eyeTrunPlus = !eyeTrunPlus;
            if (!eyeTrunPlus)
            {
                tearsPrefab.transform.position = isaac.leftEye.transform.position;
            }
            else
                tearsPrefab.transform.position = isaac.rightEye.transform.position;
            tearsPrefab.transform.rotation = Quaternion.Euler(eulerAngle);
            // .AddForce�� ������ �߻��� �� ĳ���Ͱ� �����̴� ������ velocity��ŭ ���� �� �����ְų� �� �����ִ� ���
            tearsPrefab.GetComponent<Rigidbody2D>().AddForce(isaac.rb.velocity * 0.09f, ForceMode2D.Impulse);
            lastFire = Time.time;
        }
    }
}

