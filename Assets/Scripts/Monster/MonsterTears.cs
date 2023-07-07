using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTears : MonoBehaviour
{
    [SerializeField] float Speed;

    private Coroutine fireCoroutine;

    Vector3 pos;

    private Rigidbody2D rb;

    private Animator anim;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        fireCoroutine = StartCoroutine(TearsFire());
    }

    public void SetStartPos(Vector3 pos)
    {
        this.pos = pos;
    }

    public void Disappear()
    {
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Structure")
        {
            anim.SetBool("IsTouch", false);
            gameObject.SetActive(false);
            StopCoroutine(fireCoroutine);
            GameManager.Pool.Release(this);
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Isaac>().TakeDamage(GameManager.Data.MonsterDamage);
            anim.SetBool("IsTouch", false);
            gameObject.SetActive(false);
            StopCoroutine(fireCoroutine);
            GameManager.Pool.Release(this);
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
        rb.AddForce(transform.right * Speed, ForceMode2D.Impulse);
        anim.SetBool("IsTouch", true);
        yield return null;
    }
}
