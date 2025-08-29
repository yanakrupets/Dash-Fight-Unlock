using Interfaces;
using Pools;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ObstaclePool obstaclePoolPrefab;
        [SerializeField] private Road roadPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<GameStateManager>()
                .AsSingle()
                .NonLazy();
            
            PoolBindings();
            FactoryBindings();
        }

        private void PoolBindings()
        {
            Container.Bind<IPool<Obstacle>>()
                .To<ObstaclePool>()
                .FromComponentInNewPrefab(obstaclePoolPrefab)
                .AsSingle();
        }

        private void FactoryBindings()
        {
            Container.BindFactory<Road, Road.Factory>()
                .FromComponentInNewPrefab(roadPrefab);
        }
    }
}