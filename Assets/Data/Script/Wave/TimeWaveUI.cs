using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TimeWaveUI : MonoBehaviour
{
    private TMP_Text _time;
    private float _timeFloat;
    private float _maxTime;
    //for healthbar 
    private float _currentFill;
    private float _targetFill;
    [SerializeField] private Image _imgTime;
    public static event Action OnTimeStamp;
    void Start()
    {
        _time = GetComponent<TMP_Text>();
        if (_time == null)
        {
            Debug.LogWarning("text is missing");
            return;
        }
    }
    void Update()
    {
        if (_time != null && _timeFloat > 0)
        {
            _timeFloat -= Time.deltaTime * 1.5f;
            int timeInt = (int)_timeFloat;
            _targetFill = _timeFloat / _maxTime;
            _currentFill = Mathf.Lerp(_currentFill, _targetFill, Time.deltaTime * 5f);
            _imgTime.fillAmount = _currentFill;
            RefreshUI(timeInt);
        }
        if (_timeFloat < 0)
        {
            Debug.Log("TimeStamp");
            OnTimeStamp?.Invoke();
        }
    }
    private void OnEnable()
    {
        LevelController.OnChangeWaveState += SetTime;
    }
    private void OnDisable()
    {
        LevelController.OnChangeWaveState -= SetTime;
    }
    private void SetTime(int time)
    {
        _time.text = time.ToString();
        _timeFloat = time;
        _maxTime = time;
        _targetFill = 1f;
        _currentFill = 1f;
        _imgTime.fillAmount = 1f;

    }
    private void RefreshUI(int time)
    {
        _time.text = $"{time}";
    }
}
