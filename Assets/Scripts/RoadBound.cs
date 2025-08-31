using Interfaces;
using UnityEngine;
using Zenject;

public class RoadBound : MonoBehaviour
{
    private IPool<Obstacle> _obstaclePool;
    
    [Inject]
    public void Construct(IPool<Obstacle> obstaclePool)
    {
        _obstaclePool = obstaclePool;
    }
    
    public void SetPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Consts.Obstacle) && other.TryGetComponent<Obstacle>(out var obstacle))
        {
            _obstaclePool.Return(obstacle);
        }
    }
}
