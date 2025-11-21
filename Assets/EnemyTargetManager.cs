using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetManager : MonoBehaviour
{
    public static EnemyTargetManager Instance { get; private set; }
    public EnemyHealth _target;
    private void Awake()
    {
        Instance = this;
    }
    public void RegisterTarget(EnemyHealth enemy)
    {
        if (_target != null)
        {
            if (_target.IsDeath())
            {
                Debug.Log("is death");
                _target = null;
            }
            return;
        }
        _target = enemy;

    }
}
