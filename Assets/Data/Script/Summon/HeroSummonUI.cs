
using TMPro;
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
    private void Awake()
    {
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
        CircleRotateUI.Instance.StartEffect(() =>
   {
       _previewHero.sprite = hero._previewImage;
       _previewHero.enabled = true;
       _heroName.text = $"HeroName - {hero._name}";
       _rarityText.text = $"Rarity - {hero._rarity}";
       _damageText.text = $"Damage - {hero._damage}";
       _rangeText.text = $"Range - {hero._rangeAttack}";

       TextEffectSumon.Instance.StartTextEffect();
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
}
