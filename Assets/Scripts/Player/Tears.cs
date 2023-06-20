using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tears : MonoBehaviour
{
    [SerializeField] private float tearsSpeed;
    private int damage;

    

    public void SetDamage(int damage)   // TODO : 다시 생각해서 짜야함
    {
        this.damage = damage;
    }
}
