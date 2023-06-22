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
        OnMoneyed?.Invoke();    // �Ծ��� �� �߻���ų �̺�Ʈ�� ������ �������
    }
}
