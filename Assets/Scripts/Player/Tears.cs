using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tears : MonoBehaviour
{
    [SerializeField] private float tearsSpeed;
    private int damage;

    

    public void SetDamage(int damage)   // TODO : �ٽ� �����ؼ� ¥����
    {
        this.damage = damage;
    }
}
