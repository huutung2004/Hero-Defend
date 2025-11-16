using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapDropHandler : MonoBehaviour
{
    [SerializeField] private Tilemap _placeHeroTilemap;
    public static TilemapDropHandler Instance;
    private Camera _mainCamera;

    [Header("Debug Settings")]
    public float checkRadius = 0.1f;
    public Vector3Int debugCell;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (_placeHeroTilemap == null)
        {
            foreach (var tm in FindObjectsOfType<Tilemap>())
            {
                if (tm.name.ToLower().Contains("placehero"))
                {
                    _placeHeroTilemap = tm;
                    break;
                }
            }
        }

        if (_mainCamera == null) _mainCamera = Camera.main;
    }

    public bool TrySpawnHero(Vector3 screenPos, HeroData heroData)
    {
        if (_mainCamera == null) return false;

        Vector3 worldPos = _mainCamera.ScreenToWorldPoint(screenPos);
        worldPos.z = 0;

        Vector3Int cellPos = _placeHeroTilemap.WorldToCell(worldPos);
        Vector3 spawnPos = _placeHeroTilemap.GetCellCenterWorld(cellPos);

        if (!_placeHeroTilemap.HasTile(cellPos))
            return false;

        int heroLayer = LayerMask.NameToLayer("Hero");
        Collider2D hit = Physics2D.OverlapCircle(spawnPos, checkRadius, 1 << heroLayer);

        if (hit != null)
        {
            Debug.Log("Tile này đã có hero!");
            return false;
        }
        if (GoldManager.Instance.GetTotalGold() < heroData._goldToSpawn)
        {
            DialogManager.Instance.ShowDialog($"Not Enought Gold - {heroData._goldToSpawn}");
            Debug.Log("Not enought gold");
            return false;
        }
        else
        {
            GoldManager.Instance.ChangeTotalGold(-heroData._goldToSpawn);
            GameObject hero = Instantiate(heroData._heroPrefab, spawnPos, Quaternion.identity);
            hero.layer = heroLayer;
            return true;
        }
    }

    //gizmos
    private void OnDrawGizmosSelected()
    {
        if (_placeHeroTilemap == null) return;

        Vector3 spawnPos = _placeHeroTilemap.GetCellCenterWorld(debugCell);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawnPos, checkRadius);
    }
}
