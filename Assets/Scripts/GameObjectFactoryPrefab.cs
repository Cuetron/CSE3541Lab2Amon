using UnityEngine;

namespace ShareefSoftware
{
    class GameObjectFactoryPrefab : IGameObjectFactory
    {
        public GameObject Prefab { get; set; }
        public Transform Parent { get; set; }

        public GameObject CreateAt(Vector3 position, string name = null)
        {
            var prefab = GameObject.Instantiate<GameObject>(Prefab, position, Quaternion.identity, Parent);
            prefab.name = name ?? Prefab.name;
            return prefab;
        }
    }
}