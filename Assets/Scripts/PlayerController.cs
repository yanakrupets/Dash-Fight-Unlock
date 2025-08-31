using Enums;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Damageable damageable;
    [SerializeField] private Transform graphic;
    [SerializeField] private Shooter shooter;

    private GameStateManager _gameStateManager;
    private bool _isCrashed;

    [Inject]
    private void Construct(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
        
        _gameStateManager.OnGameStateChanged += HandleGameStateChange;
        damageable.OnDeath += OnDeath;
    }
    
    private void OnDestroy()
    {
        if (_gameStateManager != null)
            _gameStateManager.OnGameStateChanged -= HandleGameStateChange;
        
        damageable.OnDeath -= OnDeath;
    }
    
    private void HandleGameStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.Dash:
                shooter.EnableShooting(false);
                break;
            case GameState.Fight:
                shooter.EnableShooting(true);
                break;
            case GameState.Unlock:
                shooter.EnableShooting(false);
                break;
        }
    }

    private void Crash()
    {
        if (_isCrashed)
            return;
        
        _isCrashed = true;
        
        var startScale = graphic.localScale;
        graphic.localScale = new Vector3(startScale.x, startScale.y / 2, startScale.z);
        
        var startPosition = graphic.localPosition;
        graphic.localPosition = new Vector3(startPosition.x, startPosition.y - graphic.localScale.y / 2, startPosition.z);
    }
    
    private void OnDeath()
    {
        switch (_gameStateManager.CurrentState)
        {
            case GameState.Dash:
                Crash();
                break;
            case GameState.Fight:
                shooter.EnableShooting(false);
                break;
        }
        
        // game state on reload
        RestartGame();
    }
    
    private void RestartGame()
    {
        Debug.Log("game over");
        // restart
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Consts.Bullet) && other.TryGetComponent<Bullet>(out var bullet))
        {
            damageable.TakeDamage(bullet.Damage);
        }

        if (other.CompareTag(Consts.Obstacle))
        {
            damageable.Die();
        }
    }
}
