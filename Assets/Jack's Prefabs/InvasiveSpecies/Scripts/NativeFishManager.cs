using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeFishManager : MonoBehaviour
{
    public GameObject fishPrefab;
    public int maxFish = 100;
    public float spawnInterval = 1f;
    public BoxCollider lakeBounds;   // Your invisible lake volume

    private List<GameObject> fishList = new List<GameObject>();

    void Start()
    {
        if (fishPrefab == null || lakeBounds == null)
        {
            Debug.LogError("FishPrefab or LakeBounds NOT assigned!");
            return;
        }

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            fishList.RemoveAll(f => f == null);

            if (fishList.Count < maxFish)
                SpawnFish();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnFish()
    {
        Bounds b = lakeBounds.bounds;

        Vector3 pos = new Vector3(
            Random.Range(b.min.x, b.max.x),
            Random.Range(b.min.y, b.max.y),
            Random.Range(b.min.z, b.max.z)
        );

        GameObject f = Instantiate(fishPrefab, pos, Quaternion.identity);
        fishList.Add(f);
    }
}

