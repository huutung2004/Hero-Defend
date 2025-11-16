using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldEffectPool : MonoBehaviour
{
    public static GoldEffectPool Instance { get; private set; }
    [SerializeField] private FloatingGold _floatingGold;
    private ObjectPool<FloatingGold> pool;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            pool = new ObjectPool<FloatingGold>(_floatingGold, 20, transform);
        }
        else Destroy(gameObject);
    }
    public FloatingGold GetGoldEffect()
    {
        return pool.Get();
    }
    public void ReturnGoldEffectToPool(FloatingGold obj)
    {
        pool.ReturnToPool(obj);
    }
}
