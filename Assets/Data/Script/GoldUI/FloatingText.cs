using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float lifetime = 1f;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private float moveUp = 1.0f;

    private float timer;
    private Vector3 startPos;

    public void Play(Vector3 pos, string value)
    {
        startPos = pos;
        transform.position = pos;
        SetValue(value);
        gameObject.SetActive(true);
    }
    void OnEnable()
    {
        timer = 0;
    }

    public void SetValue(string value)
    {
        valueText.text = value;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Move Up
        transform.position = startPos + new Vector3(0, timer * moveUp, 0);

        // Fade Out
        float alpha = 1 - (timer / lifetime);
        valueText.alpha = alpha;

        if (timer >= lifetime)
        {
            TextEffectPool.Instance.ReturnGOldEffectToPool(this);
        }
    }
}
