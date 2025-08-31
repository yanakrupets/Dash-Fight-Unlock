using Enums;
using UnityEngine;
using Zenject;

public class FightZone : MonoBehaviour
{
    [SerializeField] private EnemyMovementData[] movementData;
    
    private Enemy.Factory _enemyFactory;

    [Inject]
    private void Construct(Enemy.Factory enemyFactory)
    {
        _enemyFactory = enemyFactory;
    }
    
    public class Factory : PlaceholderFactory<FightZone> { }

    public void GenerateEnemies()
    {
        foreach (var data in movementData)
        {
            var enemy = _enemyFactory.Create();
            enemy.transform.SetParent(transform);
            enemy.Initialize(data);
        }
    }
}
