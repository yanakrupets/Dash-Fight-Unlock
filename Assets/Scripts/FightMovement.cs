using UnityEngine;
using UnityEngine.InputSystem;

public class FightMovement : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference holdAction;
    [SerializeField] private InputActionReference touchPositionAction;
    
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed;
    
    private Vector2 _touchPosition;
    private bool _isHolding;

    private void OnEnable()
    {
        holdAction.action.started += HoldStarted;
        holdAction.action.canceled += HoldCanceled;
        touchPositionAction.action.performed += MoveInput;
    }

    private void OnDisable()
    {
        holdAction.action.started -= HoldStarted;
        holdAction.action.canceled -= HoldCanceled;
        touchPositionAction.action.performed -= MoveInput;
    }

    private void HoldStarted(InputAction.CallbackContext context)
    {
        _isHolding = true;
    }

    private void HoldCanceled(InputAction.CallbackContext context)
    {
        _isHolding = false;
    }

    private void MoveInput(InputAction.CallbackContext context)
    {
        _touchPosition = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (!_isHolding) return;
        var normalizedX = 1 - _touchPosition.x / Screen.width * 2;
        transform.Translate(Vector3.forward * normalizedX * movementSpeed * Time.deltaTime);
    }
}
