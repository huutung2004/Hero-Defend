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
    private GameObject _cursorClone;

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

        if (_cursorCustom != null)
        {
            _cursorClone =  Instantiate(_cursorCustom);
            _cursorClone.SetActive(false);
        }
        else Debug.LogError("Cursor Custom is null");
    }

    // Update is called once per frame
    void Update()
    {
        if (_mainCamera == null || _placeHeroTilemap == null || _cursorClone == null)
            return;

        Vector3 touchPos;

#if UNITY_EDITOR || UNITY_STANDALONE
        // --- CHẾ ĐỘ CHUỘT ---
        if (Input.mousePosition.x < 0 || Input.mousePosition.y < 0 ||
            Input.mousePosition.x > Screen.width || Input.mousePosition.y > Screen.height)
            return;

        touchPos = Input.mousePosition;

#elif UNITY_ANDROID || UNITY_IOS
    // --- CHẾ ĐỘ CẢM ỨNG ---
    if (Input.touchCount == 0)
    {
        _cursorClone.SetActive(false);
        return;
    }
    touchPos = Input.GetTouch(0).position; // lấy vị trí ngón chạm đầu tiên
#endif

        touchPos.z = -_mainCamera.transform.position.z;
        Vector3 worldPos = _mainCamera.ScreenToWorldPoint(touchPos);
        Vector3Int cellPos = _placeHeroTilemap.WorldToCell(worldPos);

        if (_placeHeroTilemap.HasTile(cellPos))
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            Cursor.visible = false;
#endif
            _cursorClone.SetActive(true);
            _cursorClone.transform.position = _placeHeroTilemap.GetCellCenterWorld(cellPos);
        }
        else
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            Cursor.visible = true;
#endif
            _cursorClone.SetActive(false);
        }
    }

}
