using System.Collections;
using Enums;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Damageable damageable;
    [SerializeField] private Transform graphic;
    [SerializeField] private Shooter shooter;

    private GameStateManager _gameStateManager;
    
    private Vector3 _originalPosition;
    private Vector3 _originalGraphicScale;
    private Vector3 _originalGraphicPosition;
    private Vector3 _crashedGraphicScale;
    private Vector3 _crashedGraphicPosition;
    private bool _isCrashed;

    [Inject]
    private void Construct(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
        
        _originalPosition = transform.position;
        _originalGraphicScale = graphic.localScale;
        _originalGraphicPosition = graphic.localPosition;
        
        _crashedGraphicScale = new Vector3(
            _originalGraphicScale.x, 
            _originalGraphicScale.y / 2, 
            _originalGraphicScale.z);
        
        _crashedGraphicPosition = new Vector3(
            _originalGraphicPosition.x, 
            _originalGraphicPosition.y - graphic.localScale.y / 2, 
            _originalGraphicPosition.z);
        
        damageable.ResetHealthPoints();
        
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
                ResetPlayer();
                break;
            case GameState.Fight:
                shooter.EnableShooting(true);
                break;
            default:
                shooter.EnableShooting(false);
                break;
        }
    }
    
    private void OnDeath()
    {
        switch (_gameStateManager.CurrentState)
        {
            case GameState.Crashed:
                StartCoroutine(Crash());
                break;
            case GameState.Fight:
                shooter.EnableShooting(false);
                _gameStateManager.RestartGame();
                break;
        }
    }
    
    private IEnumerator Crash()
    {
        if (_isCrashed)
            yield return null;
        
        _isCrashed = true;

        graphic.localScale = _crashedGraphicScale;
        graphic.localPosition = _crashedGraphicPosition;

        yield return new WaitForSeconds(0.5f);
        
        _gameStateManager.RestartGame();
    }

    private void ResetPlayer()
    {
        if (_isCrashed)
        {
            graphic.localScale = _originalGraphicScale;
            graphic.localPosition = _originalGraphicPosition;
            
            _isCrashed = false;
        }
        
        transform.position = _originalPosition;
        shooter.EnableShooting(false);
        damageable.ResetHealthPoints();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Consts.Bullet) && other.TryGetComponent<Bullet>(out var bullet))
        {
            damageable.TakeDamage(bullet.Damage);
        }

        if (other.CompareTag(Consts.Obstacle))
        {
            _gameStateManager.SetState(GameState.Crashed);
            damageable.Die();
        }
    }
}
