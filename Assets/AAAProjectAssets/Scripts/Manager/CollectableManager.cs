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
            collectText.text = collectableName + ": " + collectCount;
        abilityManager = GetComponent<AbilityManager>();
    }



    public void Collect()
    {
        collectCount++;

        if(collectText != null)
            collectText.text = collectableName+": " + collectCount;

        for (int i = 0; i < unlockAtCount.Length; i++)
        {
            if(collectCount >= unlockAtCount[i])
            {

                UnlockAbility(i);
            }
        }

    }

    private void UnlockAbility(int index)
    {
        abilityManager.UnlockAbility(index);
    }

}
