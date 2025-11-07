
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
    private void OnEnable()
    {
        HeroSummonManager.OnHeroSummoned += UpdateUI;
    }
    private void OnDisable()
    {
        HeroSummonManager.OnHeroSummoned -= UpdateUI;
    }
    private void UpdateUI(HeroData hero)
    {
        if (hero == null) return;
        _previewHero.sprite = hero._previewImage;
        _heroName.text = $"HeroName - {hero._name}";
        _rarityText.text = $"Rarity - {hero._rarity}";
        _damageText.text = $"Damage - {hero._damage}";
        _rangeText.text = $"Range - {hero._rangeAttack}";
    }
}
