using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextAnim infoText;

    private void Start()
    {
        ChangeText("Collect the Soul Shards to reanimate yourself");
    }

    public void ChangeText(string newText)
    {
        infoText.ChangeText(newText);
        infoText.StartFading(3f);
    }

}
