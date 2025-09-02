using Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class Lock : MonoBehaviour, IDropHandler
    {
        [SerializeField] private KeyCounter keyCounter;
        [SerializeField] private Image lockImage;
        
        private GameStateManager _gameStateManager;
        
        private int _currentKeyCount;
        private ColorData[] _colorData;
        private int _currentKeyIndex;

        [Inject]
        private void Construct(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }
        
        public void Initialize(ColorData[] colors)
        {
            _colorData = colors;
            _currentKeyCount = 0;
            _currentKeyIndex = 0;

            UpdateLockAppearance();
            
            keyCounter.Initialize(colors.Length);
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null) return;

            if (eventData.pointerDrag.TryGetComponent<Key>(out var key))
            {
                HandleKeyDrop(key);
            }
        }
        
        private void HandleKeyDrop(Key key)
        {
            if (key.ColorType == _colorData[_currentKeyIndex].type)
            {
                key.Consume();
                _currentKeyCount++;
                keyCounter.UpdateText(_currentKeyCount);
            
                _currentKeyIndex++;
            
                if (_currentKeyCount < _colorData.Length)
                {
                    UpdateLockAppearance();
                }
                else
                {
                    _gameStateManager.SetState(GameState.Win);
                }
            }
            else
            {
                key.ReturnToOriginalPosition();
            }
        }
        
        private void UpdateLockAppearance()
        {
            lockImage.color = _colorData[_currentKeyIndex].color;
        }
    }
}
