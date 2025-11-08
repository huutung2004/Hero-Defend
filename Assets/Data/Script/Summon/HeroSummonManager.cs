using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroSummonManager : MonoBehaviour
{
    [Header("List Hero")]
    [SerializeField] private List<HeroData> _allHeroes;
    private Dictionary<HeroRarity, List<HeroData>> _heroByRarity;
    [Header("Cooldown")]
    [SerializeField] private float _cooldown = 2f;
    private bool _canSumon = true;
    private float _lastSummonTime;
    //envent
    public static event Action<HeroData> OnHeroSummoned;
    public static event Action HeroInventoryFull;
    void Awake()
    {
        _heroByRarity = new Dictionary<HeroRarity, List<HeroData>>();
        foreach (HeroRarity rarity in Enum.GetValues(typeof(HeroRarity)))
        {
            _heroByRarity[rarity] = new List<HeroData>();
        }
        foreach (var hero in _allHeroes)
        {
            _heroByRarity[hero._rarity].Add(hero);
        }
    }
    public HeroData Summon()
    {
        if (!_canSumon)
        {
            float _remaining = _cooldown - (Time.time - _lastSummonTime);
            return null;
        }
        if (HeroInventory.Instance.IsFull())
        {
            HeroInventoryFull?.Invoke();
            return null;
        }
        _canSumon = false;
        _lastSummonTime = Time.time;
        Invoke(nameof(ResetCooldown), _cooldown);
        float roll = UnityEngine.Random.value * 100f;
        HeroRarity rarity;
        if (roll < 70f) rarity = HeroRarity.Common;
        else if (roll < 90f) rarity = HeroRarity.Rare;
        else if (roll < 97f) rarity = HeroRarity.Legendary;
        else rarity = HeroRarity.Mysthic;
        var list = _heroByRarity[rarity];
        if (list.Count == 0)
        {
            Debug.Log($"Not found Hero: {rarity}");
            return null;
        }
        HeroData summonedHero = list[UnityEngine.Random.Range(0, list.Count)];
        Debug.Log(roll);
        Debug.Log($"Summoned: {summonedHero._name} - {summonedHero._rarity}");
        OnHeroSummoned?.Invoke(summonedHero);
        HeroInventory.Instance.AddHero(summonedHero);
        return summonedHero;

    }
     private void ResetCooldown()
    {
        _canSumon = true;
    }
    public void OnClickSummon() => Summon();
}
