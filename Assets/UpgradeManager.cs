using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private Button _buttonUpgrade;
    [SerializeField] private Image _imagePreview;
    [SerializeField] private TMP_Text _rate;
    [Header("Cooldown")]
    [SerializeField] private float _cooldown = 2f;
    private bool _canUpgrade = true;
    private float _lastUpgradeTime;
    [Header("Rate/Hero")]
    [SerializeField] private float _ratePerOnce = 0.2f;
    private int _diamondUpgrade;
    //event
    public static event Action<Action> OnUpgradeUI;
    public static event Action<string, Color> StartTextEffect;

    private void Start()
    {
        ResetUI();
        _buttonUpgrade.onClick.AddListener(() => StartUpgrade());
    }
    private void OnEnable()
    {
        HeroUpgrade.PreviewHero += InitPreview;
        HeroUpgrade.ResetPreview += ResetUI;
    }
    private void OnDisable()
    {
        HeroUpgrade.PreviewHero -= InitPreview;
        HeroUpgrade.ResetPreview -= ResetUI;
    }

    private void StartUpgrade()
    {
        List<HeroData> heroDatas = HeroUpgrade.Instance.listHeroUpgrade;
        if (heroDatas.Count == 0) return;
        if (heroDatas[0]._heroUpgrade == null) return;

        if (!_canUpgrade)
        {
            float _remaining = _cooldown - (Time.time - _lastUpgradeTime);
            DialogManager.Instance.ShowDialog("Please Wait!", 2f);
            return;
        }
        _imagePreview.enabled = false;
        //kiểm tra cooldown và bắn sự kiện
        _canUpgrade = false;
        _lastUpgradeTime = Time.time;
        Invoke(nameof(ResetCooldown), _cooldown);
        float roll = UnityEngine.Random.value * 100f;
        float rate = heroDatas.Count * _ratePerOnce * heroDatas[0]._upgradeRate;
        if (roll <= rate)
        {
            CurrencyManager.Instance.ChangeCurrentDiamond(-_diamondUpgrade);
            HeroInventory.Instance.AddHero(heroDatas[0]._heroUpgrade);
            RefreshUI(heroDatas[0]._heroUpgrade);
            heroDatas.Clear();
            HeroUpgrade.NotifiHeroChanged();
            Debug.Log("nâng cấp thành công");
        }
        else
        {
            CurrencyManager.Instance.ChangeCurrentDiamond(-_diamondUpgrade);
            RefreshUI(null);
            heroDatas.Clear();
            HeroUpgrade.NotifiHeroChanged();
            Debug.Log("nâng cấp thất bại");
        }
    }
    private void InitPreview(HeroData heroData)
    {
        if (heroData._heroUpgrade == null) return;
        List<HeroData> heroDatas = HeroUpgrade.Instance.listHeroUpgrade;
        if (heroDatas.Count == 0) return;
        _imagePreview.sprite = heroData._heroUpgrade._previewImage;
        _imagePreview.enabled = true;
        _diamondUpgrade = heroData._diamondUpgrade;
        _rate.text = $"{heroDatas.Count * _ratePerOnce * heroDatas[0]._upgradeRate}%";
        _buttonUpgrade.GetComponentInChildren<TMP_Text>().text = _diamondUpgrade.ToString();
    }
    private void ResetCooldown()
    {
        _canUpgrade = true;
    }
    private void RefreshUI(HeroData heroData)
    {
        OnUpgradeUI.Invoke(() =>
            {
                if (heroData != null)
                {
                    _imagePreview.sprite = heroData._previewImage;
                    _imagePreview.enabled = true;
                    TextUpgradeEffect.Instance.StartTextEffect("Complete!",Color.red);
                }
                else
                {
                    _imagePreview.enabled = false;
                    TextUpgradeEffect.Instance.StartTextEffect("Fail!",Color.black);
                }
            });
    }
    public void ResetUI()
    {
        _imagePreview.sprite = null;
        _imagePreview.enabled = false;
        _rate.text = "";

    }

}
