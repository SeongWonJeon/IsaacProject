using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float maxSpeed;


    private Rigidbody2D rb;
    private Vector2 moveInputDir;
    private Vector2 attackInputDir;
    public Animator[] anim;
    private new SpriteRenderer[] renderer;

    private bool turnHead = true;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentsInChildren<Animator>();
        renderer = GetComponentsInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        Move();
        
    }

    public void Move()
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

        anim[1].SetFloat("MoveSpeedX", rb.velocity.x);
        anim[1].SetFloat("MoveSpeedY", rb.velocity.y);
    }
    
    private void OnMove(InputValue value)
    {
        moveInputDir = value.Get<Vector2>();

        if (moveInputDir.x > 0)
        {
            if (turnHead == true)
            {
                renderer[1].flipX = false;
                anim[0].SetBool("RightMoveHead", true);
            }
        }
        else if (moveInputDir.x < 0)
        {
            if (turnHead == true)
            {
                renderer[1].flipX = true;
                anim[0].SetBool("LeftMoveHead", true);
            }
        }
        else if (moveInputDir.x == 0)
        {
            anim[0].SetBool("RightMoveHead", false);
            anim[0].SetBool("LeftMoveHead", false);
        }

        if (moveInputDir.y > 0)
        {
            anim[0].SetBool("UpMoveHead", true);
        }
        else if (moveInputDir.y < 0)
        {
            anim[0].SetBool("DownMoveHead", true);
        }
        else if (moveInputDir.y == 0)
        {
            anim[0].SetBool("UpMoveHead", false);
            anim[0].SetBool("DownMoveHead", false);
        }
    }
    private void OnAttack(InputValue value)
    {
        attackInputDir = value.Get<Vector2>();
        turnHead = value.Get<Vector2>().magnitude == 0;

        if (attackInputDir.x > 0)
        {
            renderer[1].flipX = false;
            anim[0].SetBool("RightAttack", true);
        }
        else if (attackInputDir.x < 0)
        {
            renderer[1].flipX = true;
            anim[0].SetBool("LeftAttack", true);
        }
        else if (attackInputDir.x == 0)
        {
            anim[0].SetBool("RightAttack", false);
            anim[0].SetBool("LeftAttack", false);
        }

        if (attackInputDir.y > 0)
        {
            anim[0].SetBool("UpAttack", true);
        }
        else if (attackInputDir.y < 0)
        {
            anim[0].SetBool("DownAttack", true);
        }
        else if (attackInputDir.y == 0)
        {
            anim[0].SetBool("UpAttack", false);
            anim[0].SetBool("DownAttack", false);
        }
    }
    
}
