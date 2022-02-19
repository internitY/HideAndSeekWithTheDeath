using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperSpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject reaperPrefab;

    [SerializeField]
    private Transform[] spawnpoints;

    public void SpawnReaper()
    {
        Debug.LogError("Spawn Reaper here");
    }
}
