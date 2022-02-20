using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MAED.ActionAndStates;

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

    [SerializeField]
    private float spawnDelay = 3f;

    public void SpawnFirstReaper()
    {
        if (disableSpawn)
            return;
        StartCoroutine(SpawnReaperRoutine(firstSpawnPoint.position));

    }

    public void SpawnReaper()
    {
        if (disableSpawn)
            return;
        StartCoroutine(SpawnReaperRoutine(spawnpoints[(int)Random.Range(0, spawnpoints.Length)].position));
        //GameObject reaper = Instantiate(reaperPrefab, spawnpoints[(int)Random.Range(0,spawnpoints.Length)].position, Quaternion.identity);
    }

    private IEnumerator SpawnReaperRoutine(Vector3 position)
    {
        GameObject reaper = Instantiate(reaperPrefab, position, Quaternion.identity);
        DeathPlugableStateController controller = reaper.GetComponent<DeathPlugableStateController>();
        controller.IsActive = false;

        yield return new WaitForSeconds(spawnDelay);

        //Disable VFX
        //Debug.LogError("Disable Spawn VFX");

        controller.IsActive = true;

    }
    

}
