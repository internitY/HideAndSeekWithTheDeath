using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedInteractable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float winDelayTime = 5f;
    [SerializeField]
    private float maxDistance = 1f;

    private AbilityManager manager;
    private UIManager uiManager;
    private Transform playerTrans; 
    private void Start()
    {
        manager = FindObjectOfType<AbilityManager>();
        uiManager = manager.GetComponent<UIManager>();
        playerTrans = FindObjectOfType<PlayerPositionController>().PlayerWaypointMarker;
        manager.winInteractable = this;
    }
    public void Use()
    {
        if(Vector3.Distance(transform.position, playerTrans.position) > maxDistance)
        {
            Debug.LogError("Too far away");
            return;
        }
        uiManager.ChangeText("Stay still you don't want to make a mistake");
        manager.waitForWinState = true;
        StartCoroutine(WinDelay());
    }

    private IEnumerator WinDelay()
    {
        //Block PlayerMovement Cancel with ESC or rightclick?
        yield return new WaitForSeconds(winDelayTime);
        Debug.Log("WINNER");
    }

    public void CancelWaitForWin()
    {
        uiManager.ChangeText("You shall not move while reanimating");
        manager.waitForWinState = false;
        StopCoroutine(WinDelay());
    }

}

    
