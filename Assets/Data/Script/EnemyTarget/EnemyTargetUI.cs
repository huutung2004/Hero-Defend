using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTargetUI : MonoBehaviour
{
    [SerializeField] private Image _panel;
    [SerializeField] private Image _spriteTarget;
    [SerializeField] private Image _healthBar;
    [SerializeField] private TMP_Text _healthText;
    private EnemyHealth _target;
    private void Update()
    {
        _target = EnemyTargetManager.Instance._target;
        if (_target != null)
        {
            _panel.gameObject.SetActive(true);
            _spriteTarget.sprite = _target.GetSprite();
            _healthBar.fillAmount = _target.GetHealthPercent();
            _healthText.text = $"{_target.GetCurrentHealth()}/{_target.GetMaxHealth()}";
        }
        else
        {
            _panel.gameObject.SetActive(false);
        }
    }
}
