using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _timeLife = 1.5f;
    private bool _hasHit = false;
    private BoxCollider2D _colider2D;
    public ProjectileType _projectileType;
    private float _damage;
    private Coroutine _despawnCoroutine;

    private void Awake()
    {
        _colider2D = GetComponent<BoxCollider2D>();
    }
    private void OnEnable()
    {
        _hasHit = false;
        if (_colider2D != null)
        {
            _colider2D.enabled = true;
        }
        _despawnCoroutine = StartCoroutine(DespawnAfterTime());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_hasHit)
        {
            // Debug.Log("Đánh rồi");
            return;
        }

        if (collision.CompareTag("Enemy"))
        {
            _hasHit = true;

            EnemyHealth e = collision.GetComponent<EnemyHealth>();
            if (e != null)
            {
                e.ChangeHeal(-_damage);
                EnemyTargetManager.Instance.RegisterTarget(e);
            }

            _colider2D.enabled = false;
            if (_despawnCoroutine != null)
                StopCoroutine(_despawnCoroutine);
            _despawnCoroutine = StartCoroutine(DespawnAfterTime());
        }
        // else Debug.Log("Not found Enemy");
    }
    public void SetDamge(float damage)
    {
        _damage = damage;
    }
    private IEnumerator DespawnAfterTime()
    {
        yield return new WaitForSeconds(_timeLife);
        ProjectilePool.Instance.ReturnEnemyToPool(gameObject);
    }

}
