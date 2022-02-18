using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    private PlayerControls playerInput;

    [SerializeField] 
    private LayerMask worldPositionMask;
    [SerializeField, ShowOnly] 
    private Vector2 mousePosition;
    private Ray mouseRaycast;
    private RaycastHit mouseRaycastHit;

    [SerializeField]
    private Ability[] abilities;

    private int activeAbility;

    private void Awake()
    {
        if (playerInput == null)
            playerInput = new PlayerControls();
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.MousePosition.performed += context => mousePosition = context.ReadValue<Vector2>();
        //Input 1-4?
        playerInput.Player.Primary.performed += context => OnPrimaryStarted();
        playerInput.Player.Secondary.performed += context => OnEscStarted();
        playerInput.Player.ESC.performed += context => OnEscStarted();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }
    
    #region AbilityUse

    private void OnPrimaryStarted()
    {
        if (activeAbility == -1)
            return;
        //Click on Object of current Ability
        abilities[activeAbility].UseAbility(GetWorldPointInteractable(abilities[activeAbility].GetTag()));

    }


    //Rightclick and Esc
    private void OnEscStarted()
    {
        //CancelAbility use
        activeAbility = -1;
    }

    #endregion AbilityUse

    public void UnlockAbility(int index)
    {
        if (index > abilities.Length)
            return;
        abilities[index].UnLockAbility(true);
    }


    private IInteractable GetWorldPointInteractable(string tagerino)
    {
        mouseRaycast = Camera.main.ScreenPointToRay(mousePosition);
        Vector3 newWorldPoint = Vector3.zero;

        if (Physics.Raycast(mouseRaycast, out mouseRaycastHit, 50f, worldPositionMask))
        {
            if(mouseRaycastHit.collider.CompareTag(tagerino))
                return mouseRaycastHit.collider.gameObject.GetComponent<IInteractable>();
        }
        
        return null;
    }


}
