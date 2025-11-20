using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HpLevelUI : MonoBehaviour
{
    private TMP_Text tMP_Text;
    private void Awake()
    {
        tMP_Text = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        HpLevelManager.OnHpChanged += RefreshUI;
    }
    private void OnDisable()
    {
        HpLevelManager.OnHpChanged -= RefreshUI;
    }
    private void RefreshUI(int current, int total)
    {
        tMP_Text.text = $"Hp: {current}/{total}";
    }
}
