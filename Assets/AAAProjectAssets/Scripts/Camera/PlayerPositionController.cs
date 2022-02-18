using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPositionController : MonoBehaviour
{
    private PlayerControls playerInput;
    private PlayerController playerController;

    [Header("Player Input")] 
    [SerializeField] private LayerMask worldPositionMask;
    [SerializeField, ShowOnly] private Vector2 mousePosition;

    private Ray mouseRaycast;
    private RaycastHit mouseRaycastHit;

    #region unity
    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();

        if (playerInput == null)
            playerInput = new PlayerControls();
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.MousePosition.performed += context => mousePosition = context.ReadValue<Vector2>();
        playerInput.Player.Primary.performed += context => OnPrimaryStarted();
        playerInput.Player.Secondary.performed += context => OnSecondaryStarted();
        playerInput.Player.ESC.performed += context => OnEscStarted();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Update()
    {
        //mousePosition = Mouse.current.position.ReadValue();
    }
    #endregion unity

    #region input events
    private void OnPrimaryStarted()
    {
        playerController.SetPlayerDestination(GetWorldPoint());
    }
    private void OnSecondaryStarted()
    {

    }
    private void OnEscStarted()
    {

    }

    #endregion input events

    #region world interaction
    private Vector3 GetWorldPoint()
    {
        mouseRaycast = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(mouseRaycast, out mouseRaycastHit, 100f, worldPositionMask))
        {
            return mouseRaycastHit.point;
        }

        Debug.LogWarning("Not hitted ground with mosue raycast.");
        return mouseRaycast.GetPoint(20f);
    }


    #endregion world interaction
}
