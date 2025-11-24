using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Attack set")]
    [SerializeField]
    private HeroData _heroData;
    [Header("Layer Enemy")]
    public LayerMask _enemyLayer;
    [Header("Projectile Type")]
    public ProjectileType _projectileType;
    private Animator _animator;
    private float _attackTimer = 0;
    private int _normalAttackCount = 0;
    private Transform _target;

    void Start()
    {
        if (_heroData == null)
        {
            return;
        }
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        DetectEnemy();

        if (_target != null)
        {
            TryAttack();
        }
        else
        {
            _animator.Play("idle");
        }
    }
    void DetectEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _heroData._rangeAttack, _enemyLayer);

        if (hits.Length == 0)
        {
            _target = null;
            return;
        }

        float closestDist = Mathf.Infinity;
        Transform closestEnemy = null;
        //Lay hit gan nhat
        foreach (Collider2D h in hits)
        {
            float dist = Vector2.Distance(transform.position, h.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestEnemy = h.transform;
            }
        }

        _target = closestEnemy;
    }
    void TryAttack()
    {
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= _heroData._attackCooldown)
        {
            _attackTimer = 0;
            PlayAudio();
            if (_normalAttackCount < 10)
            {
                DoNormalAttack();
                _normalAttackCount++;
            }
            else
            {
                DoStrongAttack();
                _normalAttackCount = 0;
            }
        }
    }
    void DoNormalAttack()
    {
        _animator.Play("attack");
        // Spawn instant skill
        ProjectilePool.Instance.GetProjectile(_projectileType, _target.position - new Vector3(0f, 0.5f, 0f), _heroData._damage);
    }
    void DoStrongAttack()
    {
        _animator.Play("attack");

        ProjectilePool.Instance.GetProjectile(_projectileType, _target.position - new Vector3(0f, 0.5f, 0f), _heroData._damage);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _heroData._rangeAttack);
    }
    void PlayAudio()
    {
        switch (_projectileType)
        {
            case ProjectileType.light:
                MusicManager.Instance.PlayMusic("light");
                break;
            case ProjectileType.lightv2:
                MusicManager.Instance.PlayMusic("light");
                break;
            case ProjectileType.dark:
                MusicManager.Instance.PlayMusic("dark");
                break;
            case ProjectileType.ice:
                MusicManager.Instance.PlayMusic("ice");
                break;
            case ProjectileType.icev2:
                MusicManager.Instance.PlayMusic("ice");
                break;
            case ProjectileType.fire:
                MusicManager.Instance.PlayMusic("fire");
                break;
            case ProjectileType.firev2:
                MusicManager.Instance.PlayMusic("fire");
                break;
            default: break;

        }
    }
    public HeroData GetHeroData()
    {
        return _heroData;
    }

}
