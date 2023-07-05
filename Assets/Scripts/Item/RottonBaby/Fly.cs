using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private List<GameObject> monsters;
    GameObject firstMonster;

    Vector3 monster;
    Vector3 startPoint;
    private void Awake()
    {
        monsters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Monster"));
        monster = monsters[0].transform.position;
    }
    private void Start()
    {
        firstMonster = monsters[0];
    }
    private void Update()
    {
        //Vector3.Distance()
    }
}
