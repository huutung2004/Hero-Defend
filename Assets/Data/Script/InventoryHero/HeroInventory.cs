using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInventory : MonoBehaviour
{
    public static HeroInventory Instance;
    public List<HeroData> heroList = new List<HeroData>();
    [SerializeField] private int _maxList = 32;
    public static event Action InventoryHeroChanged;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void AddHero(HeroData newHero)
    {
        if (heroList.Count < _maxList)
        {
            heroList.Add(newHero);
            InventoryHeroChanged?.Invoke();
        }
    }
    public void SellHero(HeroData sellHero)
    {
        heroList.Remove(sellHero);
        InventoryHeroChanged?.Invoke();

    }
    public bool IsFull()
    {
        if (heroList.Count >= _maxList) return true;
        else return false;
    }
}
