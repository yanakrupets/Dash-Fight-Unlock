using UnityEngine;
using UnityEngine.InputSystem;

public class FightMovement : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference holdAction;
    [SerializeField] private InputActionReference touchPositionAction;
    
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float minZBound = -3f;
    [SerializeField] private float maxZBound = 3f;
    
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
        var normalizedX = 1 - (_touchPosition.x / Screen.width) * 2;
        var movement = normalizedX * movementSpeed * Time.deltaTime;

        var targetZ = Mathf.Clamp(transform.localPosition.z + movement, minZBound, maxZBound);
    
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, targetZ);
    }
}
