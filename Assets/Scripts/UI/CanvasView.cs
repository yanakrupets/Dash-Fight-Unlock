using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public class CanvasView : MonoBehaviour
    {
        private Canvas _canvas;
        private GraphicRaycaster _raycaster;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _raycaster = GetComponent<GraphicRaycaster>();
            Hide();
        }

        public void Show()
        {
            _canvas.enabled = true;
            _raycaster.enabled = true;
        }

        public void Hide()
        {
            _canvas.enabled = false;
            _raycaster.enabled = false;
        }
    }
}