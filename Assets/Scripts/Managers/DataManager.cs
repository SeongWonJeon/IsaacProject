using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    private int money;
    private int bombs;
    private int keys;
    private float moveSpeed;
    private float attackSpeed;
    private float attackDamage;
    private float attackRange;

    public int Money 
    {
        get { return money; }
        set 
        {
            OnCurMoneyChanged?.Invoke(value);
            money = value;
        }
    }
    public event UnityAction<int> OnCurMoneyChanged; 

    public int Bombs 
    { 
        get {  return bombs; } 
        set
        {
            OnCurBombChanged?.Invoke(value);
            bombs = value; 
        }
    }
    public event UnityAction<int> OnCurBombChanged;

    public int Keys 
    { 
        get { return keys; } 
        set 
        {
            OnCurKeyChanged?.Invoke(value);
            keys = value;
        }
    }
    public event UnityAction<int> OnCurKeyChanged;

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set
        {
            moveSpeed = value;
        }
    }
    public event UnityAction<float> OnCurMoveSpeedChanged;

    public float AttackSpeed
    {
        get { return attackSpeed; }
        set
        {
            attackSpeed = value;
        }
    }
    public event UnityAction<float> OnCurAttackSpeedChanged;

    public float AttackDamage
    {
        get { return attackDamage; }
        set
        {
            attackDamage = value;
        }
    }
    public event UnityAction<float> OnCurAttackDamageChanged;

    public float AttackRange
    {
        get { return attackRange; }
        set
        {
            attackRange = value;
        }
    }
    public event UnityAction<float> OnCurAttackRangeChanged;
}
