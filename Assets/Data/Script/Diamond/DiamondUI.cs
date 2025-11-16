using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiamondUI : MonoBehaviour
{
    private TMP_Text _textDiamond;
    private void Awake()
    {
        _textDiamond = GetComponent<TMP_Text>();
    }
    private void Start()
    {
        RefreshUI();
    }
    private void OnEnable()
    {
        CurrencyManager.OnDiamondChange += RefreshUI;
        RefreshUI();
    }
    private void OnDisable()
    {
        CurrencyManager.OnDiamondChange -= RefreshUI;
    }
    private void RefreshUI()
    {
        if (_textDiamond == null || CurrencyManager.Instance == null) return;
        _textDiamond.text = $"{CurrencyManager.Instance.GetCurrentDiamond()}";
    }
}
