using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroLineupUI : MonoBehaviour
{
    private HeroData _heroData;
    [SerializeField] private List<LineupSlotUI> lineupSlotUIs = new List<LineupSlotUI>();
    private void Awake()
    {
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
        Debug.Log($"Initialized {lineupSlotUIs.Count} heroLineup slots");
    }
    private void RefreshUI()
    {
        if (HeroLineup.Instance == null) return;
        List<HeroData> listHeroLineup = HeroLineup.Instance.listHeroLineup;
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
