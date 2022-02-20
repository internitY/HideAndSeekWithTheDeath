using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableManager : MonoBehaviour
{
    public static CollectableManager Instance;

    [SerializeField]
    private TextMeshProUGUI collectText;

    [SerializeField]
    private string collectableName = "Shards";

    [SerializeField]
    private int collectCount = 0;

    [SerializeField]
    private int[] unlockAtCount;

    private AbilityManager abilityManager;
    private ReaperSpawnManager reaperSpawnManager;


    [SerializeField]
    private int reaperSpawnAt = 2;
    private int currentReaperSpawnCount;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
        }
    }


    private void Start()
    {
        if (collectText != null)
        {
            //collectText.text = collectableName + ": " + collectCount;
            collectText.text = "" + collectCount;
        }
        abilityManager = GetComponent<AbilityManager>();
        reaperSpawnManager = GetComponent<ReaperSpawnManager>();

        currentReaperSpawnCount = reaperSpawnAt;
    }



    public void Collect()
    {
        collectCount++;

        if(collectText != null)
        {
            //collectText.text = collectableName+": " + collectCount;
            collectText.text = "" + collectCount;
        }

        for (int i = 0; i < unlockAtCount.Length; i++)
        {
            if(collectCount >= unlockAtCount[i])
            {
                UnlockAbility(i);
            }
        }
        if(collectCount == currentReaperSpawnCount)
        {
            reaperSpawnManager.SpawnReaper();
            currentReaperSpawnCount += reaperSpawnAt;
        }

    }

    private void UnlockAbility(int index)
    {
        abilityManager.UnlockAbility(index);
    }


}
