using System.Collections.Generic;
using Enums;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class RoadController : MonoBehaviour
{
    [SerializeField] private int roadsCount;
    [SerializeField] private Vector2 speedRange;
    [SerializeField] private float roadWidth = 1f;
    
    [Header("Safe Zone")]
    [Range(0, 1)]
    [SerializeField] private float safeZoneChance = 0.3f;
    [SerializeField] private GameObject safeZonePrefab;
    
    private readonly List<Road> _roads = new();
    
    private GameStateManager _gameStateManager;
    private Road.Factory _roadFactory;
    
    [Inject]
    public void Construct(GameStateManager gameStateManager, Road.Factory roadFactory)
    {
        _gameStateManager = gameStateManager;
        _roadFactory = roadFactory;
        
        _gameStateManager.OnGameStateChanged += HandleGameStateChange;
        
        GenerateRoads();
    }
    
    private void OnDestroy()
    {
        if (_gameStateManager != null)
            _gameStateManager.OnGameStateChanged -= HandleGameStateChange;
    }
    
    private void HandleGameStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.Dash:
                StartAllRoadsObstaclesMovement();
                break;
            case GameState.Fight:
            case GameState.Unlock:
                StopAllRoadsObstaclesMovement();
                break;
        }
    }
    
    private void GenerateRoads()
    {
        var currentX = 0f;
        for (var i = 0; i < roadsCount; i++)
        {
            var road = _roadFactory.Create();
            road.transform.SetParent(transform);
            road.transform.localPosition = new Vector3(currentX, 0f, 0f);
            road.Initialize(GetRandomSpeed(), GetRandomSide());
            _roads.Add(road);

            currentX += roadWidth;

            if (i < roadsCount - 1 && Random.value < safeZoneChance)
            {
                Instantiate(safeZonePrefab, new Vector3(currentX, 0f, 0f), Quaternion.identity, transform);
                currentX += roadWidth;
            }
        }
    }

    private void StartAllRoadsObstaclesMovement()
    {
        foreach (var road in _roads)
        {
            road.StartObstaclesMovement();
        }
    }

    private void StopAllRoadsObstaclesMovement()
    {
        foreach (var road in _roads)
        {
            road.StopObstaclesMovement();
        }
    }

    private RoadSide GetRandomSide()
    {
        return (RoadSide)Random.Range(0, 2);
    }

    private float GetRandomSpeed()
    {
        return Random.Range(speedRange.x, speedRange.y);
    }
}
