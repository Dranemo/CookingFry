using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] InputActionReference movement;
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 12f;

    Vector2 movementInput;


    private void OnEnable()
    {
        movement.action.Enable();
    }
    private void OnDisable()
    {
        movement.action.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        movementInput = movement.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);

        move = transform.TransformDirection(move);

        controller.SimpleMove(move * speed);
    }
}
