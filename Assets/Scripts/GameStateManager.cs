using System;
using Enums;

public class GameStateManager
{
    public event Action<GameState> OnGameStateChanged;

    public GameState CurrentState { get; private set; }

    public void SetState(GameState state)
    {
        CurrentState = state;
        OnGameStateChanged?.Invoke(CurrentState);
    }
}
