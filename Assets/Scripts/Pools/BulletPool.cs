using UnityEngine;
using Zenject;

namespace Pools
{
    public class BulletPool : BasePool<Bullet>
    {
        private Bullet.Factory _factory;

        [Inject]
        private void Construct(Bullet.Factory factory)
        {
            _factory = factory;
        }
        
        public override void Return(Bullet obj)
        {
            base.Return(obj);
            obj.ResetBullet();
        }

        protected override Bullet Create()
        {
            var bullet = _factory.Create();
            bullet.transform.SetParent(transform);
            bullet.gameObject.SetActive(false);
            return bullet;
        }
    }
}
