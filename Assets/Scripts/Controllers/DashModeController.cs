using Enums;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class DashModeController : MonoBehaviour
    {
        private GameStateManager _gameStateManager;
        private LevelGenerator _levelGenerator;

        private bool _isObstaclesMoving;
    
        [Inject]
        public void Construct(GameStateManager gameStateManager, LevelGenerator levelGenerator)
        {
            _gameStateManager = gameStateManager;
            _levelGenerator = levelGenerator;
        
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
                    _levelGenerator.ResetLevel();
                    ResetMovingObstacles();
                    StartAllRoadsObstaclesMovement();
                    break;
                default:
                    StopAllRoadsObstaclesMovement();
                    break;
            }
        }

        private void StartAllRoadsObstaclesMovement()
        {
            if (_isObstaclesMoving)
                return;
        
            _isObstaclesMoving = true;
            foreach (var road in _levelGenerator.Roads)
            {
                road.StartObstaclesMovement();
            }
        }

        private void StopAllRoadsObstaclesMovement()
        {
            if (!_isObstaclesMoving)
                return;
        
            _isObstaclesMoving = false;
            foreach (var road in _levelGenerator.Roads)
            {
                road.StopObstaclesMovement();
            }
        }

        private void ResetMovingObstacles()
        {
            foreach (var road in _levelGenerator.Roads)
            {
                road.ResetObstacles();
            }
        }
    }
}
