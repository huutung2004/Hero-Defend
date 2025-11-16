using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrencySaveLoad
{
    private const string SaveKey = "Currency";
    public static void Save()
    {
        int _diamond = CurrencyManager.Instance.GetCurrentDiamond();
        PlayerPrefs.SetInt(SaveKey, _diamond);
        PlayerPrefs.Save();

    }
    public static void Load()
    {
        if (!PlayerPrefs.HasKey(SaveKey)) return;
        CurrencyManager.Instance.SetCurrentDiamond(PlayerPrefs.GetInt(SaveKey));
    }
}
