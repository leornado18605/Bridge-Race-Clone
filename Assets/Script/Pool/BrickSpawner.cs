using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : Singleton<BrickSpawner>
{
    [Tooltip("This number will be multiplied to num of players")]
    [SerializeField] int numOfItemsToSpawn = 10;
    [SerializeField] int numOfPlayers = 4;

    [SerializeField] GameObject brickSpawnPoint;

    [Header("Danh sách vùng spawn trên từng sàn")]
    [SerializeField] List<GameObject> spawnAreas;

    [SerializeField] bool isStartArea;
    [SerializeField] LayerMask layerMask;

    private void Start()
    {
        if (isStartArea)
        {
            StartCoroutine(SpawnItemsAtStart(numOfItemsToSpawn, numOfPlayers));
        }
    }

    public IEnumerator SpawnItemsAtStart(int numItemsToSpawn, int numOfPlayers)
    {
        foreach (GameObject area in spawnAreas)
        {
            for (int j = 0; j < numOfPlayers; j++)
            {
                for (int i = 0; i < numItemsToSpawn; i++)
                {
                    Vector3 targetPos = GetValidSpawnPosition(area);
                    var brick = BrickPool.Instance.GetBrick(j, targetPos);
                    brick.transform.parent = brickSpawnPoint.transform;
                }
            }
        }

        yield return null;
    }

    public IEnumerator SpawnItemsAtWill(int numItemsToSpawn, int playerColorIndex, int areaIndex = 0)
    {
        if (areaIndex < 0 || areaIndex >= spawnAreas.Count) yield break;

        GameObject area = spawnAreas[areaIndex];

        for (int i = 0; i < numItemsToSpawn; i++)
        {
            Vector3 targetPos = GetValidSpawnPosition(area);
            var brick = BrickPool.Instance.GetBrick(playerColorIndex, targetPos);
            brick.transform.parent = brickSpawnPoint.transform;
        }

        yield break;
    }

    public void SpawnImmediate(int playerColorIndex, int areaIndex = 0)
    {
        if (areaIndex < 0 || areaIndex >= spawnAreas.Count) return;

        Vector3 targetPos = GetValidSpawnPosition(spawnAreas[areaIndex]);
        var brick = BrickPool.Instance.GetBrick(playerColorIndex, targetPos);
        brick.transform.parent = brickSpawnPoint.transform;
    }

    Vector3 GetValidSpawnPosition(GameObject area)
    {
        var meshRenderer = area.GetComponent<MeshRenderer>();
        Bounds bounds = meshRenderer.bounds;

        Vector3 targetPos = GetRandomPosition(bounds);
        Collider[] colliders = Physics.OverlapSphere(targetPos, 0.5f, layerMask);

        int attempts = 10;
        while (colliders.Length != 0 && attempts > 0)
        {
            targetPos = GetRandomPosition(bounds);
            colliders = Physics.OverlapSphere(targetPos, 0.5f, layerMask);
            attempts--;
        }

        return targetPos;
    }

    Vector3 GetRandomPosition(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            bounds.max.y + 0.16f,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}