using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneMenu : MonoBehaviour
{
    private Button _button;
    void Start()
    {
        _button = GetComponent<Button>();
        if (_button != null)
        {
            _button.onClick.AddListener(() => LoadScene());
        }
    }
    private void LoadScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
    
}
