using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; set; }
    [SerializeField] private List<LevelData> _levelDatas;
    //Start--Mid--Last Wave
    [SerializeField] private BaseWaveData _startWave;
    [SerializeField] private BaseWaveData _midWave;
    [SerializeField] private BaseWaveData _LastWave;
    //Button Start
    [SerializeField] private Button _buttonStart;
    private int _levelSelected;
    public static event Action<int> OnChangeWaveState;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        try
        {
            _levelSelected = int.Parse(MapSelectionManager.Instance.GetLevel());
        }
        catch (Exception e)
        {
            Debug.Log("Lỗi:" + e);
        }
        if (_buttonStart == null)
        {
            Debug.Log("Button Start is missing");
            return;
        }
        else
        {
            _buttonStart.gameObject.SetActive(true);
            _buttonStart.onClick.AddListener(() => StartFirstWave());
        }

    }
    private void StartFirstWave()
    {
        OnChangeWaveState?.Invoke(_startWave._timeWave);
        _buttonStart.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        TimeWaveUI.OnTimeStamp += StartWave;
    }
    private void OnDisable()
    {
        TimeWaveUI.OnTimeStamp -= StartWave;
    }
    private void StartWave()
    {
        Debug.Log("Bắt đầu wave mới");
        OnChangeWaveState?.Invoke(_startWave._timeWave);
    }
    public IEnumerator DelaySpawnEnemy()
    {
        yield return new WaitForSeconds(0.3f);
    }
}
