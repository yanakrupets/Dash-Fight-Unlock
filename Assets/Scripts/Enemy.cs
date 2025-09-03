using System;
using UI;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Damageable damageable;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private Shooter shooter;
    
    public event Action<Enemy> OnDeath;

    public class Factory : PlaceholderFactory<Enemy> { }

    private void OnEnable()
    {
        damageable.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        damageable.OnDeath -= HandleDeath;
    }

    public void Initialize(EnemyMovementData data)
    {
        movement.Initialize(data);
        
        movement.EnableMovement(true);
        shooter.EnableShooting(true);
        
        damageable.ResetHealthPoints();
        healthBar.Initialize(damageable.HealthPoints);
    }

    private void HandleDeath()
    {
        movement.EnableMovement(false);
        shooter.EnableShooting(false);
        
        OnDeath?.Invoke(this);
        
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Consts.Bullet) && other.TryGetComponent<Bullet>(out var bullet))
        {
            damageable.TakeDamage(bullet.Damage);
            healthBar.UpdateHealthBar(damageable.CurrentHealthPoints);
        }
    }
}
