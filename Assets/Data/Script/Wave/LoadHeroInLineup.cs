using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadHeroInLineup : MonoBehaviour
{
    private List<HeroData> _heroInLineup;
    [SerializeField] private GameObject _HeroPrefab;
    [SerializeField] private Transform _HeroContain;
    private void Start()
    {
        if (_HeroPrefab == null)
        {
            Debug.LogWarning("HeroPrefab is missing");
            return;
        }
        if (_HeroContain == null)
        {
            Debug.LogWarning("Hero contain is missing");
            return;
        }
        LoadHero();
    }
    private void LoadHero()
    {
        _heroInLineup = HeroLineup.Instance.listHeroLineup;
        if(_heroInLineup == null)
        {
            Debug.Log("hero in lineup is null");
            return;
        }
        for (int i = 0; i < _heroInLineup.Count; i++)
        {
            var hero = Instantiate(_HeroPrefab, _HeroContain);
            var _heroImage = hero.GetComponentsInChildren<Image>();
            _heroImage[1].sprite = _heroInLineup[i]._previewImage;
            _heroImage[1].gameObject.GetComponent<HeroLineupSlotUI>().SetHero(_heroInLineup[i]);
        }
    }
}
