using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Hero Data", menuName ="New Hero")]
public class HeroData : ScriptableObject
{
    public string _name;
    public Sprite _previewImage;
    public GameObject _heroPrefab;
    public float _rangeAttack;
    public float _attackCooldown;
    public float _damage;
    public int _price;
    public HeroRarity _rarity;
}
