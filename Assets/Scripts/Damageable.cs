using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int healthPoints = 1;
    
    private int _currentHealthPoints;
    
    public event Action OnDeath;

    private bool IsAlive => _currentHealthPoints > 0;
    public int HealthPoints => healthPoints;
    
    public void TakeDamage(int damage)
    {
        if (!IsAlive) 
            return;
        
        _currentHealthPoints -= damage;
        
        if (!IsAlive) 
            Die();
    }

    public void Die()
    {
        _currentHealthPoints = 0;
        OnDeath?.Invoke();
    }

    public void ResetHealthPoints()
    {
        _currentHealthPoints = healthPoints;
    }
}
