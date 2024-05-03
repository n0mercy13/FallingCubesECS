using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Codebase.Features.Core;
using Codebase.Infrastructure;
using Codebase.StaticData;

namespace Codebase.Features
{
    public sealed class CubesSpawnSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<CreateCubeRequest>, Exc<Timer>> _requestFilter = default;
        private readonly EcsFilterInject<Inc<DisabledState, TransformRef>> _disabledCubesFilter = default;
        private readonly EcsPoolInject<GravityAffected> _gravityPool = default;
        private readonly EcsPoolInject<Timer> _timerPool = default;
        private readonly EcsWorldInject _world = default;

        private readonly EcsCustomInject<GameFactory> _gameFactory = default;
        private readonly EcsCustomInject<RandomService> _randomService = default;
        private readonly EcsCustomInject<GameConfig> _gameConfig = default;
        private readonly EcsCustomInject<SceneData> _sceneData = default;

        private bool _isRequestFulfilled;

        public void Init(IEcsSystems systems)
        {
            _requestFilter.Pools.Inc1
                .Add(_world.Value
                .NewEntity());
        }

        public void Run(IEcsSystems systems)
        {
            foreach(int requestEntity in _requestFilter.Value)
            {
                _isRequestFulfilled = false;
                _timerPool.Value.Add(requestEntity).Value = _gameConfig.Value.SpawnDelay;

                foreach(int cubeEntity in _disabledCubesFilter.Value)
                {
                    ref Transform cubeTransform = ref _disabledCubesFilter.Pools.Inc2.Get(cubeEntity).Value;
                    cubeTransform.position = GetRandomSpawnPosition();
                    cubeTransform.gameObject.SetActive(true);
                    _disabledCubesFilter.Pools.Inc1.Del(cubeEntity);
                    _gravityPool.Value.Add(cubeEntity);

                    _isRequestFulfilled = true;
                    break;
                }

                if (_isRequestFulfilled == false) 
                {
                    _gameFactory.Value
                        .CreateCube(GetRandomSpawnPosition());
                }
            }
        }

        private Vector3 GetRandomSpawnPosition()
        {
            return _randomService.Value
                    .Range(_sceneData.Value.SpawnPointMin.position, _sceneData.Value.SpawnPointMax.position);
        }
    }
}
