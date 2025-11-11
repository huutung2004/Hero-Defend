using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmSellHero : MonoBehaviour
{
    [SerializeField] private Button _buttonSell;
    [SerializeField] private Button _buutonCancel;
    private HeroData _heroSell;

    void Awake()
    {
        gameObject.SetActive(false);
        HeroSlotUI.SignSellHero += HeroSell;

    }
    void Start()
    {
        if (_buttonSell == null || _buttonSell == null) return;
        _buttonSell.onClick.AddListener(() => RequestSell());
        _buutonCancel.onClick.AddListener(() => CancelSellHero());
    }

    void OnDestroy()
    {
        HeroSlotUI.SignSellHero -= HeroSell;
    }
    private void RequestSell()
    {
        if (_heroSell != null)
        {
            HeroInventory.Instance.SellHero(_heroSell);
            CancelSellHero();
        }
    }
    private void HeroSell(HeroData _hero)
    {

        if (_hero != null)
        {
            _heroSell = _hero;
            Debug.Log("đã nhận herota từ event");
        }
        else
        {
            Debug.Log("Hero data null");
        }
    }
    private void CancelSellHero()
    {
        gameObject.SetActive(false);
    }
}
