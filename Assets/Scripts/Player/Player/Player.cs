using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Jobs;

abstract class Player : MonoBehaviour, IDamagable
{
    public float moveSpeed;
    public float maxSpeed;
    public float damage;
    public float playerHp;
    public float lastFire;
    public float fireDelay;
    public float tearsScale;
    public GameObject tears;
    public Transform leftEye;
    public Transform rightEye;

    protected UnityEvent OnAttacked;

    public Vector2 moveInputDir;
    public Vector2 attackInputDir;

    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    public Rigidbody2D rb;
    protected Animator anim;
    public Animator[] animChild;
    public Transform[] childs;
    public new SpriteRenderer[] renderer;

    public bool turnHead = true;
    public bool isPossible;
    public bool dead;
    public bool isHurt;
    public bool eyeTurn;

    bool isPressed;

    List<Item> itemList;


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        animChild = GetComponentsInChildren<Animator>();
        renderer = GetComponentsInChildren<SpriteRenderer>();
        tears = GameManager.Resource.Load<GameObject>("Attack/Tears");
        childs = GetComponentsInChildren<Transform>();
        leftEye = GameObject.Find("LeftEye").GetComponent<Transform>();
        rightEye = GameObject.Find("RightEye").GetComponent<Transform>();
    }

    protected virtual void Start()
    {
        itemList = new List<Item>();
        damage = GameManager.Data.AttackDamage;
        moveSpeed = GameManager.Data.MoveSpeed;
        maxSpeed = GameManager.Data.MoveSpeed;
        playerHp = GameManager.Data.PlayerHP;
    }
    protected void Update()
    {
        
        Move();

        Fire();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            anim.SetTrigger("GetItem");
            StartCoroutine(GetItemPose(collision.gameObject.GetComponent<Item>()));
            collision.collider.isTrigger = true;
            itemList.Add(collision.gameObject.GetComponent<Item>());
        }
    }

    public void Move()
    {
        if (!dead)
        {
            if (moveInputDir.x < 0 && rb.velocity.x > -maxSpeed)
                rb.AddForce(Vector2.right * moveInputDir.x * moveSpeed, ForceMode2D.Force);
            else if (moveInputDir.x > 0 && rb.velocity.x < maxSpeed)
                rb.AddForce(Vector2.right * moveInputDir.x * moveSpeed, ForceMode2D.Force);

            if (moveInputDir.y < 0 && rb.velocity.y > -maxSpeed)
                rb.AddForce(Vector2.up * moveInputDir.y * moveSpeed, ForceMode2D.Force);
            else if (moveInputDir.y > 0 && rb.velocity.y < maxSpeed)
                rb.AddForce(Vector2.up * moveInputDir.y * moveSpeed, ForceMode2D.Force);



            if (rb.velocity.x < 0)
                renderer[3].flipX = true;
            else if (rb.velocity.x > 0)
                renderer[3].flipX = false;

            animChild[3].SetFloat("MoveSpeedX", rb.velocity.x);
            animChild[3].SetFloat("MoveSpeedY", rb.velocity.y);

        }
    }

    public void OnMove(InputValue value)
    {
        moveInputDir = value.Get<Vector2>();
        if (itemList.Count > 0)
        {
            foreach (Item item in itemList)
            {
                item.MoveUse();
            }
        }

        if (moveInputDir.x > 0)
        {
            if (turnHead == true)
            {
                renderer[1].flipX = false;
                animChild[1].SetBool("RightMoveHead", true);
            }
        }
        else if (moveInputDir.x < 0)
        {
            if (turnHead == true)
            {
                renderer[1].flipX = true;
                animChild[1].SetBool("LeftMoveHead", true);
            }
        }
        else if (moveInputDir.x == 0)
        {
            if (turnHead == true)
            {
                animChild[1].SetBool("RightMoveHead", false);
                animChild[1].SetBool("LeftMoveHead", false);
            }
            
        }

        if (moveInputDir.y > 0)
        {

            animChild[1].SetBool("UpMoveHead", true);
        }
        else if (moveInputDir.y < 0)
        {
            animChild[1].SetBool("DownMoveHead", true);
        }
        else if (moveInputDir.y == 0)
        {
            animChild[1].SetBool("UpMoveHead", false);
            animChild[1].SetBool("DownMoveHead", false);
        }
    }

    public void OnAttack(InputValue value)
    {
        attackInputDir = value.Get<Vector2>();
        turnHead = value.Get<Vector2>().magnitude == 0;

        isPressed = !isPressed;
        if (!isPressed)
            AnimStop();
        OnAttacked?.Invoke();       // 공격했을 때(눈을 감았을 때)발생시킬 소리등등 구현하기
    }

    public void Fire()
    {
        if (!dead && !isHurt)
        {
            if (itemList.Count > 0)
            {
                foreach (Item item in itemList)
                {
                    item.FireUse();
                }
            }

            if (attackInputDir.x > 0)           // 오른쪽을 바라본다
            {
                renderer[1].flipX = false;
                animChild[1].SetBool("RightAttack", true);
                GetTears(new Vector3(0, 0, 0), tears);
            }
            else if (attackInputDir.x < 0)      // 왼쪽
            {
                renderer[1].flipX = true;
                animChild[1].SetBool("LeftAttack", true);
                GetTears(new Vector3(0, 180f, 0), tears);
            }
            else if (attackInputDir.x == 0)
            {
                animChild[1].SetBool("RightAttack", false);
                animChild[1].SetBool("LeftAttack", false);
            }

            if (attackInputDir.y > 0)           // 위
            {
                animChild[1].SetBool("UpAttack", true);
                GetTears(new Vector3(0,0,90f), tears);
            }
            else if (attackInputDir.y < 0)      // 아래
            {
                animChild[1].SetBool("DownAttack", true);
                GetTears(new Vector3(0,0,-90f), tears);

            }
            else if (attackInputDir.y == 0)
            {
                animChild[1].SetBool("UpAttack", false);
                animChild[1].SetBool("DownAttack", false);
            }
            
        }
        
    }

    public void GetTears(Vector3 eulerAngle, GameObject newTearsPrefab)
    {
        if (Time.time > lastFire + fireDelay)
        {
            GameObject tearsPrefab = GameManager.Pool.Get(newTearsPrefab);
            tearsPrefab.GetComponent<BaseTears>().SetStartPos(transform.position);
            eyeTurn = !eyeTurn;
            if (eyeTurn)
            {
                tearsPrefab.transform.position = leftEye.transform.position;
            }
            else
                tearsPrefab.transform.position = rightEye.transform.position;
            tearsPrefab.transform.rotation = Quaternion.Euler(eulerAngle);
            tearsPrefab.transform.localScale = new Vector3(4.5f + tearsScale, 4.5f + tearsScale, 0);
            // .AddForce로 눈물을 발사할 때 캐릭터가 움직이는 방향의 velocity만큼 힘들 더 가해주거나 덜 가해주는 방법
            tearsPrefab.GetComponent<Rigidbody2D>().AddForce(rb.velocity * 0.09f, ForceMode2D.Impulse);
            lastFire = Time.time;
        }
    }


    public void AnimStop()
    {
        animChild[1].SetBool("RightAttack", false);
        animChild[1].SetBool("LeftAttack", false);
        animChild[1].SetBool("UpAttack", false);
        animChild[1].SetBool("DownAttack", false);
        animChild[1].SetBool("UpMoveHead", false);
        animChild[1].SetBool("DownMoveHead", false);
        animChild[1].SetBool("RightMoveHead", false);
        animChild[1].SetBool("LeftMoveHead", false);
        animChild[2].SetBool("Right", false);
        animChild[2].SetBool("Left", false);
    }

    IEnumerator Invincibility()
    {
        isHurt = true;
        
        StartCoroutine(StopDamageSecond());

        while (isHurt)
        {
            renderer[0].color = halfA;
            yield return new WaitForSeconds(0.2f);
            renderer[0].color = fullA;
            yield return new WaitForSeconds(0.2f);
        }
        
        yield return null;
    }

    IEnumerator StopDamageSecond()
    {
        animChild[0].SetBool("OnDamage", true);
        for (int i = 1; i < childs.Length; i++)
        {
            childs[i].gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(1.5f);
        animChild[0].SetBool("OnDamage", false);
        if (!dead)
        {
            for (int i = 1; i < childs.Length; i++)
            {
                childs[i].gameObject.SetActive(true);
            }
        }
        isHurt = false;
        yield return null;
    }
    IEnumerator StopTime()
    {
        anim.enabled = true;
        // TODO : 사운드 재생해주고
        animChild[0].SetBool("OnDie", true);
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
        // TODO : 죽었을 때 UI띄워주기
        yield return null;
    }

    IEnumerator GetItemPose(Item item)
    {
        yield return null;

        for (int i = 1; i < childs.Length; i++)
        {
            childs[i].gameObject.SetActive(false);
        }

        yield return null;
        
        SpriteRenderer render = GameManager.Resource.Instantiate<SpriteRenderer>("Prefab/GetItemPos", transform.position + (transform.up * 1.2f), Quaternion.identity, transform);
        render.sprite = item.spriteRenderer[1].sprite;

        yield return new WaitForSeconds(1);

        Destroy(render.gameObject);

        for (int i = 1; i < childs.Length; i++)
        {
            childs[i].gameObject.SetActive(true);
        }

        yield return null;
    }

    public void TakeDamage(float damage)
    {
        if (!isHurt)
        {
            playerHp -= damage;
            StartCoroutine(Invincibility());
        }

        if (playerHp <= 0 && !dead)
        {
            Die();
        }
    }


    protected void Die()           // TODO : 1. 씬을 전환해서 state변경 2. 1.5초동안 데미지를 입지 않는 상태로
    {
        dead = true;
        StartCoroutine(StopTime());
        for (int i = 1; i < childs.Length; i++)
        {
            childs[i].gameObject.SetActive(false);
        }

    }
}
