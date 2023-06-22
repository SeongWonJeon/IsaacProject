using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private TMP_Text bombText;
    private void Awake()
    {
        bombText = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        GameManager.Data.OnCurBombChanged += ChangeBomb;
    }
    private void OnDisable()
    {
        GameManager.Data.OnCurBombChanged -= ChangeBomb;
    }
    private void ChangeBomb(int bomb)
    {
        if (bomb < 10)
            bombText.text = ($"0{bomb}");
        else
            bombText.text = bomb.ToString();
    }
}
