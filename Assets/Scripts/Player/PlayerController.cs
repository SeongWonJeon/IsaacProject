using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Jobs;

public class PlayerController : MonoBehaviour, IDamagable
{
    [SerializeField] float moveSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] Eyes eyes;
    private float damage;
    private float playerHp;
    GameObject tears;

    public UnityEvent OnAttacked;

    private Vector2 moveInputDir;
    private Vector2 attackInputDir;

    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    private Rigidbody2D rb;
    private Animator animator;
    public Animator[] anim;
    Transform[] childs;
    private new SpriteRenderer[] renderer;

    private bool turnHead = true;
    private bool isPossible;
    private bool dead;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentsInChildren<Animator>();
        animator = GetComponent<Animator>();
        renderer = GetComponentsInChildren<SpriteRenderer>();
        tears = GameManager.Resource.Load<GameObject>("Attack/Tears");
        childs = GetComponentsInChildren<Transform>();
        damage = GameManager.Data.AttackDamage;
        playerHp = GameManager.Data.PlayerHP;
    }
    private void Update()
    {
        Move();
        
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
                renderer[2].flipX = true;
            else if (rb.velocity.x > 0)
                renderer[2].flipX = false;

            anim[2].SetFloat("MoveSpeedX", rb.velocity.x);
            anim[2].SetFloat("MoveSpeedY", rb.velocity.y);
        }
        
    }
    
    private void OnMove(InputValue value)
    {
        moveInputDir = value.Get<Vector2>();

        if (moveInputDir.x > 0)
        {
            if (turnHead == true)
            {
                renderer[1].flipX = false;
                anim[1].SetBool("RightMoveHead", true);
            }
        }
        else if (moveInputDir.x < 0)
        {
            if (turnHead == true)
            {
                renderer[1].flipX = true;
                anim[1].SetBool("LeftMoveHead", true);
            }
        }
        else if (moveInputDir.x == 0)
        {
            anim[1].SetBool("RightMoveHead", false);
            anim[1].SetBool("LeftMoveHead", false);
        }

        if (moveInputDir.y > 0)
        {
            anim[1].SetBool("UpMoveHead", true);
        }
        else if (moveInputDir.y < 0)
        {
            anim[1].SetBool("DownMoveHead", true);
        }
        else if (moveInputDir.y == 0)
        {
            anim[1].SetBool("UpMoveHead", false);
            anim[1].SetBool("DownMoveHead", false);
        }
    }
    private void OnAttack(InputValue value)
    {
        attackInputDir = value.Get<Vector2>();
        turnHead = value.Get<Vector2>().magnitude == 0;

        if (!isPossible)
        {
            Fire();
            
        }
        OnAttacked?.Invoke();       // 공격했을 때(눈을 감았을 때)발생시킬 소리등등 구현하기
    }

    public void Fire()
    {
        if (!dead && !isHurt)
        {
            if (attackInputDir.x > 0)           // 오른쪽을 바라본다
            {
                renderer[1].flipX = false;
                anim[1].SetBool("RightAttack", true);
                GameObject tearsPrefab = GameManager.Pool.Get(tears);
                tearsPrefab.GetComponent<Tears>().SetStartPos(transform.position);
                tearsPrefab.transform.position = transform.position;
                tearsPrefab.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                // 눈물을 발사할 때 캐릭터가 움직이는 방향의 velocity만큼 힘들 더 가해주거나 덜 가해주는 방법
                tearsPrefab.GetComponent<Rigidbody2D>().AddForce(rb.velocity * 0.09f, ForceMode2D.Impulse);
            }
            else if (attackInputDir.x < 0)      // 왼쪽
            {
                renderer[1].flipX = true;
                anim[1].SetBool("LeftAttack", true);
                GameObject tearsPrefab = GameManager.Pool.Get(tears);
                tearsPrefab.GetComponent<Tears>().SetStartPos(transform.position);
                tearsPrefab.transform.position = transform.position;
                tearsPrefab.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                tearsPrefab.GetComponent<Rigidbody2D>().AddForce(rb.velocity * 0.09f, ForceMode2D.Impulse);
            }
            else if (attackInputDir.x == 0)
            {
                anim[1].SetBool("RightAttack", false);
                anim[1].SetBool("LeftAttack", false);
            }

            if (attackInputDir.y > 0)           // 위
            {
                anim[1].SetBool("UpAttack", true);
                GameObject tearsPrefab = GameManager.Pool.Get(tears);
                tearsPrefab.GetComponent<Tears>().SetStartPos(transform.position);
                tearsPrefab.transform.position = transform.position;
                tearsPrefab.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                tearsPrefab.GetComponent<Rigidbody2D>().AddForce(rb.velocity * 0.09f, ForceMode2D.Impulse);
            }
            else if (attackInputDir.y < 0)      // 아래
            {
                anim[1].SetBool("DownAttack", true);
                GameObject tearsPrefab = GameManager.Pool.Get(tears);
                tearsPrefab.GetComponent<Tears>().SetStartPos(transform.position);
                tearsPrefab.transform.position = transform.position;
                tearsPrefab.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                tearsPrefab.GetComponent<Rigidbody2D>().AddForce(rb.velocity * 0.09f, ForceMode2D.Impulse);
            }
            else if (attackInputDir.y == 0)
            {
                anim[1].SetBool("UpAttack", false);
                anim[1].SetBool("DownAttack", false);
            }
        }
        
    }
    
    private bool isHurt;
    public void TakeDamage(float damage)
    {
        Debug.Log("1");
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
    
    
    public void Die()           // TODO : 1. 씬을 전환해서 state변경 2. 1.5초동안 데미지를 입지 않는 상태로
    {
        dead = true;
        StartCoroutine(StopTime());
        for (int i = 1; i < childs.Length; i++)
        {
            childs[i].gameObject.SetActive(false);
        }
        
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
        anim[0].SetBool("OnDamage", true);
        for (int i = 1; i < childs.Length; i++)
        {
            childs[i].gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(1.5f);
        anim[0].SetBool("OnDamage", false);
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
        animator.enabled = true;
        // TODO : 사운드 재생해주고
        anim[0].SetBool("OnDie", true);
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
        // TODO : 죽었을 때 UI띄워주기
        yield return null;
    }

    IEnumerator AttackTime()
    {
        yield return null;
        isPossible = true;
        yield return new WaitForSeconds(0.5f);
        isPossible = false;
        yield return null;
    }
}
