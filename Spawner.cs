using MyBox;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject prefabToSpawn; // The prefab to spawn
    public float spawnFrequency = 1f; // The frequency of spawning

    private Coroutine spawnCoroutine; // Reference to the spawn coroutine

    [ButtonMethod]
    public void StartSpawning() {
        if (spawnCoroutine == null) {
            spawnCoroutine = StartCoroutine(SpawnAtFrequency());
        }
    }

    [ButtonMethod]
    public void StopSpawning() {
        if (spawnCoroutine != null) {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnAtFrequency() {
        while (true) {
            // Instantiate the prefab at the parent object's position
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity, transform);

            // Wait for the defined frequency duration
            yield return new WaitForSeconds(spawnFrequency);
        }
    }
}
