using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Damageable damageable;
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private Shooter shooter;

    public class Factory : PlaceholderFactory<Enemy> { }

    private void OnEnable()
    {
        damageable.OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        damageable.OnDeath -= OnDeath;
    }

    public void Initialize(EnemyMovementData data)
    {
        movement.Initialize(data);
        movement.Enablemovement(true);
        shooter.EnableShooting(true);
    }

    private void OnDeath()
    {
        movement.Enablemovement(false);
        shooter.EnableShooting(false);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Consts.Bullet) && other.TryGetComponent<Bullet>(out var bullet))
        {
            damageable.TakeDamage(bullet.Damage);
        }
    }
}
