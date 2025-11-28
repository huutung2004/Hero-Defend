
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeroSummonUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image _previewHero;
    [SerializeField] private TMP_Text _rarityText;
    [SerializeField] private TMP_Text _heroName;
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private TMP_Text _rangeText;
    [SerializeField] private Image _imageEffect;
    [SerializeField] private Color _commonColor;
    [SerializeField] private Color _rareColor;
    [SerializeField] private Color _legendColor;
    [SerializeField] private Color _mysthicColor;


    //event
    public static event Action<Action> OnSumonUI;
    private void Awake()
    {
        _imageEffect.gameObject.SetActive(false);
        ResetUI();
    }
    private void OnEnable()
    {
        HeroSummonManager.OnHeroSummoned += UpdateUI;
        HeroSummonManager.HeroInventoryFull += ShowFullInventory;
        HeroSummonManager.OnCoolDown += ShowDialogCoolDown;
        HeroSummonManager.NotEnoughtDiamond += ShowDialogNotEnought;
    }
    private void OnDisable()
    {
        HeroSummonManager.OnHeroSummoned -= UpdateUI;
        HeroSummonManager.HeroInventoryFull -= ShowFullInventory;
        HeroSummonManager.OnCoolDown -= ShowDialogCoolDown;
        HeroSummonManager.NotEnoughtDiamond -= ShowDialogNotEnought;


    }
    private void UpdateUI(HeroData hero)
    {
        if (hero == null) return;
        ResetUI();
        OnSumonUI.Invoke(() =>
   {
       switch (hero._rarity)
       {
           case HeroRarity.Common:
               _imageEffect.color = _commonColor;
               _imageEffect.gameObject.SetActive(true);

               break;
           case HeroRarity.Rare:
               _imageEffect.color = _rareColor;
               _imageEffect.gameObject.SetActive(true);
               break;
           case HeroRarity.Legendary:
               _imageEffect.color = _legendColor;
               _imageEffect.gameObject.SetActive(true);
               break;
           case HeroRarity.Mysthic:
               _imageEffect.color = _mysthicColor;
               _imageEffect.gameObject.SetActive(true);
               break;
       }
       _previewHero.sprite = hero._previewImage;
       _previewHero.enabled = true;
       _heroName.text = $"HeroName - {hero._name}";
       _rarityText.text = $"Rarity - {hero._rarity}";
       _damageText.text = $"Damage - {hero._damage}";
       _rangeText.text = $"Range - {hero._rangeAttack}";

       TextEffectSumon.Instance.StartTextEffect();
       StartCoroutine(waitEffect(2f));

   });
    }
    private void ResetUI()
    {
        _previewHero.sprite = null;
        _previewHero.enabled = false;
        _heroName.text = "HeroName - ";
        _rarityText.text = "Rarity - ";
        _damageText.text = "Damage - ";
        _rangeText.text = "Range - ";
        _imageEffect.gameObject.SetActive(false);

    }
    private void ShowFullInventory()
    {
        DialogManager.Instance.ShowDialog("Inventory Is Full", 2f);
    }
    private void ShowDialogCoolDown()
    {
        DialogManager.Instance.ShowDialog("Please Wait!", 2f);
    }
    private void ShowDialogNotEnought()
    {
        DialogManager.Instance.ShowDialog("Not Enought Dimond (10 Diamomd)!", 2f);
    }
    private IEnumerator waitEffect(float t)
    {
        yield return new WaitForSeconds(t);
        _imageEffect.gameObject.SetActive(false);
    }
}
