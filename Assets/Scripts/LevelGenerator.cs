using System.Collections.Generic;
using Enums;
using UnityEngine;
using Zenject;

public class LevelGenerator : MonoBehaviour
{
    [Header("Road Settings")]
    [SerializeField] private int roadsCount = 3;
    [SerializeField] private Vector2 speedRange = new(3f, 5f);
    [SerializeField] private float roadWidth = 1f;
    
    [Header("Safe Zone Settings")]
    [Range(0, 1)]
    [SerializeField] private float safeZoneChance = 0.3f;
    
    [Header("Prefabs")]
    [SerializeField] private GameObject safeZonePrefab;
    [SerializeField] private GameObject fightZonePrefab;
    
    private Road.Factory _roadFactory;
    private FightZone.Factory _fightZoneFactory;
    
    private float _currentX;

    public List<Road> Roads { get; } = new();
    public FightZone FightZone { get; private set; }

    [Inject]
    public void Construct(Road.Factory roadFactory, FightZone.Factory fightZoneFactory)
    {
        _roadFactory = roadFactory;
        _fightZoneFactory = fightZoneFactory;
        
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        GenerateRoads();
        GenerateFightZone();
    }
    
    private void GenerateRoads()
    {
        for (var i = 0; i < roadsCount; i++)
        {
            var road = _roadFactory.Create();
            road.transform.SetParent(transform);
            road.transform.localPosition = new Vector3(_currentX, 0f, 0f);
            road.Initialize(GetRandomSpeed(), GetRandomSide());
            Roads.Add(road);

            _currentX += roadWidth;

            if (i < roadsCount - 1 && Random.value < safeZoneChance)
            {
                Instantiate(safeZonePrefab, new Vector3(_currentX, 0f, 0f), Quaternion.identity, transform);
                _currentX += roadWidth;
            }
        }
    }
    
    private void GenerateFightZone()
    {
        FightZone = _fightZoneFactory.Create();
        FightZone.transform.SetParent(transform);
        FightZone.transform.localPosition = new Vector3(_currentX, 0f, 0f);
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
