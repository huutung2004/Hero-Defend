using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="LevelData",menuName ="new LevelData")]
public class LevelData : ScriptableObject
{
    public int _totalWave;
    public int _diamondReward;
    public int _startGold;
    public List<HeroData> heroRewards;

}
