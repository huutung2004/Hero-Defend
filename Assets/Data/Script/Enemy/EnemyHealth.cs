using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxHeal;
    [SerializeField] private float _currentHeal;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _currentHeal = _maxHeal;
        _animator = GetComponent<Animator>();
    }
    private void ChangeHeal(float value)
    {
        _currentHeal = Mathf.Clamp(_currentHeal + value, 0, _maxHeal);
        if (_currentHeal <= 0)
        {
            _animator.SetBool("Death", true);
            EnemyPool.Instance.ReturnEnemyToPool(gameObject);
        }
    }
    public void InitHeal(float heal)
    {
        _maxHeal += heal;
        _currentHeal = _maxHeal;
    }
}
