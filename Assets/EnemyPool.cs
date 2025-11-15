using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance { get; private set; }
    [SerializeField] private List<EnemyData> enemies;
    private Dictionary<EnemyType, ObjectPool<MonoBehaviour>> pools;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitPools();
    }
    //Khoi tai dic [type] [enemy]
    public void InitPools()
    {
        pools = new Dictionary<EnemyType, ObjectPool<MonoBehaviour>>();
        foreach (var m in enemies)
        {
            var comp = m._enemyPrefab.GetComponent<MonoBehaviour>();
            if (comp == null)
            {
                Debug.LogError("Missing monobehavior");
                continue;
            }

            var pool = new ObjectPool<MonoBehaviour>(comp, 20, transform);
            pools[m.enemyType] = pool;
        }
    }

    public GameObject GetEnemy(EnemyType type, Vector3 pos)
    {
        if (!pools.ContainsKey(type))
        {
            Debug.Log("Dont have Enemy");
            return null;
        }
        var obj = pools[type].Get();
        obj.transform.position = pos;
        obj.GetComponent<Enemy>().enemyType = type;
        EnemyManager.Instance.RegisterAlive();
        return obj.gameObject;
    }
    public void ReturnEnemyToPool(GameObject enemy)
    {
        var type = enemy.GetComponent<Enemy>().enemyType;
        if (!pools.ContainsKey(type)) return;
        EnemyManager.Instance.UnregisterAlive();
        pools[type].ReturnToPool(enemy.GetComponent<MonoBehaviour>());
    }
}
