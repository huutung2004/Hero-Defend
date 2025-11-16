using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }
    [SerializeField] private int _currentDiamond;
    //event
    public static event Action OnDiamondChange;
    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SetCurrentDiamond(int _diamond)
    {
        _currentDiamond = _diamond;
        OnDiamondChange?.Invoke();
    }
    public int GetCurrentDiamond()
    {
        return _currentDiamond;
    }
    public void ChangeCurrentDiamond(int _diamond)
    {
        _currentDiamond += _diamond;
        OnDiamondChange?.Invoke();
    }

}
