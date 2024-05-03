using System;
using UnityEngine;
using Codebase.Logic;

namespace Codebase.StaticData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "StaticData/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public Cube CubePrefab { get; private set; }
        [field: SerializeField, Range(0f, 1f)] public float SpawnDelay { get; private set; } = 0.05f;
        [field: SerializeField] public Vector3 GravityDirection { get; private set; } = Vector3.down;
        [field: SerializeField, Range(0f, 20f)] public float GravitySpeed { get; private set; } = 9.81f;
        [field: SerializeField] public Color CubeDefaultColor { get; private set; } = Color.white;
        [field: SerializeField, Range(0f, 10f)] public float CubeDisableDelayMin { get; private set; } = 2f;
        [field: SerializeField, Range(0f, 10f)] public float CubeDisableDelayMax { get; private set; } = 5f;

        private void OnValidate()
        {
            if (CubePrefab == null)
                throw new ArgumentNullException(nameof(CubePrefab));

            if(CubeDisableDelayMin >= CubeDisableDelayMax)
                throw new InvalidOperationException($"{CubeDisableDelayMin} cannot exceed {CubeDisableDelayMax}.");
        }
    }
}
