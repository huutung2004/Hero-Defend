using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    private TMP_Text _goldText;
    private void Awake()
    {
        _goldText = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        GoldManager.OnGoldChanged += RefreshUI;
    }
    private void OnDisable()
    {
        GoldManager.OnGoldChanged -= RefreshUI;
    }
    private void RefreshUI(int gold)
    {
        _goldText.text = $"{gold}";
    }
}
