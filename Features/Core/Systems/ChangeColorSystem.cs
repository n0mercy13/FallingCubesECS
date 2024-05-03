using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Codebase.Features.Core;
using Codebase.Infrastructure;

namespace Codebase.Features
{
    public sealed class ChangeColorSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MeshRenderRef, ChangeColorRequest>> _requestFilter = default;
        private readonly EcsCustomInject<RandomService> _randomService = default;

        public void Run(IEcsSystems systems)
        {
            foreach(int entity in _requestFilter.Value)
            {
                _requestFilter.Pools.Inc1.Get(entity).Value.material.color = _randomService.Value.GetColor();

                _requestFilter.Pools.Inc2.Del(entity);
            }
        }
    }
}
