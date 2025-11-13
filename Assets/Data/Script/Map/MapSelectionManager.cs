using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectionManager : MonoBehaviour
{
    public static MapSelectionManager Instance { get; private set; }

    private string _level;
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
}
