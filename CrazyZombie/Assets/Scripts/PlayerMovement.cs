using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
     [SerializeField] float gravity = -9.81f;

    CharacterController controller;
    Vector3 velocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Movimiento horizontal
        Vector3 move = transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime
                     + transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;

        controller.Move(move);

        // Gravedad
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f; 

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
    }

}