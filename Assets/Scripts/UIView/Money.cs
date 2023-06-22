using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    private TMP_Text money_Text;

    private void Awake()
    {
        money_Text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        GameManager.Data.OnCurMoneyChanged += ChangeMoney;
    }
    private void OnDisable()
    {
        GameManager.Data.OnCurMoneyChanged -= ChangeMoney;
    }
    private void ChangeMoney(int money)
    {
        if (money < 10)
            money_Text.text = ($"0{money}");
        else
            money_Text.text = money.ToString();
            
    }
}
