using System.Collections;
using Enums;
using Interfaces;
using UnityEngine;
using Zenject;

public class Road : MonoBehaviour
{
    [SerializeField] private RoadBound roadBound;
    
    [SerializeField] private Vector3 leftPoint;
    [SerializeField] private Vector3 rightPoint;
    
    [SerializeField] private Vector2 delayBetweenObstacles = new(1f, 3f);

    private IPool<Obstacle> _obstaclePool;
    
    private float _speed;
    private Vector3 _spawnPoint;
    private RoadSide _roadSide;
    private Coroutine _spawnCoroutine;

    [Inject]
    public void Construct(IPool<Obstacle> obstaclePool)
    {
        _obstaclePool = obstaclePool;
    }
    
    public class Factory : PlaceholderFactory<Road> { }

    public void Initialize(float speed, RoadSide side)
    {
        _speed = speed;
        _roadSide = side;
        
        _spawnPoint = side == RoadSide.Left ? leftPoint : rightPoint;
        
        var boundPosition = side == RoadSide.Left ? rightPoint : leftPoint;
        roadBound.SetPosition(boundPosition);
    }
    
    public void StartObstaclesMovement()
    {
        _spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    public void StopObstaclesMovement()
    {
        if (_spawnCoroutine == null) return;
        
        StopCoroutine(_spawnCoroutine);
        _spawnCoroutine = null;
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(GetRandomSpawnDelay());
        
        while (true)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(GetRandomSpawnDelay());
        }
    }
    
    private void SpawnObstacle()
    {
        var obstacle = _obstaclePool.Get();
        obstacle.transform.SetParent(transform);
        obstacle.transform.localPosition = _spawnPoint;
        obstacle.Initialize(_roadSide, _speed);
    }
    
    private float GetRandomSpawnDelay()
    {
        return Random.Range(delayBetweenObstacles.x, delayBetweenObstacles.y);
    }
}
