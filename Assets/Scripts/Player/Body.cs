using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Body : MonoBehaviour
{
    public UnityEvent OnMoneyed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Money")
        {
            GameManager.Data.Money++;
            OnMoneyed?.Invoke();    // �Ծ��� �� �߻���ų �̺�Ʈ�� ������ �������
        }
        else if (collision.tag == "Key")
        {
            GameManager.Data.Keys++;
        }
        else if ( collision.tag == "Bomb")
        {
            GameManager.Data.Bombs++;
        }
    }
}
