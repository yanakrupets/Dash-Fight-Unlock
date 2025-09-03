using System;
using Enums;
using UI;
using UnityEngine;
using Zenject;

public class UnlockModeController : MonoBehaviour
{
    [SerializeField] private int keysCountToUnlock = 3;
    [SerializeField] private CanvasView unlockCanvas;
    [SerializeField] private KeyGrid keyGrid;
    [SerializeField] private Lock lockItem;
    
    private GameStateManager _gameStateManager;
    private ColorManager _colorManager;

    [Inject]
    private void Construct(GameStateManager gameStateManager, ColorManager colorManager)
    {
        _gameStateManager = gameStateManager;
        _colorManager = colorManager;
        
        _gameStateManager.OnGameStateChanged += HandleGameStateChange;
    }
    
    private void OnDestroy()
    {
        if (_gameStateManager != null)
            _gameStateManager.OnGameStateChanged -= HandleGameStateChange;
    }
    
    private void HandleGameStateChange(GameState newState)
    {
        //Debug.Log("Unlock Mode " + newState);
        switch (newState)
        {
            case GameState.Unlock:
                Generate();
                unlockCanvas.Show();
                break;
            default:
                unlockCanvas.Hide();
                break;
        }
    }

    private void Generate()
    {
        ResetUnlock();
        
        var currentSessionColors = _colorManager.GetRandomColors(keysCountToUnlock, true);
        lockItem.Initialize(currentSessionColors);
        keyGrid.Initialize(currentSessionColors);
    }

    private void ResetUnlock()
    {
        keyGrid.ResetGrid();
    }
}
