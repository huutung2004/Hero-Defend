using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }
    [SerializeField] private List<Projectile> list_Projectile;
    private Dictionary<ProjectileType, ObjectPool<MonoBehaviour>> pools;
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
    //Khoi tai dic [type] [projectile]
    public void InitPools()
    {
        pools = new Dictionary<ProjectileType, ObjectPool<MonoBehaviour>>();
        foreach (var m in list_Projectile)
        {
            var comp = m.GetComponent<MonoBehaviour>();
            if (comp == null)
            {
                Debug.LogError("Missing monobehavior");
                continue;
            }

            var pool = new ObjectPool<MonoBehaviour>(comp, 20, transform);
            pools[m._projectileType] = pool;
        }
    }

    public GameObject GetProjectile(ProjectileType type, Vector3 pos, float damage)
    {
        if (!pools.ContainsKey(type))
        {
            Debug.Log("Dont have Enemy");
            return null;
        }
        var obj = pools[type].Get();
        obj.transform.position = pos;
        obj.GetComponent<Projectile>()._projectileType = type;
        obj.GetComponent<Projectile>().SetDamge(damage);
        return obj.gameObject;
    }
    public void ReturnEnemyToPool(GameObject obj)
    {
        var type = obj.GetComponent<Projectile>()._projectileType;
        if (!pools.ContainsKey(type)) return;
        pools[type].ReturnToPool(obj.GetComponent<MonoBehaviour>());
    }
}
