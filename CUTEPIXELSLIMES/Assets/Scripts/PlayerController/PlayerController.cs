using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControllerNameSpace
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInfo baseStats;
        [SerializeField] InputAction buildToggle;
        [SerializeField] InputAction leftMouseClick;
        [SerializeField] InputAction movement;
        [SerializeField] LayerMask InteractMask;
        private bool buildMode;
        private CharacterController controller => GetComponent<CharacterController>();
        private Mouse mouse => Mouse.current;
        private Vector3 movementDirection;
        public PlayerInfo BaseStats => baseStats;

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
        void ToggleBuildMode(InputAction.CallbackContext callbackContext)
        {
            buildMode = !buildMode;
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
            if (buildMode)
            {
                RayCastFromScreen();
            }
            else
            {
                //hier attack doen
            }
        }
        void RayCastFromScreen()
        {
            Ray _ray = Camera.main.ScreenPointToRay(new Vector3(mouse.position.ReadValue().x, mouse.position.ReadValue().y, 0));
            if (Physics.Raycast(_ray, out RaycastHit _hit, Mathf.Infinity, InteractMask))
            {
                //hier alle possable hits filteren
                BaseTower hitTower = _hit.transform.GetComponentInParent<BaseTower>();
                print(hitTower);
                GameManager.instance.OpenTowerPanel(hitTower);
            }
        }
    }
}
