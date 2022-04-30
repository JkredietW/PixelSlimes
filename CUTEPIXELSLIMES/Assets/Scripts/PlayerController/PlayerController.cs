using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance => FindObjectOfType<PlayerController>();
    [SerializeField] private PlayerInfo baseStats;
    [SerializeField] InputAction buildToggle;
    [SerializeField] InputAction leftMouseClick;
    [SerializeField] InputAction movement;
    [SerializeField] LayerMask towerMask;
    [SerializeField] LayerMask groundMask;
    private SelectionState selectionState;
    private bool buildMode;
    private CharacterController controller => GetComponent<CharacterController>();
    private Mouse mouse => Mouse.current;
    private Vector3 movementDirection;
    public PlayerInfo BaseStats => baseStats;
    public SelectionState SelectionState => selectionState;


    private void Awake()
    {
        buildToggle.performed += ToggleBuildMode;
        leftMouseClick.performed += LeftMouseClick;
        movement.performed += Movement;
    }
    private void OnEnable()
    {
        buildToggle.Enable();
        leftMouseClick.Enable();
        movement.Enable();
    }
    private void OnDisable()
    {
        buildToggle.Disable();
        leftMouseClick.Disable();
        movement.Disable();
    }
    private void LateUpdate()
    {
        MoveCharacter();
    }
    public void SetState(SelectionState newState)
    {
        selectionState = newState;
    }
    void ToggleBuildMode(InputAction.CallbackContext callbackContext)
    {
        ToggleBuildModeNonButton();
    }
    public void ToggleBuildModeNonButton()
    {
        if (selectionState == SelectionState.inTowerUi)
            return;
        buildMode = !buildMode;
        selectionState = buildMode ? SelectionState.InUi : SelectionState.Default;
        GameManager.instance.ToggleTowerShop(buildMode);
    }
    public void ToDefaultState()
    {
        buildMode = false;
        selectionState = buildMode ? SelectionState.InUi : SelectionState.Default;
        GameManager.instance.ToggleTowerShop(buildMode);
    }
    void Movement(InputAction.CallbackContext callbackContext)
    {
        Vector2 moveInput = callbackContext.ReadValue<Vector2>();
        movementDirection.x = moveInput.x;
        movementDirection.z = moveInput.y;
    }
    void MoveCharacter()
    {
        if (movement.ReadValue<Vector2>().magnitude == 0)
        {
            movementDirection = Vector3.zero;
        }

        controller.Move(movementDirection.normalized * Time.deltaTime * baseStats.BaseMovementSpeed);
    }
    void LeftMouseClick(InputAction.CallbackContext callbackContext)
    {
        RayCastFromScreen();
    }
    public Ray RayFromMouse()
    {
        return Camera.main.ScreenPointToRay(new Vector3(mouse.position.ReadValue().x, mouse.position.ReadValue().y, 0));
    }
    void RayCastFromScreen() //ook nog een ghost wall/tower doen
    {
        Ray _ray = RayFromMouse();
        switch (selectionState)
        {
            case SelectionState.Default:
                if (Physics.Raycast(_ray, out RaycastHit _hit, Mathf.Infinity, towerMask))
                {
                    MonoBehaviour hitTower = _hit.transform.GetComponentInParent<MonoBehaviour>();
                    selectionState = SelectionState.InUi;

                    //hier filteren
                    if (hitTower.GetComponent<BaseTower>())
                    {
                        selectionState = SelectionState.inTowerUi;
                        GameManager.instance.OpenTowerPanel(hitTower as BaseTower);
                    }
                    else
                    {
                        //als niks is dan niet uimode
                        selectionState = SelectionState.Default;
                    }
                }
                break;
            case SelectionState.InUi:
            case SelectionState.inTowerUi:
                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    GameManager.instance.CloseTowerPanel();
                    ToDefaultState();
                }
                break;
            case SelectionState.buildMode:
                if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, groundMask))
                {
                    //check als tower hier mag plaatsen
                    Vector3 newLocation = Grid.instance.GetClossedPoint(_hit.point);

                    PlaceItemBase.instance.PlaceTower();
                    ToggleBuildModeNonButton();
                    GameManager.instance.ToggleTowerShop(false);
                }
                break;
        }
    }
}
public enum SelectionState
{ 
    Default,
    InUi,
    inTowerUi,
    buildMode,
}
