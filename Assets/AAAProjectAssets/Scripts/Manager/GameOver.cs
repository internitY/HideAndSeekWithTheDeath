using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MAED.ActionAndStates;

public class GameOver : MonoBehaviour
{
    private UIManager uiManager;

    private PlayerPlugableStateController player;

    private bool isGameovered;

    private void Start()
    {
        uiManager = GetComponent<UIManager>();
        player = GetComponent<AbilityManager>().PlayerController;
    }

    public void TimerOver()
    {
        if (isGameovered)
            return;
        isGameovered = true;
        uiManager.ChangeText("You ran out of Time");

        StartCoroutine(TitleReturnTimer());
    }
    public void DeathTouchOver()
    {
        if (isGameovered)
            return;
        isGameovered = true;

        uiManager.ChangeText("There is no escape from Death");

        StartCoroutine(TitleReturnTimer());
    }

    private IEnumerator TitleReturnTimer()
    {
        player.IsActive = false;

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(0);
    }

}
