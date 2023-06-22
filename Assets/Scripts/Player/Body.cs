using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Body : MonoBehaviour
{
    private UnityEvent OnMoneyed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Data.Money++;
        OnMoneyed?.Invoke();    // 먹었을 때 발생시킬 이벤트가 있으면 사용하자
    }
}
