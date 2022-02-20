using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartScreenUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI creditsText;
    bool active;


    public void StartButton()
    {
        Debug.Log("Load Sceen");
        SceneManager.LoadSceneAsync(1);
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
