using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="BaseHeroData", menuName ="new BaseHero")]
public class BaseWaveData : ScriptableObject
{
    public int _timeWave;
    public int _totalEnemy;
    public List<EnemyData> enemyDatas;
}
