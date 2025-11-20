using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextUpgradeEffect : MonoBehaviour
{
    public static TextUpgradeEffect Instance{ get; set; }
    [Header("Setting")]
    [SerializeField] private float moveDistance = 10f;
    [SerializeField] private float duration = 1f;
    private RectTransform rectTransform;
    void Awake()
    {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        gameObject.SetActive(false);
    }
    public void StartTextEffect(string notify, Color color)
    {
        TMP_Text text = gameObject.GetComponent<TMP_Text>();
        if (!text.IsActive()) gameObject.SetActive(true);
        text.text = notify;
        text.color = color;
        text.alpha = 1f;
        Vector3 startPos = rectTransform.anchoredPosition;
        Vector3 endPos = startPos + Vector3.up * moveDistance;
        Sequence seq = DOTween.Sequence();
        seq.Join(rectTransform.DOAnchorPos(endPos, duration).SetEase(Ease.OutCubic));
        seq.Join(text.DOFade(0f, 0.2f).SetLoops(Mathf.RoundToInt(duration / 0.2f), LoopType.Yoyo));
        seq.OnComplete(() =>
        {
            rectTransform.anchoredPosition = startPos;
            text.alpha = 1f;
            gameObject.SetActive(false);
        });
    }
}
