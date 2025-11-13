using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "New Enemy")]
public class EnemyData : ScriptableObject
{
    public string _name;
    public float _hp;
    public GameObject _enemyPrefab;
    public Sprite _spriteEnemy;
    public EnemyType enemyType;

}
