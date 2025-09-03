using System;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class WinCanvas : MonoBehaviour
    {
        [SerializeField] private CanvasView canvasView;
        [SerializeField] private Button restartButton;
        
        private GameStateManager _gameStateManager;

        [Inject]
        private void Construct(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;

            _gameStateManager.OnGameStateChanged += HandleGameStateChange;
            
            restartButton.onClick.AddListener(Restart);
        }

        private void OnDisable()
        {
            if (_gameStateManager != null)
                _gameStateManager.OnGameStateChanged -= HandleGameStateChange;
            
            restartButton.onClick.RemoveListener(Restart);
        }

        private void HandleGameStateChange(GameState newState)
        {
            //Debug.Log("Win Canvas " + newState);
            switch (newState)
            {
                case GameState.Win:
                    canvasView.Show();
                    break;
                default:
                    canvasView.Hide();
                    break;
            }
        }

        private void Restart()
        {
            //_gameStateManager.SetState(GameState.Restart);
            _gameStateManager.RestartGame();
        }
    }
}
