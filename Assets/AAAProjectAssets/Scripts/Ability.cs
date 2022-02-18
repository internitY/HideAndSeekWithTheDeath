using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    [SerializeField]
    private bool isUnlocked;

    private Image image;
    private Image icon;

    [SerializeField]
    private string InteractTag;

    [SerializeField]
    private float speed = 1f;

    private void Start()
    {
        image = GetComponent<Image>();
        icon = transform.GetChild(0).GetComponent<Image>();
        icon.color = isUnlocked ? Color.white : Color.gray;
    }

    public void UseAbility(IInteractable interactable)
    {
        if(image.fillAmount < 1)
        {
            return;
        }
        image.fillAmount = 0;
        interactable.Use();
    }


    public void UnLockAbility(bool unlocked)
    {
        isUnlocked = unlocked;
        icon.color = unlocked ? Color.white : Color.gray;
    }
    private void Update()
    {
        if(isUnlocked)
            image.fillAmount += Time.deltaTime * speed;
    }

    public string GetTag()
    {
        return InteractTag;
    }

    public bool IsUnlocked()
    {
        return isUnlocked;
    }
}
