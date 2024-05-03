using Codebase.Features.Core;
using Codebase.StaticData;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Codebase.Features
{
    public sealed class DisableAfterDelaySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DisableAfterDelay, TransformRef, MeshRenderRef>, Exc<Timer>> _disableFilter = default;
        private readonly EcsPoolInject<DisabledState> _disabledStatePool = default;
        private readonly EcsCustomInject<GameConfig> _gameConfig = default;

        public void Run(IEcsSystems systems)
        {
            foreach(int entity in _disableFilter.Value)
            {
                _disableFilter.Pools.Inc1.Del(entity);

                _disableFilter.Pools.Inc2.Get(entity).Value.gameObject.SetActive(false);
                _disableFilter.Pools.Inc3.Get(entity).Value.material.color = _gameConfig.Value.CubeDefaultColor;

                _disabledStatePool.Value.Add(entity);
            }
        }
    }
}
