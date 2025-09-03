using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private const float DistanceThreshold = 0.1f;
    
    private Vector3 _pointA;
    private Vector3 _pointB;
    private float _speed;
    
    private Vector3 _nextPosition;
    private bool _isMoving;
    
    private void Update()
    {
        if (!_isMoving)
            return;
        
        Move();
    }
    
    public void Initialize(EnemyMovementData data)
    {
        _pointA = data.pointA;
        _pointB = data.pointB;
        _speed = data.speed;
        
        transform.localPosition = _pointA;
        _nextPosition = _pointB;
    }
    
    public void EnableMovement(bool isMoving)
    {
        _isMoving = isMoving;
    }
    
    private void Move()
    {
        transform.localPosition = Vector3.MoveTowards(
            transform.localPosition, 
            _nextPosition, 
            _speed * Time.deltaTime);

        if (Vector3.Distance(transform.localPosition, _nextPosition) <= DistanceThreshold)
        {
            _nextPosition = _nextPosition == _pointA ? _pointB : _pointA;
        }
    }
}
