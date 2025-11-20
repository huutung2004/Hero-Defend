using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;
    [SerializeField] private Image _dialog;
    private TMP_Text _dialogText;
    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if(_dialog == null)
        {
            Debug.LogWarning("Dialog is null");
            return;
        }
        _dialogText =_dialog.GetComponentInChildren<TMP_Text>();
        _canvasGroup = _dialog.gameObject.GetComponent<CanvasGroup>();
        _rectTransform = _dialogText.GetComponent<RectTransform>();
        _dialog.gameObject.SetActive(false);
    }
    public void ShowDialog(string text, float duration = 2f)
    {
        MusicManager.Instance.PlayMusic("notify");
        _dialogText.text = text;
        _dialog.gameObject.SetActive(true);
        _canvasGroup.alpha = 0;
        _rectTransform.anchoredPosition = new Vector2(0, -100f);
        Sequence seq = DOTween.Sequence();
        seq.Append(_canvasGroup.DOFade(1f, 0.3f))
             .Join(_rectTransform.DOAnchorPosY(0, 0.3f).SetEase(Ease.OutBack))
             .AppendInterval(duration)
             .Append(_canvasGroup.DOFade(0f, 0.3f))
             .Join(_rectTransform.DOAnchorPosY(50, 0.3f).SetEase(Ease.InBack))
             .OnComplete(() => _dialog.gameObject.SetActive(false));
    }

}
