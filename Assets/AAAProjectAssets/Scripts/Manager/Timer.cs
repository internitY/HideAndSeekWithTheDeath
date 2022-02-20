using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    float remainingTime = 50f;

    [SerializeField]
    private bool timerIsRunning = true;

    [SerializeField]
    private float deathStartTime = 20;
    private float deathstartsAt;
    private bool deathStarted;

    //[SerializeField]
    

    private GameOver gameOver;
    private ReaperSpawnManager reaperSpawnManager;
    private UIManager uiManager;

    private void Start()
    {
        gameOver = GetComponent<GameOver>();
        uiManager = GetComponent<UIManager>();
        reaperSpawnManager = GetComponent<ReaperSpawnManager>();
        deathstartsAt = remainingTime - deathStartTime;
    }


    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            remainingTime -= Time.deltaTime;

            if((remainingTime < deathstartsAt) && !deathStarted)
            {
                deathStarted = true;
                uiManager.ChangeText("A Reaper appeared");
                reaperSpawnManager.SpawnFirstReaper();
            }

            CheckDisplay();
        }

    }

    private void CheckDisplay()
    {
        if (remainingTime <= 0)
        {
            timeText.text = "GAMEOVER";
            gameOver.TimerOver();
            timerIsRunning = false;
        }
        else
        {
            DisplayTime(remainingTime);
        }
    }

    #region Publics

    public void AddToTimer(float timeToAdd)
    {
        remainingTime += timeToAdd;

        CheckDisplay();
    }

    public void RemoveFromTimer(float timeToRemove)
    {
        remainingTime -= timeToRemove;

        CheckDisplay();
    }

    public void ToggleTimer()
    {
        timerIsRunning = !timerIsRunning;
    }

    #endregion Publics
}
