using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroInventoryUI : MonoBehaviour
{
    public static HeroInventoryUI Instance;
    [SerializeField] private Transform contentParent;
    [SerializeField] private List<HeroSlotUI> _heroSlots = new List<HeroSlotUI>();
    [Header("Hero Detail UI")]
    [SerializeField] private TMP_Text _heroNameText;
    [SerializeField] private TMP_Text _rarityText;
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private TMP_Text _rangeText;
    private bool _isInitialized = false;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        InitializeSlots();
    }
    void Start()
    {
        RefreshDetail();
    }
    private void InitializeSlots()
    {
        _heroSlots.Clear();
        foreach (Transform row in contentParent)
        {
            foreach (Transform heroContain in row)
            {
                foreach (Transform slot in heroContain)
                {
                    HeroSlotUI slotUI = slot.GetComponent<HeroSlotUI>();
                    if (slotUI != null)
                    {
                        _heroSlots.Add(slotUI);
                    }

                }
            }
        }
        _isInitialized = true;
        Debug.Log($"Initialized {_heroSlots.Count} hero slots");
    }

    private void OnEnable()
    {
        HeroInventory.InventoryHeroChanged += Refresh;
        HeroSlotUI.ShowInformationHero += ShowHeroDetail;
    }
    private void OnDisable()
    {
        HeroInventory.InventoryHeroChanged -= Refresh;
        HeroSlotUI.ShowInformationHero -= ShowHeroDetail;

    }
    public void Refresh()
    {
        if (!_isInitialized || HeroInventory.Instance == null) return;

        List<HeroData> heroes = HeroInventory.Instance.heroList;
        Debug.Log($"Refreshing UI with {heroes.Count} heroes");
        for (int i = 0; i < _heroSlots.Count; i++)
        {
            if (i < heroes.Count)
            {
                _heroSlots[i].SetHero(heroes[i]);
            }
            else _heroSlots[i].SetHero(null);
        }
    }
    public void ShowHeroDetail(HeroData hero)
    {
        if (hero == null) return;
        _heroNameText.text = $"HeroName - {hero._name}";
        _rarityText.text = $"Rarity - {hero._rarity}";
        _damageText.text = $"Damage - {hero._damage}";
        _rangeText.text = $"Range - {hero._rangeAttack}";
    }
    public void RefreshDetail()
    {
        _heroNameText.text = "HeroName - ";
        _rarityText.text = "Rarity - ";
        _damageText.text = "Damage - ";
        _rangeText.text = "Range - ";
    }
}