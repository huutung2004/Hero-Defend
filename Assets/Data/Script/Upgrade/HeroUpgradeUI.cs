using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroUpgradeUI : MonoBehaviour
{
    public static HeroUpgradeUI Instance;
    private HeroData _heroData;
    [SerializeField] private List<UpgradeSlotUI> upgradeSlotUIs = new List<UpgradeSlotUI>();
    private bool _isInitialized = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        InitializeSlots();
    }
    private void OnEnable()
    {
        HeroUpgrade.OnHeroUpgradeChanged += RefreshUI;
    }
    private void OnDisable()
    {
        HeroUpgrade.OnHeroUpgradeChanged -= RefreshUI;
    }

    private void InitializeSlots()
    {
        upgradeSlotUIs.Clear();
        foreach (Transform heroContain in transform)
        {
            foreach (Transform slot in heroContain)
            {
                if (slot != null)
                {
                    UpgradeSlotUI upgradeSlotUI = slot.GetComponent<UpgradeSlotUI>();
                    upgradeSlotUIs.Add(upgradeSlotUI);
                }
            }
        }
        _isInitialized = true;

        Debug.Log($"Initialized {upgradeSlotUIs.Count} HeroUpgrade slots");
    }
    public void RefreshUI()
    {
        if (!_isInitialized && HeroUpgrade.Instance == null) return;
        List<HeroData> listHeroUpgrade = HeroUpgrade.Instance.listHeroUpgrade;
        Debug.Log($"Refreshing {listHeroUpgrade.Count} heroUpgrade");
        for (int i = 0; i < upgradeSlotUIs.Count; i++)
        {
            if (i < listHeroUpgrade.Count)
            {
                upgradeSlotUIs[i].SetHero(listHeroUpgrade[i]);
            }
            else upgradeSlotUIs[i].SetHero(null);
        }
    }
}
