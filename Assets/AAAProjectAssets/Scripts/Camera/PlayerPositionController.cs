using MAED.ActionAndStates;
using UnityEngine;

public class PlayerPositionController : MonoBehaviour
{
    private PlayerControls playerInput;

    [Header("Player Input")] 
    [SerializeField] private LayerMask worldPositionMask;
    [SerializeField, ShowOnly] private Vector2 mousePosition;

    [Header("Player References")]
    [SerializeField] private PlugableStateController playerController;
    [SerializeField] private State playerMoveState;
    [SerializeField] private Transform playerWaypointMarker;

    private Ray mouseRaycast;
    private RaycastHit mouseRaycastHit;

    #region getter
    public Transform PlayerWaypointMarker => playerWaypointMarker;
    

    #endregion getter

    #region unity
    private void Awake()
    {
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
    #endregion unity

    #region input events
    private void OnPrimaryStarted()
    {
        
    }
    private void OnSecondaryStarted()
    {
        playerWaypointMarker.position = GetWorldPoint();
        playerController.SetDestination(playerWaypointMarker.position);
    }
    private void OnEscStarted()
    {

    }

    #endregion input events

    #region world interaction
    private Vector3 GetWorldPoint()
    {
        mouseRaycast = Camera.main.ScreenPointToRay(mousePosition);
        Vector3 newWorldPoint = Vector3.zero;

        if (Physics.Raycast(mouseRaycast, out mouseRaycastHit, 50f, worldPositionMask))
        {
            newWorldPoint = mouseRaycastHit.point;
            newWorldPoint.y = 0;
            return newWorldPoint;
        }

        Debug.LogWarning("Not hitted ground with mosue raycast.");
        newWorldPoint = mouseRaycast.GetPoint(25);
        newWorldPoint.y = 0;
        return newWorldPoint;
    }


    #endregion world interaction
}
