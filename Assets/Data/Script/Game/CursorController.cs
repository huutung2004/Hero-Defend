using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CursorController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Tilemap _placeHeroTilemap;
    [SerializeField] private GameObject _cursorCustom;
    public Camera _mainCamera;

    // Start is called before the first frame update
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

        if (_cursorCustom != null) _cursorCustom.SetActive(false);
        else Debug.LogError("Cursor Custom is null");
    }

    // Update is called once per frame
    void Update()
    {
        if (_mainCamera == null || _placeHeroTilemap == null || _cursorCustom == null)
            return;
        if (Input.mousePosition.x < 0 || Input.mousePosition.y < 0 ||
                    Input.mousePosition.x > Screen.width || Input.mousePosition.y > Screen.height)
        {
            return;
        }
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = -_mainCamera.transform.position.z;
        Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(mouseScreenPos);
        Vector3Int cellPos = _placeHeroTilemap.WorldToCell(mouseWorldPos);
        if (_placeHeroTilemap.HasTile(cellPos))
        {
            Cursor.visible = false;
            _cursorCustom.SetActive(true);
            Vector3 snappedPos = _placeHeroTilemap.GetCellCenterWorld(cellPos);
            _cursorCustom.transform.position = snappedPos;
        }
        else _cursorCustom.SetActive(false);
        Cursor.visible = true;
    }
}
