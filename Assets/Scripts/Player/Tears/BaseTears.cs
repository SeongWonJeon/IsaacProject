using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTears : MonoBehaviour
{
    [SerializeField] float Speed;
    public float tearsMaxSpeed;
    public float tearsMaxRange;

    public Coroutine fireCoroutine;
    public Coroutine coolCoroutine;

    public Vector3 pos;

    public Rigidbody2D rb;

    public Animator[] anim;


    protected virtual void Awake()
    {
        tearsMaxSpeed = GameManager.Data.AttackSpeed;
        tearsMaxRange = GameManager.Data.AttackRange;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentsInChildren<Animator>();
    }
    protected virtual void OnEnable()
    {
        anim[0].SetBool("IsTouch", false);
        fireCoroutine = StartCoroutine(TearsFire());
    }

    public virtual void SetStartPos(Vector3 pos)
    {
        this.pos = pos;
    }

    protected virtual void Disappear()
    {
        gameObject.SetActive(false);
        GameManager.Pool.Release(this);
        anim[0].SetBool("IsTouch", false);
        
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Monster" || collision.gameObject.tag == "Structure")
        {
            anim[0].SetBool("IsTouch", true);
            StopCoroutine(fireCoroutine);

        }
    }

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
                StopCoroutine(fireCoroutine);               // 계속 if문이 반복되서 안에서 멈추도록 하였다
            }
            else
            {
                
                anim[0].SetBool("IsTouch", false);
            }
            
            yield return null;
        }
        
    }

    IEnumerator CoolRoutine()           //TODO : 몇초후에 터지도록
    {
        yield return null;
        yield return new WaitForSeconds(0.5f);
        anim[0].SetBool("IsTouch", true);
        rb.velocity = Vector3.zero;
        yield return null;
    }
}
