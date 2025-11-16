using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectionManager : MonoBehaviour
{
    public static MapSelectionManager Instance { get; private set; }
    [SerializeField] private int _totalMapUnlock = 1;

    private string _level;
    //event 
    public static event Action OnTotalUnLockChanged;
    private void OnEnable()
    {
        SelectMap.OnMapSelected += GetLevelMap;
    }
    private void OnDisable()
    {
        SelectMap.OnMapSelected -= GetLevelMap;
    }
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
    public void GetLevelMap(string level)
    {
        _level = level;
        Debug.Log($"Level: {_level}");
        SceneManager.LoadScene("WaveScene");
    }
    public string GetLevel()
    {
        return _level;
    }
    public int GetTotalUnlock()
    {
        return _totalMapUnlock;
    }
    public void UnlockNewMap()
    {
        int _levelInt = int.Parse(_level);
        Debug.Log($"{_levelInt} - {_totalMapUnlock}");
        if (_levelInt == _totalMapUnlock)
            _totalMapUnlock++;
    }
    public void SetTotalUnlock(int total)
    {
        _totalMapUnlock = total;
        OnTotalUnLockChanged?.Invoke();

    }
}
