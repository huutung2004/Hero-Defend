using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HeroSaveSystem
{
    private const string SaveKey = "HeroSaveData";
    public static void Save(List<HeroData> inventory, List<HeroData> lineup)
    {
        //Luu vao wrapper
        HeroSaveWrapper wrapper = new HeroSaveWrapper();
        foreach (var hero in inventory)
        {
            wrapper.inventorySave.Add(new HeroSaveData { heroName = hero._name });
        }
        foreach (var hero in lineup)
        {
            wrapper.lineupSave.Add(new HeroSaveData { heroName = hero._name });
        }
        string json = JsonUtility.ToJson(wrapper);
        //Ghi vao player prefs
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
        Debug.Log("Hero Saved");
    }
    public static void Load(List<HeroData> inventory, List<HeroData> lineup, List<HeroData> allHero)
    {
        if (!PlayerPrefs.HasKey(SaveKey)) return;
        //lay ra json
        string json = PlayerPrefs.GetString(SaveKey);
        //chuyen json thanh wrapper
        HeroSaveWrapper wrapper = JsonUtility.FromJson<HeroSaveWrapper>(json);
        //lam sach du lieu
        inventory.Clear();
        lineup.Clear();
        //do du lieu vao 
        foreach (var heroLoad in wrapper.inventorySave)
        {
            HeroData hero = allHero.Find(h => h._name == heroLoad.heroName);
            if (hero != null) inventory.Add(hero);
        }
        foreach (var heroLoad in wrapper.lineupSave)
        {
            HeroData hero = allHero.Find(h => h._name == heroLoad.heroName);
            if (hero != null) lineup.Add(hero);
        }
        // HeroInventory.NotifyHeroChanged();
        // HeroLineup.NotifiHeroChanged();
        Debug.Log("Loaded Hero");
    }
}
