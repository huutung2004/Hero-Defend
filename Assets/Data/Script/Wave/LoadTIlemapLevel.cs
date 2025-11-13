using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTIlemapLevel : MonoBehaviour
{
    public static LoadTIlemapLevel Instance { get; set; }
    private string _level;
    [SerializeField] private List<GameObject> listTilemapPrefabs;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        _level = MapSelectionManager.Instance.GetLevel();
        if (_level == null)
        {
            Debug.Log("level map is missing (Invoke is missing)");
            return;
        }
        if (listTilemapPrefabs.Count < 1)
        {
            Debug.Log("Tilemap is empty");
            return;
        }
        LoadTilemap();
    }
    private void LoadTilemap()
    {
        int levelmap = int.Parse(_level);
        Instantiate(listTilemapPrefabs[levelmap-1]);

    }
}
