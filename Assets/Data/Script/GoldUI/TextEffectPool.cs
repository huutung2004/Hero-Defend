using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffectPool : MonoBehaviour
{
    public static TextEffectPool Instance { get; private set; }
    [SerializeField] private FloatingText _floatingGold;
    [SerializeField] private FloatingText _floatingDamage;
    private ObjectPool<FloatingText> poolGold;
    private ObjectPool<FloatingText> poolDamage;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            poolGold = new ObjectPool<FloatingText>(_floatingGold, 20, transform);
            poolDamage = new ObjectPool<FloatingText>(_floatingDamage, 20, transform);

        }
        else Destroy(gameObject);
    }
    public FloatingText GetGoldEffect()
    {
        return poolGold.Get();
    }
    public void ReturnGOldEffectToPool(FloatingText obj)
    {
        poolGold.ReturnToPool(obj);
    }
    public FloatingText GetDamageEffect()
    {
        return poolDamage.Get();
    }
    public void ReturnDamageEffectToPool(FloatingText obj)
    {
        poolDamage.ReturnToPool(obj);
    }
}
