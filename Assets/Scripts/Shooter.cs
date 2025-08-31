using Interfaces;
using UnityEngine;
using Zenject;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float shootCooldown = 0.5f;
    [SerializeField] private Material material;
    
    private IPool<Bullet> _bulletPool;
    
    private bool _canShoot;
    private float _shootTimer;
    
    [Inject]
    private void Construct(IPool<Bullet> bulletPool)
    {
        _bulletPool = bulletPool;
    }
    
    private void Update()
    {
        if (!_canShoot) return;
        
        _shootTimer -= Time.deltaTime;
        
        if (_shootTimer <= 0f)
        {
            Shoot();
            _shootTimer = shootCooldown;
        }
    }
    
    public void EnableShooting(bool canShoot)
    {
        _canShoot = canShoot;
    }

    private void Shoot()
    {
        var bullet = _bulletPool.Get();
        bullet.transform.position = transform.position;
        bullet.Initialize(transform.right, material);
    }
}
