using System.Collections;
using UnityEditor.Compilation;
using UnityEngine;

// Spawner class for spawning prefabs at a user-defined frequency.
public class Spawner : MonoBehaviour {
    // By default, Spawning is done by instantiating a prefab at the parent object's position at a defined frequency.
    // The spawning functionality is virtual so it can be overriden enable pooling implementation.

    // The prefab to spawn.
    public GameObject PrefabToSpawn;

    // The frequency of spawning.
    public float spawnFrequency = 1f;

    public bool StartSpawningOnStart = false;

    // Reference to the spawn coroutine.
    private Coroutine spawnCoroutine;

    private void Start() {
        if (StartSpawningOnStart) {
            StartSpawning();
        }
    }

    // Starts the spawning process.
    public void StartSpawning() {
        if (spawnCoroutine == null) {
            spawnCoroutine = StartCoroutine(SpawnAtFrequency());
        }
    }

    // Stops the spawning process.
    public void StopSpawning() {
        if (spawnCoroutine != null) {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    // Coroutine that spawns the prefab at the parent object's position at the defined frequency.
    private IEnumerator SpawnAtFrequency() {
        while (true) {
            // Instantiate the prefab at the parent object's position
            Spawn();

            // Wait for the defined frequency duration
            yield return new WaitForSeconds(spawnFrequency);
        }
    }

    // Override this method to implement custom spawning logic. Pooling etc.
    protected virtual void Spawn() {
        Instantiate(PrefabToSpawn, transform.position, transform.rotation, transform);
    }
}
