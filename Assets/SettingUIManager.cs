using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUIManager : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _setting;
    private void Start()
    {
        if (_button == null || _setting == null) return;
        _button.onClick.AddListener(() => ToggleSetting());
        _setting.gameObject.SetActive(false);
    }
    public void ToggleSetting()
    {
        if (_setting.IsActive())
        {
            _setting.gameObject.SetActive(false);
        }
        else _setting.gameObject.SetActive(true);
    }
}
