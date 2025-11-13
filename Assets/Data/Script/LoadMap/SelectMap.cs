using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectMap : MonoBehaviour
{
    private Button _button;
    public static event Action<string> OnMapSelected;
    private TMP_Text levelMap;
    void Start()
    {
        _button = GetComponent<Button>();
        levelMap = gameObject.GetComponentInChildren<TMP_Text>();
        if (_button != null && levelMap != null)
        {
            _button.onClick.AddListener(() => OnMapSelected?.Invoke(levelMap.text));
        }
    }
}
