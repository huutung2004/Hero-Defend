using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private List<HeroData> allHero;
    void Awake()
    {
        if (allHero == null)
        {
            Debug.Log("allHero is missing ");
            return;
        }
        Load();
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    private void Save()
    {
        //SaveHero
        HeroSaveSystem.Save(HeroInventory.Instance.heroList, HeroLineup.Instance.listHeroLineup);

    }
    private void Load()
    {
        //LoadHero
        HeroSaveSystem.Load(HeroInventory.Instance.heroList, HeroLineup.Instance.listHeroLineup, allHero);
    }
}
