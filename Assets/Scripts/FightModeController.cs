using Enums;
using UnityEngine;
using Zenject;

public class FightModeController : MonoBehaviour
{
    private GameStateManager _gameStateManager;
    private LevelGenerator _levelGenerator;
    
    [Inject]
    public void Construct(GameStateManager gameStateManager, LevelGenerator levelGenerator)
    {
        _gameStateManager = gameStateManager;
        _levelGenerator = levelGenerator;
        
        _gameStateManager.OnGameStateChanged += HandleGameStateChange;
        _levelGenerator.FightZone.OnFightWon += HandleFightWon;
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
            case GameState.Fight:
                StartFight();
                break;
            case GameState.Chest:
                break;
            default:
                ResetFightZone();
                break;
        }
    }

    private void StartFight()
    {
        _levelGenerator.FightZone.GenerateEnemies();
    }

    private void ResetFightZone()
    {
        _levelGenerator.FightZone.ResetFightZone();
    }

    private void HandleFightWon()
    {
        _gameStateManager.SetState(GameState.Chest);
    }
}
