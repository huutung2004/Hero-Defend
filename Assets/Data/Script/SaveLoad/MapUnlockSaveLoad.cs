using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapUnlockSaveLoad
{
    private const string SaveKey = "MapUnLock";
    public static void Save()
    {
        int _totalMapUnLock = MapSelectionManager.Instance.GetTotalUnlock();
        PlayerPrefs.SetInt(SaveKey, _totalMapUnLock);
    }
    public static void Load()
    {
        if (!PlayerPrefs.HasKey(SaveKey)) return;
        MapSelectionManager.Instance.SetTotalUnlock(PlayerPrefs.GetInt(SaveKey));

    }
}
