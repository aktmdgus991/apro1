using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummySpawner : MonoBehaviour
{
    public GameObject dummyPrefab;
    public Transform player;
    public float spawnDistance = 5f;
    public int spawnCount = 7;
    public float spawnInterval = 10f;
    public LayerMask dummyLayer;
    public float dummyRadius = 1f;

    private void Start()
    {
        StartCoroutine(SpawnDummies());
    }

    IEnumerator SpawnDummies()
    {
        while (true)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                Vector3 spawnPosition;
                do
                {
                    spawnPosition = GetRandomSpawnPosition();
                } while (IsPositionOccupied(spawnPosition));

                Instantiate(dummyPrefab, spawnPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized * spawnDistance;
        Vector3 spawnPosition = player.position + new Vector3(randomDirection.x, 0, randomDirection.y);
        return spawnPosition;
    }

    bool IsPositionOccupied(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, dummyRadius, dummyLayer);
        return colliders.Length > 0;
    }
}
