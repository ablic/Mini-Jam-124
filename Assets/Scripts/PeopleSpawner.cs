using System.Collections;
using UnityEngine;

public class PeopleSpawner : MonoBehaviour
{
    [SerializeField] private GameConfig config;
    [SerializeField] private Passerby[] peoplePrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform parent;

    private Coroutine spawnCoroutine;

    private void Start()
    {
        StartSpawning();
    }

    private void StartSpawning()
    {
        if (spawnCoroutine == null)
            spawnCoroutine = StartCoroutine(Spawn());
    }

    private void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    public IEnumerator Spawn()
    {
        while (true)
        {
            Passerby passerbyPrefab = peoplePrefabs[Random.Range(0, peoplePrefabs.Length)];
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            Passerby passerby = Instantiate(passerbyPrefab, parent);
            passerby.transform.position = spawnPoint.position;
            passerby.Direction = Vector2.left * Mathf.Sign(spawnPoint.position.x);
            passerby.Speed = Random.Range(
                config.Passerby.MinSpeed,
                config.Passerby.MaxSpeed);

            yield return new WaitForSeconds(Random.Range(
                config.Passerby.MinSpawnPeriod,
                config.Passerby.MaxSpawnPeriod));
        }
    }
}
