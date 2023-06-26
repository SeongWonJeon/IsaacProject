using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tears : MonoBehaviour
{
    [SerializeField] float Speed;
    private float moveSpeed;
    private float tearsMaxSpeed;
    private float tearsMaxRange;

    private Coroutine coroutine;
    
    Vector3 pos;

    private Animator anim;

    private void Awake()
    {
        moveSpeed = GameManager.Data.MoveSpeed;
        tearsMaxSpeed = GameManager.Data.AttackSpeed;
        tearsMaxRange = GameManager.Data.AttackRange;
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        anim.SetBool("IsTouch", false);
        coroutine = StartCoroutine(TearsFire());
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "UI")
        {
            anim.SetBool("IsTouch", true);
            StopCoroutine(coroutine);
        }
        else
            anim.SetBool("IsTouch", false);
    }

    IEnumerator TearsFire()
    {
        //TODO : ������ ���� �� �̵��ϴ� ���⿡ ���ؼ� ������ �ӵ��� ������ ��Ÿ��� ������ �ǵ��� �����ϱ�
        while (true)
        {
            if (pos.x < gameObject.transform.position.x - tearsMaxRange || pos.x > gameObject.transform.position.x + tearsMaxRange
            || pos.y < gameObject.transform.position.y - tearsMaxRange || pos.y > gameObject.transform.position.y + tearsMaxRange)
            {
                anim.SetBool("IsTouch", true);
            }
            else
            {
                transform.Translate(Vector2.right * tearsMaxSpeed * Time.deltaTime);
                anim.SetBool("IsTouch", false);
            }
            
            yield return null;
        }
        
    }
}
