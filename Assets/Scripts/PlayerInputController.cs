using Enums;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    
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
            case GameState.Dash:
                playerInput.SwitchCurrentActionMap(Consts.DashMap);
                break;
            case GameState.Fight:
                playerInput.SwitchCurrentActionMap(Consts.FightMap);
                break;
            case GameState.Unlock:
                playerInput.DeactivateInput();
                break;
        }
    }
}
