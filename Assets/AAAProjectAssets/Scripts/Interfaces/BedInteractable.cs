using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedInteractable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float winDelayTime = 5f;

    private AbilityManager manager;
    private void Start()
    {
        manager = FindObjectOfType<AbilityManager>();
        manager.winInteractable = this;
    }
    public void Use()
    {
        
        manager.waitForWinState = true;
        StartCoroutine(WinDelay());
    }

    private IEnumerator WinDelay()
    {
        //Block PlayerMovement Cancel with ESC or rightclick?
        yield return new WaitForSeconds(winDelayTime);
        Debug.Log("WINNER");
    }
}
