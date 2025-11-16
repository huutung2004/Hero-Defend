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
    public void ChangeHeal(float value)
    {
        _currentHeal = Mathf.Clamp(_currentHeal + value, 0, _maxHeal);
        Debug.Log($"Hp: {_currentHeal}");
        if (_currentHeal <= 0)
        {
            _animator.SetBool("Death", true);
            var obj = GoldEffectPool.Instance.GetGoldEffect();
            obj.Play(transform.position, _enemyData._goldDrop);
            GoldManager.Instance.ChangeTotalGold(_enemyData._goldDrop);
            EnemyPool.Instance.ReturnEnemyToPool(gameObject);
        }
    }
    public void InitHeal(float heal)
    {
        _maxHeal += heal;
        _currentHeal = _maxHeal;
    }
}
