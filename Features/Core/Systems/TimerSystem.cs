using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Codebase.Features.Core;

namespace Codebase.Features
{
    public sealed class TimerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Timer>> _timerFilter = default;

        public void Run(IEcsSystems systems)
        {
            foreach(int entity in _timerFilter.Value)
            {
                ref float timer = ref _timerFilter.Pools.Inc1
                    .Get(entity).Value;
                timer -= Time.deltaTime;

                if(timer <= 0)
                    _timerFilter.Pools.Inc1
                        .Del(entity);
            }
        }
    }
}
