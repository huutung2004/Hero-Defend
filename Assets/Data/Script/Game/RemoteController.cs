using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoteController : MonoBehaviour
{
    [SerializeField] private Button _buttonNextWave;
    [SerializeField] private Button _buttonSpeedWave;
    [SerializeField] private Button _buttonPauseWave;
    [SerializeField] private float _speed = 2f;
    //pause
    private bool _isPause = false;
    //speed
    private bool _isRunSpeed = false;
    void Start()
    {
        if (_buttonNextWave == null || _buttonPauseWave == null || _buttonSpeedWave == null) return;
        _buttonNextWave.onClick.AddListener(() => OnNextWave());
        _buttonPauseWave.onClick.AddListener(() => OnPauseWave());
        _buttonSpeedWave.onClick.AddListener(() => OnSpeedWave());
    }
    private void OnNextWave()
    {
        //Ngắn không bấm next ở wave cuối
        if (!LevelController.Instance.IsLastWave())
            LevelController.Instance.StartWave();
        else
        {
            Debug.Log("Đang là wave cuối");
        }
    }
    private void OnPauseWave()
    {
        if (_isPause)
        {
            Resume();
        }
        else Pause();
    }
    private void OnSpeedWave()
    {
        if (_isRunSpeed)
        {
            Resume();
        }
        else RunSpeed();

    }
    private void Pause()
    {
        Time.timeScale = 0f;
        _isPause = true;
    }
    private void Resume()
    {
        Time.timeScale = 1f;
        _isPause = false;
        _isRunSpeed = false;
    }
    private void RunSpeed()
    {
        Time.timeScale = _speed;
        _isRunSpeed = true;
    }
}
