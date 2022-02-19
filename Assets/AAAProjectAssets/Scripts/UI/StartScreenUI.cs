using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartScreenUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI creditsText;
    bool active;

    public void StartButton()
    {
        Debug.Log("Load Sceen");
    }

    public void CreditsButton()
    {
        active = !active;
        creditsText.gameObject.SetActive(active);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
