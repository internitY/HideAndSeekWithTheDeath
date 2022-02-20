using System.Collections;
using System.Collections.Generic;
using MAED.ActionAndStates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedInteractable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float winDelayTime = 5f;
    [SerializeField]
    private float maxDistance = 1f;

    private AbilityManager manager;
    private UIManager uiManager;
    private PlayerPlugableStateController player; 
    private void Start()
    {
        manager = FindObjectOfType<AbilityManager>();
        uiManager = manager.GetComponent<UIManager>();
        player = FindObjectOfType<PlayerPlugableStateController>();
        manager.winInteractable = this;
    }
    public void Use()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > maxDistance)
        {
            Debug.LogWarning("Too far away");
            return;
        }
        uiManager.ChangeText("Stay still you don't want to make a mistake");
        manager.waitForWinState = true;
        StartCoroutine(WinDelay());
    }

    private IEnumerator WinDelay()
    {
        player.IsHiding = true;
        //Block PlayerMovement Cancel with ESC or rightclick?
        yield return new WaitForSeconds(winDelayTime);
        uiManager.ChangeText("You survived Death");
        SceneManager.LoadScene(2);
        player.IsHiding = false;
    }

    public void CancelWaitForWin()
    {
        uiManager.ChangeText("You shall not move while reanimating");
        manager.waitForWinState = false;
        StopCoroutine(WinDelay());
    }

}

    
