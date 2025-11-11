using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HeroLineup : MonoBehaviour
{
    public static HeroLineup Instance;
    public List<HeroData> listHeroLineup = new List<HeroData>();
    [SerializeField] private int _maxSlot = 5;
    //event
    public static event Action OnHeroLineupChanged;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void OnEnable()
    {
        HeroInventory.InventoryHeroRemoved += RemoveHeroFromLineup;
    }
    private void OnDisable()
    {
        HeroInventory.InventoryHeroRemoved -= RemoveHeroFromLineup;

    }
    public bool AddHeroInLineup(HeroData heroData)
    {
        if (heroData != null || listHeroLineup.Count > _maxSlot)
        {
            if (listHeroLineup.Contains(heroData))
            {
                Debug.Log("Hero đã tồn tại trong đội hình");
                return false;
            }
            else
            {
                listHeroLineup.Add(heroData);
                OnHeroLineupChanged?.Invoke();
                return true;
            }
        }
        return false;

    }
    public void RemoveHeroFromLineup(HeroData heroData)
    {
        if (heroData != null)
        {
            listHeroLineup.Remove(heroData);
            OnHeroLineupChanged?.Invoke();
        }
    }
    public static void NotifiHeroChanged()
    {
        OnHeroLineupChanged?.Invoke();
    }
}
