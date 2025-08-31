using Enums;
using UnityEngine;
using Zenject;

public class FightModeTrigger : MonoBehaviour
{
    private GameStateManager _gameStateManager;

    [Inject]
    private void Construct(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Consts.Player))
        {
            _gameStateManager.SetState(GameState.Fight);
        }
    }
}
