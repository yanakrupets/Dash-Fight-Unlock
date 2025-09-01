using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Zenject;

public class FightZone : MonoBehaviour
{
    [SerializeField] private Chest chest;
    [SerializeField] private EnemyMovementData[] movementData;
    
    private Enemy.Factory _enemyFactory;
    
    private readonly List<Enemy> _enemies = new();
    
    public event Action OnFightWon;

    [Inject]
    private void Construct(Enemy.Factory enemyFactory)
    {
        _enemyFactory = enemyFactory;
    }
    
    public class Factory : PlaceholderFactory<FightZone> { }

    private void OnDestroy()
    {
        foreach (var enemy in _enemies)
        {
            enemy.OnDeath -= HandleEnemyDeath;
        }
        
        _enemies.Clear();
    }

    public void GenerateEnemies()
    {
        foreach (var data in movementData)
        {
            var enemy = _enemyFactory.Create();
            enemy.transform.SetParent(transform);
            enemy.Initialize(data);
            
            enemy.OnDeath += HandleEnemyDeath;
            
            _enemies.Add(enemy);
        }
    }
    
    private void HandleEnemyDeath(Enemy enemy)
    {
        enemy.OnDeath -= HandleEnemyDeath;
        _enemies.Remove(enemy);
        
        if (_enemies.Count == 0)
        {
            chest.Show();
            OnFightWon?.Invoke();
        }
    }
}
