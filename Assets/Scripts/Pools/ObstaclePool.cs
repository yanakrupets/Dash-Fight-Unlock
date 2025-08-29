using UnityEngine;

namespace Pools
{
    public class ObstaclePool : BasePool<Obstacle>
    {
        [SerializeField] private Obstacle[] obstaclePrefabs;

        public override void Return(Obstacle obj)
        {
            base.Return(obj);
            obj.ResetObstacle();
        }

        protected override Obstacle Create()
        {
            var randomPrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            var item = Instantiate(randomPrefab, transform);
            item.gameObject.SetActive(false);
            return item;
        }
    }
}