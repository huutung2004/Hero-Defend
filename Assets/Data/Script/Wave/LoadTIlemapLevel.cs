using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTIlemapLevel : MonoBehaviour
{
    public static LoadTIlemapLevel Instance { get; set; }
    private string _level;
    [SerializeField] private List<GameObject> listTilemapPrefabs;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "WaveScene")
            return;

        _level = MapSelectionManager.Instance.GetLevel();
        if (string.IsNullOrEmpty(_level))
        {
            Debug.Log("Level map is missing");
            return;
        }

        LoadTilemap();
    }

    private void LoadTilemap()
    {
        int levelIndex = int.Parse(_level);
        if (levelIndex - 1 >= listTilemapPrefabs.Count)
        {
            Debug.LogError("Tilemap index out of range");
            return;
        }

        Instantiate(listTilemapPrefabs[levelIndex - 1]);
        Debug.Log("Đã load tilemap level " + levelIndex);
    }
}
