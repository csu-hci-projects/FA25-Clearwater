using UnityEngine;

public class EvasiveFishManager : MonoBehaviour
{
    public GameObject evasiveFishPrefab;
    public int evasiveFishCount = 10;

    public NativeFishManager normalFishManager;

    void Start()
    {
        if (evasiveFishPrefab == null ||
            normalFishManager == null ||
            normalFishManager.lakeBounds == null)
        {
            Debug.LogError("EvasiveFishPrefab, NativeFishManager, or LakeBounds NOT assigned!");
            return;
        }

        for (int i = 0; i < evasiveFishCount; i++)
        {
            SpawnEvasiveFish();
        }
    }

    void SpawnEvasiveFish()
    {
        Bounds b = normalFishManager.lakeBounds.bounds;

        Vector3 pos = new Vector3(
            Random.Range(b.min.x, b.max.x),
            Random.Range(b.min.y, b.max.y),
            Random.Range(b.min.z, b.max.z)
        );

        Instantiate(evasiveFishPrefab, pos, Quaternion.identity);
    }
}
