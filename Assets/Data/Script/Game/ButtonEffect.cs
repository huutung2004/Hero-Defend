using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class ButtonEffect : MonoBehaviour
{
    private Button _button;
    void Start()
    {
        _button = GetComponent<Button>();
        if (_button == null) Debug.LogWarning("Button is missing");
        _button.onClick.AddListener(() => PlayEffectButton());
    }
    private void PlayEffectButton()
    {
        _button.transform.DOScale(1.2f, 0.2f)
        .SetEase(Ease.OutBounce)
        .OnComplete(() => _button.transform.DOScale(1f, 0.2f));
    }
}
