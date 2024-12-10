using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour {
    public float moveSpeed = 5f; // Speed of movement
    public float jumpForce = 5f; // Force applied when jumping
    private Rigidbody rb; // Rigidbody component

    public Transform playerCamera; // Reference to the player's camera
    public float mouseSensitivity = 200f; // Sensitivity of mouse movement
    private float xRotation = 0f; // Rotation around the X-axis (pitch)

    public void Start() {
        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();

        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void FixedUpdate() {
        // Handle player movement
        MovePlayer();

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Handle camera rotation
        RotateCamera();
    }

    private void MovePlayer() {
        // Get movement input
        float moveHorizontal = Input.GetKey(KeyCode.A) ? -1 : (Input.GetKey(KeyCode.D) ? 1 : 0);
        float moveVertical = Input.GetKey(KeyCode.W) ? 1 : (Input.GetKey(KeyCode.S) ? -1 : 0);

        // Calculate movement direction based on camera's forward and right vectors
        Vector3 moveDirection = (playerCamera.forward * moveVertical + playerCamera.right * moveHorizontal).normalized;

        // Prevent movement in the vertical direction (to avoid flying or sinking)
        moveDirection.y = 0;

        // Apply movement
        rb.MovePosition(rb.position + moveSpeed * Time.deltaTime * moveDirection);
        // rb.AddForce(moveSpeed * Time.deltaTime * moveDirection);
    }

    private void RotateCamera() {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player's body horizontally (yaw)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically (pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit vertical rotation
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
