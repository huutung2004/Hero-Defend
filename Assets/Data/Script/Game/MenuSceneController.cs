using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneController : MonoBehaviour
{
    [SerializeField] private Button _play;
     [SerializeField] private Button _exit;
    void Start()
    {
        _play.onClick.AddListener(() => OnPlay());
        _exit.onClick.AddListener(() => OnExit());

    }
    private void OnPlay()
    {
        SceneManager.LoadScene("MainScene");
    }
    private void OnExit()
    {
        Application.Quit();
    }
}
