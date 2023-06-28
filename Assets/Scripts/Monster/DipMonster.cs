using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UIElements;

public class DipMonster : MonoBehaviour
{
    public float moveSpeed;

    private Animator anim;
    private Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(MoevingTiming());
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
    
    public void StopMoving()
    {
        anim.SetBool("Ismove", false);
    }*/

    IEnumerator MoevingTiming()
    {
        while (true)
        {
            Vector3 vec3 = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);    // 랜덤으로 위치를 생성한다.
            rb.AddForce(vec3 * moveSpeed, ForceMode2D.Impulse);         // ForceMode2D.Impulse로 시작부터 한번에 훅 가도록

            anim.SetBool("IsMove", true);
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
            rb.velocity = Vector2.zero;
            anim.SetBool("IsMove", false);
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
        
        /*Vector3 vec3 = new Vector3(transform.position.x + Random.Range(-2.5f, 2.5f), transform.position.y + Random.Range(-2.5f, 2.5f), 0);
        Vector2 monsterVect = transform.position;
        bool endPoint = true;
        while (endPoint)
        {
            transform.position = Vector3.Lerp(transform.position, vec3, 0.001f);
            isMoving = true;
            if ((transform.position - vec3).sqrMagnitude <= 0.01f)
            {
                endPoint = false;
            }
        }
        
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        isMoving = false;
        endPoint = true;
        yield return null;*/
    }
    
}
