using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpLevelManager : MonoBehaviour
{
    public static HpLevelManager Instance { get; private set; }
    [SerializeField] private int _totalHP;
    [SerializeField] private int _currentHP;
    //event
    public static event Action<int, int> OnHpChanged;
    public static event Action OnZeroHp;

    private void Awake()
    {
        Instance = this;
    }
    public void InitTotalHP(int hp)
    {
        _totalHP = hp;
        _currentHP = _totalHP;
        OnHpChanged?.Invoke(_currentHP, _totalHP);
    }
    public int GetHp()
    {
        return _currentHP;
    }
    public void ChangeHp(int hp)
    {
        _currentHP += hp;
        OnHpChanged?.Invoke(_currentHP, _totalHP);
        if (_currentHP <= 0) OnZeroHp?.Invoke();
    }
}
