using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(CanvasGroup))]
public class CircleRotateUI : MonoBehaviour
{
    public static CircleRotateUI Instance{ get; set; }
    private Image _circleImage;
    private CanvasGroup _circleCanvasGroup;
    [Header("Settings")]
    [SerializeField] private float totalRotation = 7200f; // tổng số độ quay
    [SerializeField] private float rotateDuration = 2.5f;
    [SerializeField] private float pauseDuration = 0.5f;
    [SerializeField] private float resetDuration = 0.2f;
    void Awake()
    {
        Instance = this;
        _circleImage = gameObject.GetComponent<Image>();
        _circleCanvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    public void StartEffect(Action onComplete = null)
    {
        if (_circleImage == null || _circleCanvasGroup == null) return;
        //reset rotation
        _circleImage.rectTransform.rotation = Quaternion.identity;
        _circleCanvasGroup.alpha = 1f;

        Sequence seq = DOTween.Sequence();
        //quay nhanh 
        seq.Append(_circleImage.rectTransform
            .DORotate(new Vector3(0, 0, -totalRotation), rotateDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.OutCubic));
        //nhap nhay
        seq.Join(_circleCanvasGroup
            .DOFade(0.2f, 0.25f)
            .SetLoops(Mathf.RoundToInt(rotateDuration / 0.25f), LoopType.Yoyo));
        //dung
        seq.AppendInterval(0.1f);
        //reset rotation
        seq.Append(_circleImage.rectTransform
            .DORotate(Vector3.zero, 0.1f)
            .SetEase(Ease.InOutSine));
        seq.Join(_circleCanvasGroup.DOFade(1f, resetDuration));
        if (onComplete != null)
        {
            seq.OnComplete(() => onComplete.Invoke());
        }
    }
}
