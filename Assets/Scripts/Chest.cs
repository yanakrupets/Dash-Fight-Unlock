using Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Chest : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject graphic;
    
    private GameStateManager _gameStateManager;
    
    private bool _isAvailable;

    [Inject]
    private void Construct(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isAvailable)
            return;
        
        _gameStateManager.SetState(GameState.Unlock);
    }
    
    public void Show()
    {
        _isAvailable = true;
        graphic.SetActive(true);
    }

    public void Hide()
    {
        _isAvailable = false;
        graphic.SetActive(false);
    }
}
