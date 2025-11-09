using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class HeroSlotUI : MonoBehaviour
{
    private HeroData _heroData;
    private Image _imageHero;
    private Button _buttonInParent;
    public static event Action<HeroData> ShowInformationHero;
    void Start()
    {
        _imageHero = GetComponent<Image>();
        _buttonInParent = GetComponentInParent<Button>();
        if(_buttonInParent!= null)
        {
            _buttonInParent.onClick.AddListener(() => ButtonHeroClick());
        }
        SetHero(null);
    }
    public void SetHero(HeroData _data)
    {
        _heroData = _data;
        if (_data != null)
        {
            _imageHero.sprite = _heroData._previewImage;
            _imageHero.enabled = true;
        }
        else
        {
            _imageHero.sprite = null;
            _imageHero.enabled = false;
        }
    }
    private void ButtonHeroClick()
    {
        ShowInformation();
    }
    private void ShowInformation()
    {
        ShowInformationHero?.Invoke(_heroData);
    }
    
}
