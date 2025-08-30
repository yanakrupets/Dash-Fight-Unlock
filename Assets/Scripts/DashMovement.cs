using UnityEngine;
using UnityEngine.InputSystem;

public class DashMovement : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference moveAction;
    
    [Header("Movement Settings")]
    [SerializeField] private float moveDistance = 1f;
    [SerializeField] private float moveDuration = 0.3f;
    [SerializeField] private float jumpHeight = 0.4f;
    
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private bool _isMoving;
    private float _moveTimer;
    
    private void OnEnable()
    {
        moveAction.action.performed += Move;
    }

    private void OnDisable()
    {
        moveAction.action.performed -= Move;
    }
    
    private void Update()
    {
        HandleMovement();
    }

    private void Move(InputAction.CallbackContext context)
    {
        if (_isMoving) return;
        
        StartMovement(Vector3.right);
    }
    
    private void StartMovement(Vector3 direction)
    {
        _isMoving = true;
        _startPosition = transform.position;
        _targetPosition = transform.position + direction * moveDistance;
        _moveTimer = 0f;
    }
    
    private void HandleMovement()
    {
        if (!_isMoving) return;
        
        _moveTimer += Time.deltaTime;
        var progress = Mathf.Clamp01(_moveTimer / moveDuration);
        
        var horizontalPosition = Vector3.Lerp(_startPosition, _targetPosition, progress);
        
        var jumpProgress = Mathf.Sin(progress * Mathf.PI);
        var verticalOffset = jumpHeight * jumpProgress;
        
        transform.position = horizontalPosition + Vector3.up * verticalOffset;
        
        if (progress >= 1f)
        {
            transform.position = _targetPosition;
            _isMoving = false;
        }
    }
}
