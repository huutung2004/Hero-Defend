using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroInventoryUI : MonoBehaviour
{
    [SerializeField] private Transform contentParent;
    [SerializeField] private List<Image> _heroSlots = new List<Image>();
    void Awake()
    {
        _heroSlots.Clear();
        foreach (Transform row in contentParent)
        {
            foreach (Transform heroContain in row)
            {
                foreach (Transform slot in heroContain)
                {
                    Image img = slot.GetComponent<Image>();
                    if (img != null)
                    {
                        _heroSlots.Add(img);
                    }
                }
            }
        }
    }
    void Start()
    {
        Refresh();
    }
    private void OnEnable()
    {
        HeroInventory.InventoryHeroChanged += Refresh;
    }
    private void OnDisable()
    {
        HeroInventory.InventoryHeroChanged -= Refresh;

    }
    public void Refresh()
    {
        List<HeroData> heroes = HeroInventory.Instance.heroList;

        for (int i = 0; i < _heroSlots.Count; i++)
        {
            if (i < heroes.Count)
            {
                _heroSlots[i].sprite = heroes[i]._previewImage;
                _heroSlots[i].enabled = true;
            }
            else
            {
                _heroSlots[i].sprite = null;
                _heroSlots[i].enabled = false;
            }
        }
    }

}
