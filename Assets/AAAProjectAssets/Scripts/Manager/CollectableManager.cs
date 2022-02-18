using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public static CollectableManager Instance;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
        }
    }


    [SerializeField]
    private int collectCount = 0;

    [SerializeField]
    private int[] unlockAtCount;
    



    
    public void Collect()
    {
        collectCount++;

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
        Debug.LogError("ADD AbilitySystem here");
    }

}
