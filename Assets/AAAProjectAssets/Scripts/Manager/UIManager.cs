using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextAnim infoText;

    public void ChangeText(string newText)
    {
        infoText.ChangeText(newText);
        infoText.StartFading(5f);
    }

}
