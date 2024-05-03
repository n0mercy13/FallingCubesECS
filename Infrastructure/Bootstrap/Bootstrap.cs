using System;
using UnityEngine;
using Codebase.StaticData;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Codebase.Features;

namespace Codebase.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private SceneData _sceneData;

        private EcsWorld _world;
        private EcsSystems _systems;

        private void OnValidate()
        {
            if(_gameConfig == null)
                throw new ArgumentNullException(nameof(_gameConfig));

            if(_sceneData == null)
                throw new ArgumentNullException(nameof(_sceneData));
        }

        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _systems
                .Add(new CubesSpawnSystem())
                .Add(new TimerSystem())
                .Add(new GravitySystem())
                .Add(new ChangeColorSystem())
                .Add(new DisableAfterDelaySystem())
                .Add(new SetDisableDelaySystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem())
#endif
                .Inject(
                new GameFactory(_world, _gameConfig.CubePrefab),
                new RandomService(),
                _gameConfig, 
                _sceneData)
                .Init();
        }

        void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            if(_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }

            if(_world != null )
            {
                _world.Destroy();
                _world = null;
            }
        }
    } 
}
