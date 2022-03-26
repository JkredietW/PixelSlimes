using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerController
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        private CharacterController controller => GetComponent<CharacterController>();
        private Mouse mouse => Mouse.current;
        private Keyboard keyboard => Keyboard.current;
        private Vector3 movementDirection;


        private void Update()
        {
            MovementInput();
        }
        private void LateUpdate()
        {
            MoveCharacter();
        }
        void MovementInput()
        {
            movementDirection = new Vector3(keyboard.dKey.ReadValue() - keyboard.aKey.ReadValue(), 0, keyboard.wKey.ReadValue() - keyboard.sKey.ReadValue());
        }
        void MoveCharacter()
        {
            controller.Move(movementDirection.normalized * Time.deltaTime * speed);
        }
    }
}
