using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyUIManager : MonoBehaviour
{
    [SerializeField] private Button _buttonParty;
    [SerializeField] private Image _panelParty;

    void Start()
    {
        if (_buttonParty == null || _panelParty == null)
        {
            Debug.LogWarning("Button party or panel is missing");
            return;
        }

        // Đảm bảo panel được kích hoạt tạm thời để khởi tạo UI components
        _panelParty.gameObject.SetActive(true);

        // Chờ 1 frame để các component khởi tạo xong
        StartCoroutine(InitializeUI());
    }

    private IEnumerator InitializeUI()
    {
        // Chờ 1 frame để đảm bảo tất cả Awake() đã chạy
        yield return null;

        // Tắt panel sau khi khởi tạo xong
        _panelParty.gameObject.SetActive(false);

        // Gán sự kiện click
        _buttonParty.onClick.RemoveAllListeners();
        _buttonParty.onClick.AddListener(() => OnParty());
    }

    public void OnParty()
    {
        if (_panelParty.gameObject.activeSelf)
        {
            _panelParty.gameObject.SetActive(false);
        }
        else
        {
            _panelParty.gameObject.SetActive(true);

            // Đảm bảo refresh sau khi UI đã được kích hoạt
            StartCoroutine(DelayedRefresh());
        }
    }

    private IEnumerator DelayedRefresh()
    {
        // Chờ 1 frame để đảm bảo UI đã được kích hoạt và cập nhật
        yield return null;

        HeroInventoryUI.Instance.Refresh();
        HeroLineupUI.Instance.RefreshUI();
    }
}