using Enums;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float _speed;
    private bool _isMoving;

    private void Update()
    {
        if (!_isMoving)
            return;
        
        Move();
    }

    public void Initialize(float speed)
    {
        _speed = speed;
        _isMoving = true;
    }
    
    public void ResetObstacle()
    {
        _isMoving = false;
        _speed = 0f;
    }

    private void Move()
    {
        var movement = transform.forward * _speed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }
}
