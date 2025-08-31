using Enums;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    private GameStateManager _gameStateManager;
    private LevelGenerator _levelGenerator;
    
    [Inject]
    public void Construct(GameStateManager gameStateManager, LevelGenerator levelGenerator)
    {
        _gameStateManager = gameStateManager;
        _levelGenerator = levelGenerator;
        
        _gameStateManager.OnGameStateChanged += HandleGameStateChange;
        
        _levelGenerator.GenerateLevel();
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
                StartFight();
                break;
            case GameState.Unlock:
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

    private void StartFight()
    {
        _levelGenerator.FightZone.GenerateEnemies();
    }
}
