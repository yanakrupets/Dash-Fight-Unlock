using Interfaces;
using Pools;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Header("Controllers")]
        //[SerializeField] private PlayerController playerController;
        //[SerializeField] private FightModeController fightModeController;
        
        [Header("Pools")]
        [SerializeField] private ObstaclePool obstaclePoolPrefab;
        [SerializeField] private BulletPool bulletPoolPrefab;
        
        [Header("Factory Prefabs")]
        [SerializeField] private Road roadPrefab;
        [SerializeField] private FightZone fightZonePrefab;
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private Bullet bulletPrefab;
        
        [Space]
        [SerializeField] private LevelGenerator levelGeneratorPrefab;
        
        [Space]
        [SerializeField] private ColorData[] colorData;
        
        public override void InstallBindings()
        {
            ControllersAndManagersBindings();
            PoolBindings();
            FactoryBindings();
        }

        private void ControllersAndManagersBindings()
        {
            Container.Bind<GameStateManager>()
                .AsSingle()
                .NonLazy();

            Container.Bind<ColorManager>()
                .FromMethod(_ => new ColorManager(colorData))
                .AsSingle();

            Container.Bind<LevelGenerator>()
                .FromComponentInNewPrefab(levelGeneratorPrefab)
                .AsSingle()
                .NonLazy();
            
            // Container.Bind<PlayerController>()
            //     .FromInstance(playerController)
            //     .AsSingle()
            //     .NonLazy();
            //
            // Container.Bind<FightModeController>()
            //     .FromInstance(fightModeController)
            //     .AsSingle()
            //     .NonLazy();
        }

        private void PoolBindings()
        {
            Container.Bind<IPool<Obstacle>>()
                .To<ObstaclePool>()
                .FromComponentInNewPrefab(obstaclePoolPrefab)
                .AsSingle();
            
            Container.Bind<IPool<Bullet>>()
                .To<BulletPool>()
                .FromComponentInNewPrefab(bulletPoolPrefab)
                .AsSingle();
        }

        private void FactoryBindings()
        {
            Container.BindFactory<Road, Road.Factory>()
                .FromComponentInNewPrefab(roadPrefab);
            
            Container.BindFactory<FightZone, FightZone.Factory>()
                .FromComponentInNewPrefab(fightZonePrefab);
            
            Container.BindFactory<Enemy, Enemy.Factory>()
                .FromComponentInNewPrefab(enemyPrefab);
            
            Container.BindFactory<Bullet, Bullet.Factory>()
                .FromComponentInNewPrefab(bulletPrefab);
        }
    }
}