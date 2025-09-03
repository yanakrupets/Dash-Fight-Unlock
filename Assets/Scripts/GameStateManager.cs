using System;
using Enums;
using UnityEngine;

public class GameStateManager
{
    public event Action<GameState> OnGameStateChanged;

    public GameState CurrentState { get; private set; }

    public void SetState(GameState state)
    {
        CurrentState = state;
        OnGameStateChanged?.Invoke(CurrentState);
    }

    public void RestartGame()
    {
        SetState(GameState.Dash);
    }
}
