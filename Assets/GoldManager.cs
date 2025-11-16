using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance { get; private set;}
    private int _totalGold;
    //event
    public static event Action<int> OnGoldChanged;
    
    void Awake()
    {
        Instance = this;
    }
    public void SetTotalGold(int totalGold)
    {
        _totalGold = totalGold;
        OnGoldChanged?.Invoke(_totalGold);
    }
    public int GetTotalGold()
    {
        return _totalGold;
    }
    public void ChangeTotalGold(int gold)
    {
        _totalGold += gold;
        OnGoldChanged?.Invoke(_totalGold);
    }
}
