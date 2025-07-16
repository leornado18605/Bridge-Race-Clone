using System.Collections.Generic;
using UnityEngine;

public class BrickPool : Singleton<BrickPool>
{
    [Header("Brick Prefabs follow the color")]
    [SerializeField] private GameObject[] brickPrefabs;

    [SerializeField] private int poolSizePerColor = 50;

    private Dictionary<int, Queue<GameObject>> brickPools = new Dictionary<int, Queue<GameObject>>();

    protected override void Awake()
    {
        base.Awake();
        InitializeBrickPools();
    }

    private void InitializeBrickPools()
    {
        for (int i = 0; i < brickPrefabs.Length; i++)
        {
            brickPools[i] = new Queue<GameObject>();

            for (int j = 0; j < poolSizePerColor; j++)
            {
                GameObject brick = Instantiate(brickPrefabs[i]);
                brickPools[i].Enqueue(brick);
                brick.SetActive(false);
            }
        }
    }

    //Take the Brick from Pool or new create
    public GameObject GetBrick(int colorIndex, Vector3 spawnPos)
    {
        GameObject brick;

        if (brickPools[colorIndex].Count > 0)
        {
            brick = brickPools[colorIndex].Dequeue();
        }

        else
        {
            brick = Instantiate(brickPrefabs[colorIndex]);
            brick.SetActive(true);
        }

        brick.transform.position = spawnPos;
        brick.transform.rotation = Quaternion.identity;
        brick.SetActive(true);
        return brick;
    }

    public void ReturnBrick(int colorIndex, GameObject brick)
    {
        brick.SetActive(false);
        brickPools[colorIndex].Enqueue(brick);
    }
}
