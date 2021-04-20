using UnityEngine;

namespace RPG.Core
{
    class PersistentObjects : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectsPrefab;

        private static bool hasSpawned = false;

        private void Awake()
        {
            if (hasSpawned) return;

            SpawnPersistentObjects();

            hasSpawned = true;
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObjects = Instantiate(persistentObjectsPrefab);
            DontDestroyOnLoad(persistentObjects);
        }
    }
}
