using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform graphic;

    private bool _isCrashed;
    
    public void Crash()
    {
        if (_isCrashed)
            return;
        
        _isCrashed = true;
        
        var startScale = graphic.localScale;
        graphic.localScale = new Vector3(startScale.x, startScale.y / 2, startScale.z);
        
        var startPosition = graphic.localPosition;
        graphic.localPosition = new Vector3(startPosition.x, startPosition.y - graphic.localScale.y / 2, startPosition.z);
        
        OnCrashComplete();
    }
    
    private void OnCrashComplete()
    {
        Debug.Log("game over");
    }
}
