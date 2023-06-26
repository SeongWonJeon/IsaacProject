using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    //[SerializeField] TMP_Text coinNumber;
    private Animator anim;
    private new Collider2D collider2D;
    private AudioSource coinPickUp;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        //coinNumber = GameObject.Find("CoinText").GetComponent<TMP_Text>();
        collider2D = GetComponent<Collider2D>();
        coinPickUp = GetComponentInChildren<AudioSource>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Body")
        {
            anim.SetTrigger("IsTrigger");
            collider2D.enabled = false;
        }
    }
    public void PickUp()
    {
        Destroy(this.gameObject);
    }
}
