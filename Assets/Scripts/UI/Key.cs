using Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class Key : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image keyImage;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private CanvasGroup canvasGroup;
        
        private ColorType _colorType;
        private Vector2 _originalPosition;
        private Transform _parentTransform;

        public ColorType ColorType => _colorType;
        
        public void Initialize(ColorData colorData)
        {
            _colorType = colorData.type;
            keyImage.color = colorData.color;
            
            canvasGroup.blocksRaycasts = true;
            
            gameObject.SetActive(true);
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalPosition = rectTransform.anchoredPosition;
            _parentTransform = transform.parent;
        
            canvasGroup.blocksRaycasts = false;
        
            transform.SetParent(transform.root);
        }
    
        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
                    rectTransform, eventData.position, eventData.pressEventCamera, out var worldPoint))
            {
                rectTransform.position = worldPoint;
            }
        }
    
        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;
        
            if (transform.parent == _parentTransform || transform.parent == transform.root)
            {
                ReturnToOriginalPosition();
            }
        }

        public void ReturnToOriginalPosition()
        {
            transform.SetParent(_parentTransform);
            rectTransform.anchoredPosition = _originalPosition;
        }
    
        public void Consume()
        {
            gameObject.SetActive(false);
            ReturnToOriginalPosition();
        }
    }
}
