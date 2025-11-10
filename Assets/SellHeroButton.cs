using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellHeroButton : MonoBehaviour
{
    private Button _buttonSell;
    private HeroData _heroSell;

    void Start()
    {
        _buttonSell = GetComponent<Button>();
        _buttonSell.onClick.AddListener(() => RequestSell());
    }
    void OnEnable()
    {
        HeroSlotUI.SignSellHero += HeroSell;
    }
    void OnDisable()
    {
        HeroSlotUI.SignSellHero -= HeroSell;
    }
    private void RequestSell()
    {
        if (_heroSell != null)
        {
            HeroInventory.Instance.SellHero(_heroSell);
        }

    }
    private void HeroSell(HeroData _hero)
    {

        if (_hero != null)
        {
            _heroSell = _hero;
        }
    }
}
