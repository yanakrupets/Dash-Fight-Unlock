using UnityEngine;
using UnityEngine.UI;

public class UnlockCanvas : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GraphicRaycaster raycaster;

    public void Show()
    {
        canvas.enabled = true;
        raycaster.enabled = true;
    }

    public void Hide()
    {
        canvas.enabled = false;
        raycaster.enabled = false;
    }
}
