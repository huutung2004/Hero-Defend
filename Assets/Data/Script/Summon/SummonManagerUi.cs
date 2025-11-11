using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonManagerUi : MonoBehaviour
{
    [SerializeField] private Button _summonButton;
    [SerializeField] private Image _panelSummon;
    void Start()
    {
        if (_panelSummon == null || _summonButton == null)
        {
            Debug.LogWarning("PanelSummon or ButtonSummon is null ");
            return;
        }
        _panelSummon.gameObject.SetActive(false);
        _summonButton.onClick.AddListener(() => OnSummon());
    }
    public void OnSummon()
    {
        if (_panelSummon.IsActive())
        {
            _panelSummon.gameObject.SetActive(false);
        }
        else
        {
            _panelSummon.gameObject.SetActive(true);
        }
    }
}
