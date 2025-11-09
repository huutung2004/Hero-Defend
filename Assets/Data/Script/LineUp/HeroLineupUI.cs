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
    private void Start()
    {

    }
    private void InitializeSlots()
    {
        lineupSlotUIs.Clear();
        foreach (Transform heroContain in transform)
        {
            foreach(Transform slot in heroContain)
            {
                if(slot!= null)
                {
                    LineupSlotUI lineupSlotUI = slot.GetComponent<LineupSlotUI>();
                    lineupSlotUIs.Add(lineupSlotUI);
                }
            }
        }
        Debug.Log($"Initialized {lineupSlotUIs.Count} heroLineup slots");
    }
}
