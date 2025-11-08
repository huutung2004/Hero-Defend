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
        _panelParty.gameObject.SetActive(false);
        _buttonParty.onClick.AddListener(() => OnParty());
    }

    public void OnParty()
    {
        if (_panelParty.IsActive())
        {
            _panelParty.gameObject.SetActive(false);
        }else _panelParty.gameObject.SetActive(true);
        
    }
}
