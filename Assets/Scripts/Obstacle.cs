using Enums;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float _speed;
    private RoadSide _moveDirection;
    private bool _isMoving;

    private void Update()
    {
        if (!_isMoving)
            return;
        
        Move();
    }

    public void Initialize(RoadSide moveDirection, float speed)
    {
        _speed = speed;
        _moveDirection = moveDirection;
        _isMoving = true;
    }
    
    public void ResetObstacle()
    {
        _isMoving = false;
        _speed = 0f;
    }

    private void Move()
    {
        var direction = _moveDirection == RoadSide.Left ? 1f : -1f;
        var movement = transform.forward * direction * _speed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }
}
