using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardRuntimeUI : MonoBehaviour
{
    public static RewardRuntimeUI Instance;
    [SerializeField] private Image _rewardImg;

    private TMP_Text _textDimond;
    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (_rewardImg == null)
        {
            Debug.LogWarning("Dialog is null");
            return;
        }
        _textDimond = _rewardImg.GetComponentInChildren<TMP_Text>();
        _canvasGroup = _rewardImg.gameObject.GetComponent<CanvasGroup>();
        _rectTransform = _textDimond.GetComponent<RectTransform>();
        _rewardImg.gameObject.SetActive(false);
    }
    public void ShowReward(string text, float duration = 2f)
    {
        MusicManager.Instance.PlayMusic("notify");
        _textDimond.text = text;
        _rewardImg.gameObject.SetActive(true);
        _canvasGroup.alpha = 0;
        _rectTransform.anchoredPosition = new Vector2(0, -100f);
        Sequence seq = DOTween.Sequence();
        seq.Append(_canvasGroup.DOFade(1f, 0.3f))
             .Join(_rectTransform.DOAnchorPosY(0, 0.3f).SetEase(Ease.OutBack))
             .AppendInterval(duration)
             .Append(_canvasGroup.DOFade(0f, 0.3f))
             .Join(_rectTransform.DOAnchorPosY(50, 0.3f).SetEase(Ease.InBack))
             .OnComplete(() => _rewardImg.gameObject.SetActive(false));
    }
}
