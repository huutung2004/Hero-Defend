using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameConfig", menuName = "New GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Rate a+b+c+d = 100")]
    public float _rateComon = 70f;
    public float _rateRare = 20f;
    public float _rateLegend = 7f;
    public float _rateMysthic = 3f;
    [Header("Up Hp Enemy per Wave (0-1)")]
    public float _percent = 0.1f;
    [Header("Delay Spawn Enemy")]
    public float _timeSpawnPerEnemy = 1f;
}


