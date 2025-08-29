using Enums;
using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    private GameStateManager _gameStateManager;
    
    [Inject]
    public void Construct(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
    }

    private void Start()
    {
        _gameStateManager.SetState(GameState.Dash);
    }
}
