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

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void Update()
    {
        remainingTime -= Time.deltaTime;

        

        if(remainingTime <= 0)
        {
            timeText.text = "GAMEOVER";
        }
        else
        {
            DisplayTime(remainingTime);
        }
    }
}
