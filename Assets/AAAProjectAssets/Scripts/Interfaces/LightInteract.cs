using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteract : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Light pointLight;

    public void Use()
    {
        pointLight.color = Color.red;
    }
}
