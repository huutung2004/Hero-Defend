using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SellHeroButton : MonoBehaviour
{

    [SerializeField] private Image _notifiSellHero;
    private Button _buttonSell;

    void Start()
    {
        if (_notifiSellHero == null) return;
        _buttonSell = GetComponent<Button>();
        if (_buttonSell == null)
        {
            Debug.Log("Not found button sell");
            return;
        }
        _buttonSell.onClick.AddListener(() => OnClickSell());


    }
    private void OnClickSell()
    {
        _notifiSellHero.gameObject.SetActive(true);
    }

}
