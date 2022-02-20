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

    [SerializeField]
    private bool reverseTimer;
    [SerializeField]
    private bool spawnReaperWithTime;
    [SerializeField]
    private float reaperSpawnTime = 30f;
    private float reaperSpawnsAt;

    //[SerializeField]
    

    private GameOver gameOver;
    private ReaperSpawnManager reaperSpawnManager;
    private UIManager uiManager;

    private void Start()
    {
        gameOver = GetComponent<GameOver>();
        uiManager = GetComponent<UIManager>();
        reaperSpawnManager = GetComponent<ReaperSpawnManager>();
        reaperSpawnsAt = reaperSpawnTime;
        if (reverseTimer)
        {
            remainingTime = 0;
            deathstartsAt = deathStartTime;
        }
        else
        {
            deathstartsAt = remainingTime - deathStartTime;
        }
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
            if (reverseTimer)
            {
                remainingTime += Time.deltaTime;

                if ((remainingTime > deathstartsAt) && !deathStarted)
                {
                    deathStarted = true;
                    uiManager.ChangeText("A Reaper appeared");
                    reaperSpawnManager.SpawnFirstReaper();
                }
                if(remainingTime > reaperSpawnsAt && spawnReaperWithTime)
                {
                    uiManager.ChangeText("A Reaper appeared");
                    reaperSpawnManager.SpawnReaper();
                    reaperSpawnsAt = remainingTime + reaperSpawnTime; 
                }
            }
            else
            {
                remainingTime -= Time.deltaTime;

                if((remainingTime < deathstartsAt) && !deathStarted)
                {
                    deathStarted = true;
                    uiManager.ChangeText("A Reaper appeared");
                    reaperSpawnManager.SpawnFirstReaper();
                }
                if (remainingTime < reaperSpawnsAt && spawnReaperWithTime)
                {
                    uiManager.ChangeText("A Reaper appeared");
                    reaperSpawnManager.SpawnReaper();
                    reaperSpawnsAt = remainingTime - reaperSpawnTime;
                }

            }

            CheckDisplay();
        }

    }

    private void CheckDisplay()
    {
        if(timeText == null)
        {
            return;
        }
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
