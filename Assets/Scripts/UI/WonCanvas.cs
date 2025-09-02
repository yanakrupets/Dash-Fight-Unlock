using Enums;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class WonCanvas : MonoBehaviour
    {
        [SerializeField] private CanvasView canvasView;
        
        private GameStateManager _gameStateManager;

        [Inject]
        private void Construct(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;

            _gameStateManager.OnGameStateChanged += HandleGameStateChange;
        }
        
        private void HandleGameStateChange(GameState newState)
        {
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
    }
}
