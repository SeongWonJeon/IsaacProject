using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tears : MonoBehaviour
{
    [SerializeField] float Speed;
    private float tearsMaxSpeed;
    private float tearsMaxRange;

    private Coroutine fireCoroutine;
    private Coroutine coolCoroutine;
    
    Vector3 pos;

    private Rigidbody2D rb;

    private Animator anim;


    private void Awake()
    {
        tearsMaxSpeed = GameManager.Data.AttackSpeed;
        tearsMaxRange = GameManager.Data.AttackRange;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        anim.SetBool("IsTouch", false);
        fireCoroutine = StartCoroutine(TearsFire());
    }

    public void SetStartPos(Vector3 pos)
    {
        this.pos = pos;
    }

    public void Disappear()
    {
        gameObject.SetActive(false);
        GameManager.Pool.Release(this);
        anim.SetBool("IsTouch", false);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Monster" || collision.gameObject.tag == "Structure")
        {
            anim.SetBool("IsTouch", true);
            StopCoroutine(fireCoroutine);
        }
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            anim.SetBool("IsTouch", true);
            StopCoroutine(fireCoroutine);
        }
        
    }*/

    IEnumerator TearsFire()
    {
        yield return null;
        rb.AddForce(transform.right * tearsMaxSpeed, ForceMode2D.Impulse);
        
        while (true)
        {
            if (pos.x < gameObject.transform.position.x - tearsMaxRange || pos.x > gameObject.transform.position.x + tearsMaxRange
            || pos.y < gameObject.transform.position.y - tearsMaxRange || pos.y > gameObject.transform.position.y + tearsMaxRange)
            {
                coolCoroutine = StartCoroutine(CoolRoutine());
                StopCoroutine(fireCoroutine);               // ��� if���� �ݺ��Ǽ� �ȿ��� ���ߵ��� �Ͽ���
            }
            else
            {
                
                anim.SetBool("IsTouch", false);

            }
            
            yield return null;
        }
        
    }

    IEnumerator CoolRoutine()           //TODO : �����Ŀ� ��������
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("IsTouch", true);
        rb.velocity = Vector3.zero;
        yield return null;
    }
}
