using Interfaces;
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 1;
    
    [Space]
    [SerializeField] private MeshRenderer meshRenderer;
    
    private IPool<Bullet> _bulletPool;
    private Vector3 _direction;
    private bool _isActive;

    public int Damage => damage;
    
    [Inject]
    private void Construct(IPool<Bullet> pool)
    {
        _bulletPool = pool;
    }
    
    public class Factory : PlaceholderFactory<Bullet> { }
    
    public void Initialize(Vector3 direction, Material material)
    {
        _direction = direction.normalized;
        _isActive = true;
        
        meshRenderer.material = material;
    }
    
    private void Update()
    {
        if (!_isActive) 
            return;
        
        transform.position += _direction * speed * Time.deltaTime;
    }
    
    public void StopMovement()
    {
        _isActive = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Consts.Player) || other.CompareTag(Consts.Enemy) || other.CompareTag(Consts.BulletBound))
        {
            _bulletPool.Return(this);
        }
    }
}
