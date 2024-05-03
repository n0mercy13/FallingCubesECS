using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Codebase.Features.Core;
using Codebase.Infrastructure;
using Codebase.StaticData;

namespace Codebase.Features
{
    public sealed class SetDisableDelaySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DisableAfterDelayRequest>> _requestFilter = default;
        private readonly EcsPoolInject<DisableAfterDelay> _disableStatePool = default;
        private readonly EcsPoolInject<Timer> _timerPool = default;
        private readonly EcsCustomInject<RandomService> _randomService = default;
        private readonly EcsCustomInject<GameConfig> _gameConfig = default;

        public void Run(IEcsSystems systems)
        {
            foreach(int entity in _requestFilter.Value)
            {
                float delay = _randomService.Value
                    .Range(_gameConfig.Value.CubeDisableDelayMin, _gameConfig.Value.CubeDisableDelayMax);

                _disableStatePool.Value.Add(entity);
                _timerPool.Value.Add(entity).Value = delay;

                _requestFilter.Pools.Inc1.Del(entity);
            }
        }
    }
}
