using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tears : MonoBehaviour
{
    private Vector2 vec2;

    public void Fire()
    {
        transform.Translate(Vector2.right * vec2.x * GameManager.Data.AttackSpeed);
    }
}
