using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] List<SpawnedObjectSO> spawnedObjects;

    private Dictionary<SpawnedObjectSO, List<GameObject>> poolDictionary = new Dictionary<SpawnedObjectSO, List<GameObject>>();

    public static ObjectPoolManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Spawns an object from the pool. If no inactive objects are available, a new one is created.
    /// </summary>
    public GameObject SpawnFromPool(SpawnedObjectSO spawnedObject, Vector3 position, Quaternion rotation)
    {
        // Find the corresponding pool configuration
        var pool = spawnedObjects.Find(p => p == spawnedObject);
        if (pool == null)
        {
            Debug.LogWarning($"Pool with tag '{spawnedObject}' not found!");
            return null;
        }

        // Ensure the pool exists in the dictionary
        if (!poolDictionary.ContainsKey(spawnedObject))
        {
            poolDictionary[spawnedObject] = new List<GameObject>();
        }

        // Try to find an inactive object in the pool
        foreach (var pooledObject in poolDictionary[spawnedObject])
        {
            if (!pooledObject.activeInHierarchy)
            {
                pooledObject.transform.position = position;
                pooledObject.transform.rotation = rotation;
                pooledObject.SetActive(true);

                // Call OnObjectSpawn if the object implements IPooledObject
                pooledObject.GetComponent<IPooledObject>()?.OnObjectSpawn();

                return pooledObject;
            }
        }

        // If no inactive objects are available, instantiate a new one
        GameObject newObject = Instantiate(pool.Prefab, position, rotation);
        poolDictionary[spawnedObject].Add(newObject);

        // Call OnObjectSpawn for the newly created object
        newObject.GetComponent<IPooledObject>()?.OnObjectSpawn();

        return newObject;
    }

    /// <summary>
    /// Returns an object to the pool by deactivating it and resetting its state.
    /// </summary>
    public void ReturnToPool(GameObject obj, SpawnedObjectSO spawnedObject)
    {
        obj.SetActive(false);

        // Call OnObjectReturn if the object implements IPooledObject
        obj.GetComponent<IPooledObject>()?.OnObjectReturn();

        // Ensure the pool exists for the tag
        if (!poolDictionary.ContainsKey(spawnedObject))
        {
            poolDictionary[spawnedObject] = new List<GameObject>();
        }

        if (!poolDictionary[spawnedObject].Contains(obj))
        {
            poolDictionary[spawnedObject].Add(obj);
        }
    }
}
