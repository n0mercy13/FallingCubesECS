using System;
using UnityEngine;

namespace Codebase.Logic
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private Transform _halfExtentPoint;

        private Collider[] _results;
        private Cube _cube;
        private Vector3 _halfExtent;
        private Vector3 _center;

        private void OnValidate()
        {
            if (_halfExtentPoint == null)
                throw new ArgumentNullException(nameof(_halfExtentPoint));
        }

        private void Start()
        {
            _center = transform.position;
            _halfExtent = _halfExtentPoint.position;

            int maxCubesToDetect = 1000;
            _results = new Collider[maxCubesToDetect];
        }

        private void Update()
        {
            CheckForCubesCollision();
        }

        private void CheckForCubesCollision()
        {
            int hits = Physics.OverlapBoxNonAlloc(_center, _halfExtent, _results);

            if (hits <= 0)
                return;

            for(int i = 0; i < hits; i++)
            {
                if (_results[i].gameObject.TryGetComponent(out _cube)
                    && _cube.IsCollided == false)
                    _cube.PlatformCollided();
            }
        }
    }
}
