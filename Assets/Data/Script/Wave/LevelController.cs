using System;
using System.Collections;
using System.Collections.Generic;
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
    //flag check
    public bool _isLastWaveSpawned = false;
    //For Level
    private int _levelSelected;
    private int _waveCount;
    private int _totalWave;
    public int _totalDiamondReward;
    public int _startGold;
    public int _totalHP;
    public List<HeroData> _heroRewards;
    //event
    public static event Action<int> OnChangeWaveState;
    public static event Action OnFailLevel;
    public static event Action<int, int> WavePerTotal;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }
    private void Start()
    {
        try
        {
            _levelSelected = int.Parse(MapSelectionManager.Instance.GetLevel());
            _totalWave = _levelDatas[_levelSelected - 1]._totalWave;
            _totalDiamondReward = _levelDatas[_levelSelected - 1]._diamondReward;
            _heroRewards = _levelDatas[_levelSelected - 1].heroRewards;
            _startGold = _levelDatas[_levelSelected - 1]._startGold;
            _totalHP = _levelDatas[_levelSelected - 1]._totalHp;
            //init gold for level
            GoldManager.Instance.SetTotalGold(_startGold);
            //init hp for level
            HpLevelManager.Instance.InitTotalHP(_totalHP);
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
    public void StartWave()
    {
        if (_waveCount >= _totalWave)
        {
            OnFailLevel?.Invoke();
            OnChangeWaveState?.Invoke(0);
            return;
        }
        _waveCount += 1;
        //ban su kien wave/wave
        WavePerTotal?.Invoke(_waveCount, _totalWave);

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
        if (EnemyPool.Instance == null)
        {
            Debug.LogError("EnemyPool is missing!");
            yield break;
        }
        for (int i = 1; i <= waveData._totalEnemy; i++)
        {
            //Spawm Boss
            if (i == 1)
            {
                EnemyType type = waveData._enemyBoss.enemyType;
                GameObject boss = EnemyPool.Instance.GetEnemy(type, _pointSpawn.position);
                boss.GetComponent<EnemyMovement>().InitWaypoints(GameObject.FindWithTag("Waypoints").transform);
                boss.transform.localScale = new Vector3(1.5f, 1.5f, 0);
                boss.GetComponent<EnemyHealth>().InitHeal(100f);
                yield return new WaitForSeconds(0.5f);
            }
            //SpawnLeader
            else if (i == 2)
            {
                EnemyType type = waveData._enemyLeader.enemyType;
                GameObject boss = EnemyPool.Instance.GetEnemy(type, _pointSpawn.position);
                boss.GetComponent<EnemyMovement>().InitWaypoints(GameObject.FindWithTag("Waypoints").transform);

                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                EnemyType type = waveData._enemyBasic.enemyType;
                GameObject boss = EnemyPool.Instance.GetEnemy(type, _pointSpawn.position);
                boss.GetComponent<EnemyMovement>().InitWaypoints(GameObject.FindWithTag("Waypoints").transform);
                yield return new WaitForSeconds(0.5f);
            }
        }
        // Debug.Log(IsLastWave()+ "-"+ _isLastWaveSpawned);
        if (IsLastWave()) _isLastWaveSpawned = true;
        Debug.Log(IsLastWave() + "-" + _isLastWaveSpawned);
    }
    public bool IsLastWave()
    {
        return _waveCount == _totalWave;
    }
}
