using System;
using Enums;

public class GameStateManager
{
    public event Action<GameState> OnGameStateChanged;

    private GameState CurrentState { get; set; }

    public void SetState(GameState state)
    {
        CurrentState = state;
        OnGameStateChanged?.Invoke(CurrentState);
    }
}
