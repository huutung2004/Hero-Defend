using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroSaveData
{
    public string heroName;
}
[System.Serializable]
public class HeroSaveWrapper
{
    public List<HeroSaveData> inventorySave = new List<HeroSaveData>();
    public List<HeroSaveData> lineupSave = new List<HeroSaveData>();

}
[System.Serializable]
public class MapUnLock
{
    public int _totalMap;
}
