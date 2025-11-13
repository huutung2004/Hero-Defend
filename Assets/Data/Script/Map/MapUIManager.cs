using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapUIManager : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _imagePanel;
    // Start is called before the first frame update
    void Start()
    {

        if (_button == null || _imagePanel == null) return;
        _imagePanel.gameObject.SetActive(false);
        _button.onClick.AddListener(() => OnMap());   
    }

    public void OnMap()
    {
        if (_imagePanel.IsActive())
        {
            _imagePanel.gameObject.SetActive(false);
        }else  _imagePanel.gameObject.SetActive(true);
    }
}
