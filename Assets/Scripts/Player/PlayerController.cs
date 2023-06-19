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


    private Rigidbody2D rb;
    private Vector2 inputDir;
    private Vector2 _inputDir;
    public Animator[] anim;
    private SpriteRenderer[] renderer;
   

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
        rb.AddForce(Vector2.right * inputDir.x *moveSpeed, ForceMode2D.Force);
        rb.AddForce(Vector2.up * inputDir.y * moveSpeed, ForceMode2D.Force);

        if (rb.velocity.x < 0)
            renderer[2].flipX = true;
        else if (rb.velocity.x > 0)
            renderer[2].flipX = false;

        anim[1].SetFloat("MoveSpeedX", rb.velocity.x);
        anim[1].SetFloat("MoveSpeedY", rb.velocity.y);


        
    }
    
    private void OnMove(InputValue value)
    {
        inputDir = value.Get<Vector2>();

        if (inputDir.x > 0)
        {
            renderer[1].flipX = false;
            anim[0].SetBool("RightMoveHead", true);
        }
        else if (inputDir.x < 0)
        {
            renderer[1].flipX = true;
            anim[0].SetBool("LeftMoveHead", true);
        }
        else if (inputDir.x == 0)
        {
            anim[0].SetBool("RightMoveHead", false);
            anim[0].SetBool("LeftMoveHead", false);
        }

        if (inputDir.y > 0)
        {
            anim[0].SetBool("UpMoveHead", true);
        }
        else if (inputDir.y < 0)
        {
            anim[0].SetBool("DownMoveHead", true);
        }
        else if (inputDir.y == 0)
        {
            anim[0].SetBool("UpMoveHead", false);
            anim[0].SetBool("DownMoveHead", false);
        }
    }
    private void OnAttack(InputValue value)
    {
        _inputDir = value.Get<Vector2>();
        if (_inputDir.x > 0)
        {
            renderer[1].flipX = false;
            anim[0].SetBool("RightAttack", true);
            
        }
        else if (_inputDir.x < 0)
        {
            renderer[1].flipX = true;
            anim[0].SetBool("LeftAttack", true);
        }
        else if (_inputDir.x == 0)
        {
            anim[0].SetBool("RightAttack", false);
            anim[0].SetBool("LeftAttack", false);
        }

        if (_inputDir.y > 0)
        {
            anim[0].SetBool("UpAttack", true);
        }
        else if (_inputDir.y < 0)
        {
            anim[0].SetBool("DownAttack", true);
        }
        else if (_inputDir.y == 0)
        {
            anim[0].SetBool("UpAttack", false);
            anim[0].SetBool("DownAttack", false);
        }
    }
    
}
