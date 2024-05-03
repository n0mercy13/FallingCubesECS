using System;
using UnityEngine;

namespace Codebase.StaticData
{
    public class SceneData : MonoBehaviour
    {
        [field: SerializeField] public Transform SpawnPointMin { get; private set; }
        [field: SerializeField] public Transform SpawnPointMax { get; private set; }

        private void OnValidate()
        {
            if (SpawnPointMin == null)
                throw new ArgumentNullException(nameof(SpawnPointMin));

            if(SpawnPointMax == null)
                throw new ArgumentNullException(nameof(SpawnPointMax));
        }
    }
}
