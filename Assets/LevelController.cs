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
    //Point Spawn Enemy
    [SerializeField] private Transform _pointSpawn;
    //For Level
    private int _levelSelected;
    private int _waveCount;
    private int _totalWave;
    //event
    public static event Action<int> OnChangeWaveState;
    public static event Action OnCompleteLevel;
    public static event Action<int, int> WavePerTotal;
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
            _totalWave = _levelDatas[_levelSelected - 1]._totalWave;
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
        _buttonStart.gameObject.SetActive(false);
        StartWave();
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
        if (_waveCount > _totalWave)
        {
            OnCompleteLevel?.Invoke();
            return;
        }
        _waveCount += 1;
        WavePerTotal?.Invoke(_waveCount,_totalWave);
        Debug.Log("Bắt đầu wave mới");
        //1-mid
        if (_waveCount < _totalWave / 2)
        {
            OnChangeWaveState?.Invoke(_startWave._timeWave);
            SpawnEnemy(_startWave);
            //mid-last
        }
        else if (_waveCount != _totalWave)
        {
            OnChangeWaveState?.Invoke(_midWave._timeWave);
            SpawnEnemy(_midWave);
        }
        //last
        else
        {
            OnChangeWaveState?.Invoke(_LastWave._timeWave);
            SpawnEnemy(_LastWave);
        }

    }
    private void SpawnEnemy(BaseWaveData waveData)
    {
        StartCoroutine(DelaySpawnEnemy(waveData));
    }
    public IEnumerator DelaySpawnEnemy(BaseWaveData waveData)
    {
        for (int i = 1; i <= waveData._totalEnemy; i++)
        {
            //Spawm
            Instantiate(waveData.enemyDatas[0]._enemyPrefab, _pointSpawn.position, _pointSpawn.rotation);
            Debug.Log($"Spawn Enemy{i}");
            yield return new WaitForSeconds(1f);
        }
    }
}
