using Unity.Burst;
using UnityEngine;
using UnityEngine.Jobs;

namespace Codebase.Features.Core
{
    [BurstCompile]
    internal struct GravityJob : IJobParallelForTransform
    {
        public Vector3 Direction;
        public float Speed;
        public float DeltaTime;

        public void Execute(int _, TransformAccess transform)
        {
            transform.position += DeltaTime * Speed * Direction;
        }
    }
}
