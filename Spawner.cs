using System.Collections;
using UnityEngine;

// Spawner class for spawning prefabs at a user-defined frequency.
public class Spawner : MonoBehaviour {
    // Spawning is done by instantiating a prefab at the parent object's position at a defined frequency.
    // Should not be used for spawning a large number of objects at once, as it may cause performance issues.
    // Implement pooling for spawning a large number of objects.

    // The prefab to spawn.
    public GameObject prefabToSpawn;

    // The frequency of spawning.
    public float spawnFrequency = 1f;

    // Reference to the spawn coroutine.
    private Coroutine spawnCoroutine;

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
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity, transform);

            // Wait for the defined frequency duration
            yield return new WaitForSeconds(spawnFrequency);
        }
    }
}
