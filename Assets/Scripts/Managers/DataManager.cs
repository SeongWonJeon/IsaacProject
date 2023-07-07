using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    private int money;
    private int bombs;
    private int keys;
    private int playerHP = 3;
    private int monsterDamage = 1;
    private float bombDamage = 100;
    private float moveSpeed = 6.0f;
    private float attackSpeed = 1.1f;
    private float attackDamage = 1f;
    private float attackRange = 6f;

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

    public int PlayerHP
    {
        get { return playerHP; }
        set
        {
            PlayerHP = value;
        }
    }
    public int MonsterDamage
    {
        get { return monsterDamage; }
        set
        {
            MonsterDamage = value;
        }
    }
    public float BombDamage
    {
        get { return bombDamage; }
        set
        {
            BombDamage = value;
        }
    }

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set
        {
            OnCurMoveSpeedChanged?.Invoke(value);
            moveSpeed = value;
        }
    }
    public event UnityAction<float> OnCurMoveSpeedChanged;

    public float AttackSpeed
    {
        get { return attackSpeed; }
        set
        {
            OnCurAttackSpeedChanged?.Invoke(value);
            attackSpeed = value;
        }
    }
    public event UnityAction<float> OnCurAttackSpeedChanged;

    public float AttackDamage
    {
        get { return attackDamage; }
        set
        {
            OnCurAttackDamageChanged?.Invoke(value);
            attackDamage = value;
        }
    }
    public event UnityAction<float> OnCurAttackDamageChanged;

    public float AttackRange
    {
        get { return attackRange; }
        set
        {
            OnCurAttackRangeChanged?.Invoke(value);
            attackRange = value;
        }
    }
    public event UnityAction<float> OnCurAttackRangeChanged;
}
