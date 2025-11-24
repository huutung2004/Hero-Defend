using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    private bool isSelected = false;

    private void Awake()
    {
        _canvas.SetActive(false);
    }
    public HeroData SelectHero(bool value)
    {
        isSelected = value;
        MusicManager.Instance.PlayMusic("tap");

        // hiệu ứng chọn hero
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = value ? new Color(1f, 1f, 1f, 0.5f) : Color.white;
        if (value)
        {
            HeroData heroData = gameObject.GetComponent<PlayerAttackController>().GetHeroData();
            _canvas.SetActive(true);
            return heroData;
        }
        else _canvas.SetActive(false);
        return null;
    }

    public void TeleportTo(Vector3 pos)
    {
        transform.position = pos;
    }
}
