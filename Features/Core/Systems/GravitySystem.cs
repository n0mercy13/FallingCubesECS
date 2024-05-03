using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Codebase.Features.Core;
using Codebase.StaticData;

namespace Codebase.Features
{
    public sealed class GravitySystem : IEcsRunSystem, IEcsPostRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformRef, GravityAffected>> _gravityFilter = default;
        private readonly EcsCustomInject<GameConfig> _gameConfig = default;

        private TransformAccessArray _transforms;
        private JobHandle _jobHandle;

        public void Run(IEcsSystems systems)
        {
            _transforms = new TransformAccessArray(
                _gravityFilter.Value.GetEntitiesCount());

            foreach(int entity in _gravityFilter.Value)
            {
                _transforms
                    .Add(_gravityFilter.Pools.Inc1
                    .Get(entity).Value);
            }

            GravityJob gravityJob = new()
            {
                Direction = _gameConfig.Value.GravityDirection,
                Speed = _gameConfig.Value.GravitySpeed,
                DeltaTime = Time.deltaTime,
            };

            _jobHandle = gravityJob.Schedule(_transforms);
        }

        public void PostRun(IEcsSystems systems)
        {
            _jobHandle.Complete();
            _transforms.Dispose();
        }
    }
}
