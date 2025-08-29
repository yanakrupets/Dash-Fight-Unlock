using Interfaces;
using UnityEngine;
using Zenject;

public class RoadBound : MonoBehaviour
{
    private IPool<Obstacle> _pool;
    
    [Inject]
    public void Construct(IPool<Obstacle> pool)
    {
        _pool = pool;
    }
    
    public void SetPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && other.TryGetComponent<Obstacle>(out var obstacle))
        {
            _pool.Return(obstacle);
        }
    }
}
