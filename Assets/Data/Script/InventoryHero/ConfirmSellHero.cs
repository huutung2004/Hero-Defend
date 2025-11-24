using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmSellHero : MonoBehaviour
{
    [SerializeField] private Button _buttonSell;
    [SerializeField] private Button _buutonCancel;
    [SerializeField] private TMP_Text _priceSell;
    private HeroData _heroSell;

    void Awake()
    {
        HeroSlotUI.SignSellHero += GetHeroSell;
        gameObject.SetActive(false);

    }
    void OnEnable()
    {
        HeroSlotUI.SignSellHero += GetHeroSell;
    }
    void Start()
    {
        if (_buttonSell == null || _buttonSell == null) return;
        _buttonSell.onClick.AddListener(() => RequestSell());
        _buutonCancel.onClick.AddListener(() => CancelSellHero());
    }

    void OnDestroy()
    {
        HeroSlotUI.SignSellHero -= GetHeroSell;
    }
    private void RequestSell()
    {
        if (_heroSell != null)
        {
            HeroInventory.Instance.SellHero(_heroSell);
            CancelSellHero();
        }
    }
    private void GetHeroSell(HeroData _hero)
    {

        if (_hero != null)
        {
            _heroSell = _hero;
            _priceSell.text = $"{_hero._price} - D";
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
