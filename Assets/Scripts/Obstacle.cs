using Enums;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Obstacle : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _speed;
    private RoadSide _moveDirection;
    private bool _isMoving;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!_isMoving)
            return;
        
        Move();
    }

    public void Initialize(RoadSide moveDirection, float speed, Vector3 spawnPosition)
    {
        _speed = speed;
        _moveDirection = moveDirection;
        _isMoving = true;
        
        transform.localPosition = spawnPosition;
        _rigidbody.position = spawnPosition;
    }
    
    public void ResetObstacle()
    {
        _isMoving = false;
        _speed = 0f;
    }

    private void Move()
    {
        var direction = _moveDirection == RoadSide.Left ? 1f : -1f;
        var movement = transform.forward * direction * _speed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered");
        }
    }
}
