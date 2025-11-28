using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxHeal;
    [SerializeField] private float _currentHeal;
    private Animator _animator;
    private EnemyData _enemyData;
    // Start is called before the first frame update
    void Start()
    {
        _currentHeal = _maxHeal;
        _animator = GetComponent<Animator>();
        _enemyData = GetComponent<Enemy>().enemyData;

    }
    void OnEnable()
    {
        _currentHeal = _maxHeal;
    }
    public void ChangeHeal(float value)
    {
        if (_currentHeal == 0) return;
        _currentHeal = Mathf.Clamp(_currentHeal + value, 0, _maxHeal);

        Debug.Log($"Hp: {_currentHeal}");
        if (value < 0)
        {
            var obj = TextEffectPool.Instance.GetDamageEffect();
            obj.Play(transform.position, $"{value}");
        }
        if (_currentHeal <= 0)
        {
            _animator.SetBool("Death", true);
            var obj = TextEffectPool.Instance.GetGoldEffect();
            obj.Play(transform.position, $"+ {_enemyData._goldDrop}");
            GoldManager.Instance.ChangeTotalGold(_enemyData._goldDrop);
            EnemyPool.Instance.ReturnEnemyToPool(gameObject);
        }
    }
    public bool IsDeath()
    {
        return _currentHeal == 0 || !gameObject.activeSelf;
    }
    public void InitHeal(float heal)
    {
        _maxHeal += heal;
        _currentHeal = _maxHeal;
    }
    public void BuffHealPercent(float percent)
    {
        _maxHeal += _maxHeal * percent;
    }
    public float GetHealthPercent()
    {
        return _currentHeal / _maxHeal;
    }
    public Sprite GetSprite()
    {
        return _enemyData._spriteEnemy;
    }
    public float GetCurrentHealth()
    {
        return _currentHeal;
    }
    public float GetMaxHealth()
    {
        return _maxHeal;
    }
}
