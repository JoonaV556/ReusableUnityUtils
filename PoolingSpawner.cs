using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Sub-type of Spawner.cs which utilizes object pooling to spawn objects instead of instantiating and destroying objects
/// </summary>
public class PoolingSpawner : Spawner {
    // Sub class of basic spawner, 
    // Utilizes object pooling to kill and spawn objects instead of instiantiating new ones 

    [Tooltip("Should the pooled spawner instantiate the first items before the game starts?")]
    public bool SpawnInitialItemsOnAwake = false;

    [Tooltip("If items are spawned on awake, how many to spawn?")]
    public int DefaultItemCount = 50;

    [Tooltip("Max item count of the pool. If items are returned to pool after the pool item count exceeds this number, the returned items are destroyed.")]
    public int MaxItemCount = 300;

    // Public getter for the object pool
    public ObjectPool<GameObject> Pool {
        get {
            return pool;
        }
    }

    protected ObjectPool<GameObject> pool;

    // For debugging
    private int objecsInPool;
    private int inactiveObjectsInPool;
    private int activeObjectsInPool;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// Here we initialize our pool and optionally spawn initial items.
    /// </summary>
    private void Awake() {
        // Create new pool
        pool = new ObjectPool<GameObject>(CreatePooledObject, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, DefaultItemCount, MaxItemCount);

        // Spawn initial items and add them to the pool
        if (SpawnInitialItemsOnAwake) {

            // Get initial objects from pool
            List<GameObject> InitialObjects = new();
            for (int i = 0; i < DefaultItemCount; i++) {

                GameObject newObject = pool.Get();
                InitialObjects.Add(newObject);
            }

            // After initial objects are created, release them to the pool so they can be used in runtime
            foreach (GameObject newObject in InitialObjects) {
                pool.Release(newObject);
            }
        }
    }

    /// <summary>
    /// Spawns new objects by getting an inactive object from the pool.
    /// </summary>
    protected override void Spawn() {
        // Spawn new object using the existing pool
        // Pool.get() returns inactive, free to use gameobject instead of instantiating new one
        GameObject newObject = pool.Get();
    }

    /// <summary>
    /// Creates new pooled objects and adds them to the pool.
    /// </summary>
    GameObject CreatePooledObject() {
        GameObject newItem = Instantiate(PrefabToSpawn);
        newItem.SetActive(false);
        return newItem;
    }

    /// <summary>
    /// Called when an object is taken from the pool.
    /// Activates the object and sets its position and rotation.
    /// </summary>
    void OnTakeFromPool(GameObject obj) {
        obj.SetActive(true);
        // Spawn object on the spawner location and rotation
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        objecsInPool = pool.CountAll;
    }

    /// <summary>
    /// Called when an object is returned to the pool.
    /// Deactivates the object.
    /// </summary>
    void OnReturnedToPool(GameObject obj) {
        obj.SetActive(false);
    }

    /// <summary>
    /// Called when a pool object is destroyed.
    /// Destroys the GameObject.
    /// </summary>
    void OnDestroyPoolObject(GameObject obj) {
        Destroy(obj);
    }

    /// <summary>
    /// Update is called once per frame.
    /// Here we update our debugging values.
    /// </summary>
    private void Update() {
        // Uppdate debugging values
        objecsInPool = pool.CountAll;
        inactiveObjectsInPool = pool.CountInactive;
        activeObjectsInPool = pool.CountActive;
    }

}
