using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroLineupUI : MonoBehaviour
{
    public static HeroLineupUI Instance;
    private HeroData _heroData;
    [SerializeField] private List<LineupSlotUI> lineupSlotUIs = new List<LineupSlotUI>();
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
        HeroLineup.OnHeroLineupChanged += RefreshUI;
    }
    private void OnDisable()
    {
        HeroLineup.OnHeroLineupChanged -= RefreshUI;
    }

    private void InitializeSlots()
    {
        lineupSlotUIs.Clear();
        foreach (Transform heroContain in transform)
        {
            foreach (Transform slot in heroContain)
            {
                if (slot != null)
                {
                    LineupSlotUI lineupSlotUI = slot.GetComponent<LineupSlotUI>();
                    lineupSlotUIs.Add(lineupSlotUI);
                }
            }
        }
        _isInitialized = true;

        Debug.Log($"Initialized {lineupSlotUIs.Count} heroLineup slots");
    }
    public void RefreshUI()
    {
        if (!_isInitialized && HeroLineup.Instance == null) return;
        List<HeroData> listHeroLineup = HeroLineup.Instance.listHeroLineup;
        Debug.Log($"Refreshing {listHeroLineup.Count} herolineup");
        for (int i = 0; i < lineupSlotUIs.Count; i++)
        {
            if (i < listHeroLineup.Count)
            {
                lineupSlotUIs[i].SetHero(listHeroLineup[i]);
            }
            else lineupSlotUIs[i].SetHero(null);
        }
    }
}
