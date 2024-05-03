using UnityEngine;
using Codebase.Logic;
using Leopotam.EcsLite;
using Codebase.Features.Core;

namespace Codebase.Infrastructure
{
    public class GameFactory
    {
        private readonly EcsWorld _world;
        private readonly EcsPool<TransformRef> _transformPool;
        private readonly EcsPool<MeshRenderRef> _rendererPool;
        private readonly EcsPool<GravityAffected> _gravityPool;
        private readonly Cube _prefab;
        private readonly Transform _parent;

        private Cube _cube;

        public GameFactory(EcsWorld world, Cube prefab) 
        {
            _world = world;
            _transformPool = _world.GetPool<TransformRef>();
            _rendererPool = _world.GetPool<MeshRenderRef>();
            _gravityPool = _world.GetPool<GravityAffected>();
            _prefab = prefab;

            string parentName = "Cubes";
            _parent = new GameObject(parentName).transform;
        }

        public Cube CreateCube(Vector3 position)
        {
            _cube = GameObject.Instantiate(_prefab, position, Quaternion.identity, _parent);
            CreateEntity(_cube);

            return _cube;
        }

        private void CreateEntity(Cube cube)
        {
            int newEntity = _world.NewEntity();
            cube.SetEntity(_world.PackEntityWithWorld(newEntity));

            _transformPool.Add(newEntity).Value = cube.transform;
            _rendererPool.Add(newEntity).Value = cube.Renderer;
            _gravityPool.Add(newEntity);
        }
    }
}
