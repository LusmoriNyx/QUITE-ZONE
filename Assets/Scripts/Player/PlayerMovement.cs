using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;

    private CharacterController _controller;

    private float verticalVelocity = 0f;
    private float gravity = -9.81f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Component();
    }
    void Component()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
        SetCamera();
    }
    private void Movement()
    {
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : moveSpeed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (!_controller.isGrounded)
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity = 0f;
        }

        move.y = verticalVelocity;
        _controller.Move(move * currentSpeed * Time.deltaTime);
    }
    private void SetCamera()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EventBus.Notify("ThirdPersonCameraMode", null);
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            EventBus.Notify("SwitchCameraMode", null);
        }
    }
}
