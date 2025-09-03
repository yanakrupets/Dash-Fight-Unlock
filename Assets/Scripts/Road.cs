using System.Collections;
using System.Collections.Generic;
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
    private List<Obstacle> _movingObstacles = new();

    [Inject]
    public void Construct(IPool<Obstacle> obstaclePool)
    {
        _obstaclePool = obstaclePool;

        roadBound.OnTrigger += HandleBoundTrigger;
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

    public void ResetObstacles()
    {
        foreach (var obstacle in _movingObstacles)
        {
            _obstaclePool.Return(obstacle);
        }
        
        _movingObstacles.Clear();
    }

    private void HandleBoundTrigger(Obstacle obstacle)
    {
        _obstaclePool.Return(obstacle);
        _movingObstacles.Remove(obstacle);
    }

    private IEnumerator SpawnRoutine()
    {
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
        obstacle.transform.localRotation = _roadSide == RoadSide.Left 
            ? Quaternion.identity 
            : Quaternion.Euler(0f, 180f, 0f);
        obstacle.Initialize(_speed);
        
        _movingObstacles.Add(obstacle);
    }
    
    private float GetRandomSpawnDelay()
    {
        return Random.Range(delayBetweenObstacles.x, delayBetweenObstacles.y);
    }
}
