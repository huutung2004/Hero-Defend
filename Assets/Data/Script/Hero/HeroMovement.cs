using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    private bool isSelected = false;

    public void SelectHero(bool value)
    {
        isSelected = value;

        // hiệu ứng chọn hero
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = value ? new Color(1f,1f,1f,0.5f) : Color.white;
    }

    public void TeleportTo(Vector3 pos)
    {
        transform.position = pos;
    }
}
