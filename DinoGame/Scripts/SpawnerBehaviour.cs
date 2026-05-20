using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _barrierObjects;


    [Header("Spawnings Details")]
    [SerializeField] private float _minSpawnInterval = 2f;   // минимальный 
    [SerializeField] private float _maxSpawnInterval = 4f;   // максимальный

    private float currentSpawnInterval;

    private float timer = 0f;

    private void Start()
    {
        currentSpawnInterval = Random.Range(_minSpawnInterval, _maxSpawnInterval);
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= _maxSpawnInterval)
        {
            SpawnBarrier();
            timer = 0f;
            currentSpawnInterval = Random.Range(_minSpawnInterval, _maxSpawnInterval);
        }
    }

    private void SpawnBarrier()
    {
        int randomIndex = Random.Range(0, _barrierObjects.Length);
        GameObject barrierToSpawn = _barrierObjects[randomIndex];

        Instantiate(barrierToSpawn, transform.position, Quaternion.identity);
    }
}
