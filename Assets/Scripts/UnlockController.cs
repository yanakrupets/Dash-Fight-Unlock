using Enums;
using UnityEngine;
using Zenject;

public class UnlockController : MonoBehaviour
{
    [SerializeField] private UnlockCanvas unlockCanvas;
    
    private GameStateManager _gameStateManager;

    [Inject]
    private void Construct(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
        
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
            case GameState.Unlock:
                unlockCanvas.Show();
                break;
        }
    }
}
