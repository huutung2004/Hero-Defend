using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIManager : MonoBehaviour
{
    [SerializeField] private Button _buttonUpgrade;
    [SerializeField] private Image _panelUpgrade;

    //event
    public static event Action OnOpenUpgrade;
    void Start()
    {
        if (_buttonUpgrade == null || _panelUpgrade == null)
        {
            Debug.LogWarning("Button party or panel is missing");
            return;
        }

        // Đảm bảo panel được kích hoạt tạm thời để khởi tạo UI components
        _panelUpgrade.gameObject.SetActive(true);

        // Chờ 1 frame để các component khởi tạo xong
        StartCoroutine(InitializeUI());
    }

    private IEnumerator InitializeUI()
    {
        // Chờ 1 frame để đảm bảo tất cả Awake() đã chạy
        yield return null;

        // Tắt panel sau khi khởi tạo xong
        _panelUpgrade.gameObject.SetActive(false);

        // Gán sự kiện click
        // _buttonUpgrade.onClick.RemoveAllListeners();
        _buttonUpgrade.onClick.AddListener(() => OnUpgrade());
    }

    public void OnUpgrade()
    {
        if (_panelUpgrade.gameObject.activeSelf)
        {
            _panelUpgrade.gameObject.SetActive(false);
        }
        else
        {
            _panelUpgrade.gameObject.SetActive(true);

            // Đảm bảo refresh sau khi UI đã được kích hoạt
            StartCoroutine(DelayedRefresh());
        }
    }

    private IEnumerator DelayedRefresh()
    {
        // Chờ 1 frame để đảm bảo UI đã được kích hoạt và cập nhật
        yield return null;
        OnOpenUpgrade?.Invoke();
        // HeroInventoryUI.Instance.Refresh();
        HeroUpgradeUI.Instance.RefreshUI();
    }
}
