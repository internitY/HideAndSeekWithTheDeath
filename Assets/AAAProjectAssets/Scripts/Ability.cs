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

    private bool multiTimeAbility;

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
        if(isUnlocked && !multiTimeAbility)
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
    public void ActiveState()
    {
        icon.color = Color.cyan;
    }
    public void InActiveState()
    {
        icon.color = isUnlocked ? Color.white : Color.gray;
        if (multiTimeAbility)
            multiTimeAbility = false;
    }
    //Stuff for sprint only....
    public bool OnCooldown()
    {
        if (image.fillAmount < 1)
        {
            return true;
        }
        return false;
    }
    public void Sprint()
    {
        image.fillAmount = 0;
        multiTimeAbility = true;
    }
}
