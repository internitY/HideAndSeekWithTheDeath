using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MAED.ActionAndStates;

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

    [SerializeField]
    private PlugableStateController playerController;

    [SerializeField]
    private float sprintSpeed = 10f;
    [SerializeField]
    private float sprintDuration = 2f;

    private float oldSpeed;

    private int activeAbility;

    public bool waitForWinState;

    public BedInteractable winInteractable;

    private void Awake()
    {
        if (playerInput == null)
            playerInput = new PlayerControls();

    }

    private void Start()
    {
        oldSpeed = playerController.RichAI.maxSpeed;
    }
    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.MousePosition.performed += context => mousePosition = context.ReadValue<Vector2>();
        playerInput.Player.Ability1.performed += context => CheckAbility(0);
        playerInput.Player.Ability2.performed += context => SprintAbility();//CheckAbility(1);
        playerInput.Player.Ability3.performed += context => CheckAbility(2);
        playerInput.Player.Ability4.performed += context => CheckAbility(3);
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
        if (waitForWinState)
        {
            winInteractable.CancelWaitForWin();
        }

        if (activeAbility == -1)
            return;
        //Click on Object of current Ability
        IInteractable interactable = GetWorldPointInteractable(abilities[activeAbility].GetTag());
        //Debug.Log(interactable);
        if(interactable != null)
            abilities[activeAbility].UseAbility(interactable);

        foreach (var item in abilities)
        {
            item.InActiveState();
        }

    }


    //Rightclick and Esc
    private void OnEscStarted()
    {
        //CancelAbility use
        activeAbility = -1;
        if (waitForWinState)
        {
            winInteractable.CancelWaitForWin();
        }


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
            if (mouseRaycastHit.collider.CompareTag(tagerino))
            {
                return mouseRaycastHit.collider.gameObject.GetComponent<IInteractable>();
            }
            else
                Debug.Log("Nope");
        }
        
        return null;
    }

    private void CheckAbility(int index)
    {
        if (waitForWinState)
        {
            winInteractable.CancelWaitForWin();
        }

        foreach (var item in abilities)
        {
            item.InActiveState();
        }
        if (abilities[index].IsUnlocked())
        {
            activeAbility = index;
            abilities[index].ActiveState();
            Debug.Log("Active Ability: " + index);
        }
        else
        {
            index = -1;
        }
    }
    private void SprintAbility()
    {
        if(abilities[1].IsUnlocked() && !abilities[1].OnCooldown())
        {
            abilities[1].ActiveState();
            abilities[1].Sprint();
            StartCoroutine(Sprint());
            activeAbility = -1;
        }
        
    }

    private IEnumerator Sprint()
    {
        playerController.RichAI.maxSpeed = sprintSpeed;
        yield return new WaitForSeconds(sprintDuration);
        playerController.RichAI.maxSpeed = oldSpeed;
        abilities[1].InActiveState();
    }
}
