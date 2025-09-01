using Enums;
using UnityEngine;
using Zenject;

public class DashController : MonoBehaviour
{
    private GameStateManager _gameStateManager;
    private LevelGenerator _levelGenerator;
    
    [Inject]
    public void Construct(GameStateManager gameStateManager, LevelGenerator levelGenerator)
    {
        _gameStateManager = gameStateManager;
        _levelGenerator = levelGenerator;
        
        _gameStateManager.OnGameStateChanged += HandleGameStateChange;
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
                StopAllRoadsObstaclesMovement();
                break;
        }
    }

    private void StartAllRoadsObstaclesMovement()
    {
        foreach (var road in _levelGenerator.Roads)
        {
            road.StartObstaclesMovement();
        }
    }

    private void StopAllRoadsObstaclesMovement()
    {
        foreach (var road in _levelGenerator.Roads)
        {
            road.StopObstaclesMovement();
        }
    }
}
