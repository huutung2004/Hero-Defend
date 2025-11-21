using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    [SerializeField]private int _enemyAlive;
    //event 
    public static Action OnEnemyClear;
    void Awake()
    {
        Instance = this;
    }
    public void RegisterAlive()
    {
        _enemyAlive++;
    }
    public void UnregisterAlive()
    {
        _enemyAlive--;
        if (_enemyAlive == 0 && LevelController.Instance.IsLastWave() && LevelController.Instance._isLastWaveSpawned)
        {
            OnEnemyClear?.Invoke();
        }
    }
}
