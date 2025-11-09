using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HeroLineup : MonoBehaviour
{
    public static HeroLineup Instance;
    public List<HeroData> listHeroLineup = new List<HeroData>();
    [SerializeField] private int _maxSlot = 5;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public bool AddHeroInLineup(HeroData heroData)
    {
        if (heroData != null)
        {
            if (listHeroLineup.Contains(heroData))
            {
                Debug.Log("Hero đã tồn tại trong đội hình");
                return false;
            }
            else
            {
                listHeroLineup.Add(heroData);
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
        }
    }
}
