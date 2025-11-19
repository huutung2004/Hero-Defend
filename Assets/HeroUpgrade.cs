using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroUpgrade : MonoBehaviour
{
    public static HeroUpgrade Instance;
    public List<HeroData> listHeroUpgrade = new List<HeroData>();
    [SerializeField] private int _maxSlot = 5;
    //event
    public static event Action OnHeroUpgradeChanged;
    public static event Action<HeroData> PreviewHero;
    public static event Action ResetPreview;
    private void OnApplicationPause()
    {
        ReturnToInventory();
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        HeroInventory.InventoryHeroRemoved += RemoveHeroFromUpgrade;
    }
    private void OnDisable()
    {
        HeroInventory.InventoryHeroRemoved -= RemoveHeroFromUpgrade;
    }
    public bool AddHeroInUpgrade(HeroData heroData)
    {
        if (heroData != null && listHeroUpgrade.Count == 0)
        {

            listHeroUpgrade.Add(heroData);
            OnHeroUpgradeChanged?.Invoke();
            PreviewHero?.Invoke(heroData);
            return true;
        }
        //Nếu có hero đầu tiên rồi, kiểm tra các hero có giống nhau không
        if (listHeroUpgrade.Count < _maxSlot && CheckHeroSame(heroData))
        {
            listHeroUpgrade.Add(heroData);
            OnHeroUpgradeChanged?.Invoke();
            PreviewHero?.Invoke(heroData);
            return true;
        }
        return false;

    }
    public void RemoveHeroFromUpgrade(HeroData heroData)
    {
        if (heroData != null)
        {
            listHeroUpgrade.Remove(heroData);
            OnHeroUpgradeChanged?.Invoke();
            PreviewHero?.Invoke(heroData);
        }
        if(listHeroUpgrade.Count == 0)
        {
            ResetPreview?.Invoke();
        }
    }
    public void ReturnToInventory()
    {
        if (listHeroUpgrade.Count == 0) return;
        List<HeroData> tempList = new List<HeroData>(listHeroUpgrade);
        foreach (HeroData hero in tempList)
        {
            RemoveHeroFromUpgrade(hero);
            HeroInventory.Instance.AddHero(hero);
        }
    }
    public bool CheckHeroSame(HeroData hero)
    {
        string name = listHeroUpgrade[0]._name;
        if (!hero._name.Equals(name))
        {
            DialogManager.Instance.ShowDialog("All hero must be the same", 2f);
            return false;
        }
        return true;
    }
    

    public static void NotifiHeroChanged()
    {
        OnHeroUpgradeChanged?.Invoke();
    }
}
