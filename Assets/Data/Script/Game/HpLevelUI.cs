using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpLevelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text tMP_Text;
    [SerializeField] private Image _heal;
    //for UI bar hp
    private float _targetFill;
    private float _currentFill;
    private void Awake()
    {
        if (_heal == null) _heal = GetComponent<Image>();
        if (tMP_Text == null)
        {
            tMP_Text = GetComponentInChildren<TMP_Text>();
        }
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
        _targetFill = (float)current / total;
        tMP_Text.text = $"{current}/{total}";
    }
    private void Update()
    {
        if (_heal != null)
        {
            _currentFill = Mathf.Lerp(_currentFill, _targetFill, Time.deltaTime * 5f);
            _heal.fillAmount = _currentFill;
        }
    }
}
