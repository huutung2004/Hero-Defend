using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;
    [SerializeField] private List<HeroData> allHero;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        StartCoroutine(LoadWhenReady());
    }

    private IEnumerator LoadWhenReady()
    {
        yield return new WaitUntil(() =>
            HeroInventory.Instance != null &&
            HeroLineup.Instance != null &&
            allHero != null && allHero.Count > 0
        );

        Load();
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Save();
            Debug.Log("Game saved on pause");
        }
    }
    public void Save()
    {
        //SaveHero
        HeroSaveSystem.Save(HeroInventory.Instance.heroList, HeroLineup.Instance.listHeroLineup);
        MapUnlockSaveLoad.Save();

    }
    public void Load()
    {
        //LoadHero
        HeroSaveSystem.Load(HeroInventory.Instance.heroList, HeroLineup.Instance.listHeroLineup, allHero);
        MapUnlockSaveLoad.Load();

    }
}
