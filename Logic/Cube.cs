using System;
using UnityEngine;
using Leopotam.EcsLite;
using Codebase.Features.Core;

namespace Codebase.Logic
{
    public class Cube : MonoBehaviour
    {
        [field: SerializeField] public MeshRenderer Renderer { get; private set; }

        private EcsPackedEntityWithWorld _packedEntity;

        public bool IsCollided { get; private set; }

        private void OnValidate()
        {
            if(Renderer == null)
                throw new ArgumentNullException(nameof(Renderer));
        }

        private void OnEnable()
        {
            IsCollided = false;
        }

        public void PlatformCollided()
        {
            IsCollided = true;

            if(_packedEntity.Unpack(out EcsWorld world, out int entity))
            {
                if(world.GetPool<GravityAffected>().Has(entity))
                    world.GetPool<GravityAffected>().Del(entity);

                world.GetPool<ChangeColorRequest>().Add(entity);
                world.GetPool<DisableAfterDelayRequest>().Add(entity);
            }
        }
        
        public void SetEntity(EcsPackedEntityWithWorld ecsPackedEntityWithWorld)
        {
            _packedEntity = ecsPackedEntityWithWorld;
        }
    }
}
