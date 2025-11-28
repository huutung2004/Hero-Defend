using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SellHeroInWave : MonoBehaviour
{
    [SerializeField] private Button _buttonSell;
    [SerializeField] private TMP_Text _text;
    private HeroData _hero;

    // Start is called before the first frame update
    private void Awake()
    {
        HeroSelectionManager.OnSelectHero += UpdateUI;

    }
    private void OnDestroy()
    {
        HeroSelectionManager.OnSelectHero -= UpdateUI;
    }
    private void Start()
    {
        _buttonSell.onClick.AddListener(() => TrySellHero());
    }
    public void TrySellHero()
    {
        if (_hero != null)
        {
            HeroLineup.Instance.SellHeroFromWave(_hero);
            Destroy(gameObject);
        }

        else Debug.Log("Missing herodata");
    }
    public void UpdateUI(HeroData hero)
    {
        _hero = hero;
        _text.text = $"Sell {hero._goldToSpawn / 2}";
        _buttonSell.interactable = _hero != null;

    }

}
