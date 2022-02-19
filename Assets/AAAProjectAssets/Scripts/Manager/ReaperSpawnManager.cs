using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperSpawnManager : MonoBehaviour
{
    [SerializeField][Tooltip("DebugOnly")]
    private bool disableSpawn;

    [SerializeField]
    private GameObject reaperPrefab;

    [SerializeField]
    private Transform[] spawnpoints;

    [SerializeField]
    private Transform firstSpawnPoint;

    public void SpawnFirstReaper()
    {
        if (disableSpawn)
            return;
        Instantiate(reaperPrefab, firstSpawnPoint.position, Quaternion.identity);
    }

    public void SpawnReaper()
    {
        if (disableSpawn)
            return;
        Instantiate(reaperPrefab, spawnpoints[(int)Random.Range(0,spawnpoints.Length)].position, Quaternion.identity);
    }


}
