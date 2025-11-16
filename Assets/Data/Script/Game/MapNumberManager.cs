using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MapNumberManager : MonoBehaviour
{
    [SerializeField] private List<Image> _listParent;
    void OnEnable()
    {
        MapSelectionManager.OnTotalUnLockChanged += RefreshButton;
        RefreshButton();

    }
    void OnDisable()
    {
        MapSelectionManager.OnTotalUnLockChanged -= RefreshButton;
    }
    void RefreshButton()
    {
        if (_listParent.Count <= 0)
        {
            Debug.Log("List Image is null");
        }
        else
        {
            for (int i = 0; i < _listParent.Count; i++)
            {
                TMP_Text _text = _listParent[i].gameObject.GetComponentInChildren<TMP_Text>();
                _text.text = $"{i + 1}";
                Button btn = _listParent[i].GetComponent<Button>();
                btn.interactable = (i + 1) <= MapSelectionManager.Instance.GetTotalUnlock();
            }
        }
    }
}
