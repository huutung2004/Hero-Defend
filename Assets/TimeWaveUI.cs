using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class TimeWaveUI : MonoBehaviour
{
    private TMP_Text _time;
    private float _timeFloat;
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
    }
    private void RefreshUI(int time)
    {
        _time.text = $"Time: {time}";
    }
}
