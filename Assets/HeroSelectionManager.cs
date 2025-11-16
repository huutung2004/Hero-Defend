using UnityEngine;
using UnityEngine.Tilemaps;

public class HeroSelectionManager : MonoBehaviour
{
    public static HeroSelectionManager Instance;

    [SerializeField] private Tilemap placeHeroTilemap;

    private Camera mainCam;
    private HeroMovement selectedHero;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        mainCam = Camera.main;

        if (placeHeroTilemap == null)
        {
            foreach (var tm in FindObjectsOfType<Tilemap>())
            {
                if (tm.name.ToLower().Contains("placehero"))
                {
                    placeHeroTilemap = tm;
                    break;
                }
            }
        }
    }

    void Update()
    {
        if (!GetTapPosition(out Vector3 tapPos))
            return;

        Vector3 world = mainCam.ScreenToWorldPoint(tapPos);
        world.z = 0;

        Vector3Int cell = placeHeroTilemap.WorldToCell(world);

        if (!placeHeroTilemap.HasTile(cell))
            return;

        Vector3 cellCenter = placeHeroTilemap.GetCellCenterWorld(cell);

        //Select
        if (selectedHero == null)
        {
            Collider2D hit = Physics2D.OverlapCircle(cellCenter, 0.3f, LayerMask.GetMask("Hero"));
            if (hit != null)
            {
                selectedHero = hit.GetComponent<HeroMovement>();
                selectedHero.SelectHero(true);
            }
            return;
        }
        //kiem tra đã có hero chưa
        Collider2D heroAtTarget = Physics2D.OverlapCircle(cellCenter, 0.1f, LayerMask.GetMask("Hero"));
        if (heroAtTarget != null)
        {
            Debug.Log("Ô đã có hero!");
            return;
        }
        //Di chuyển hero
        selectedHero.TeleportTo(cellCenter);
        selectedHero.SelectHero(false);
        selectedHero = null;
    }


    // ----------- INPUT PC + ANDROID -----------
    private bool GetTapPosition(out Vector3 tapPos)
    {
        tapPos = Vector3.zero;

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            tapPos = Input.mousePosition;
            return true;
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                tapPos = t.position;
                return true;
            }
        }
#endif
        return false;
    }
}
