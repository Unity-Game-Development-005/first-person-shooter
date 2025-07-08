
using System.Collections;
using UnityEngine;


public class FPSControllerScript : MonoBehaviour
{
    // reference to camera component
    private Camera playerCamera;

    // player speed
    [SerializeField] private float moveSpeed = 6f;

    // multiplier for run speed
    [SerializeField] private float runMultiplier = 2f;

    // player jump height
    [SerializeField] private float jumpForce = 2f; //7f;

    // amount of gravity applied to the player
    [SerializeField] private float gravity = 6f; //9.8f;

    // how quickly the mouse moves
    public float mouseSensitivity = 2f;

    // up/down player movement contraint
    [SerializeField] private float lookXLimit = 60f;

    // camera rotation around the 'x' axis
    private float rotationX = 0;

    // player's movement
    private Vector3 moveDirection;

    // reference to the character controller component
    private CharacterController characterController;






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // set reference to the camera
        playerCamera = Camera.main;

        // set reference to the character controller
        characterController = GetComponent<CharacterController>();

        // lock the cursor
        Cursor.lockState = CursorLockMode.Locked;

        // and hide it
        Cursor.visible = false;


        PlayerHealthController.playerHealthController.currentHealth = PlayerHealthController.playerHealthController.maximumHealth;

        PlayerHealthController.playerHealthController.currentRunStamina = PlayerHealthController.playerHealthController.maximumRunStamina;


        UIController.uiController.healthBarSlider.maxValue = PlayerHealthController.playerHealthController.maximumHealth;

        UIController.uiController.healthBarSlider.value = PlayerHealthController.playerHealthController.currentHealth;

        UIController.uiController.staminaBarSlider.maxValue = PlayerHealthController.playerHealthController.maximumRunStamina;

        UIController.uiController.staminaBarSlider.value = PlayerHealthController.playerHealthController.currentRunStamina;

    }


    // Update is called once per frame
    void Update()
    {
        // using the mouse
        // rotate the player around the 'y' axis to make the camera look left/right
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivity);

        // rotate the camera around the 'x' axis
        rotationX += -Input.GetAxis("Mouse Y") * mouseSensitivity;

        // restrict the movement of the camera when the player looks up/down
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        // tilt the camera
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);



        // player movement
        // if the player is on the ground
        if (characterController.isGrounded)
        {
            // get player input
            // horizontal
            float horizontalInput = Input.GetAxis("Horizontal");

            // vertical
            float verticalInput = Input.GetAxis("Vertical");

            // temporary variable for storing the player's 'y' movement (jumping)
            float moveDirectionY = moveDirection.y;

            // get the player's move direction
            moveDirection = (horizontalInput * transform.right) + (verticalInput * transform.forward);


            // player jump
            // if the player presses the jump button
            if (Input.GetButtonDown("Jump"))
            {
                // make the player jump
                moveDirection.y = jumpForce;
            }

            // otherwise
            else
            {
                // the player is still on the ground
                moveDirection.y = moveDirectionY;
            }


            // player run
            // if the player presses the run key ( 'left-shift' )
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                // make the player run
                moveSpeed *= runMultiplier;

                PlayerHealthController.playerHealthController.currentRunStamina -= PlayerHealthController.playerHealthController.runEnergyUse * Time.deltaTime;

                if (PlayerHealthController.playerHealthController.currentRunStamina <= 0)
                {
                    PlayerHealthController.playerHealthController.currentRunStamina = 0;
                }

                UIController.uiController.staminaBarSlider.value = PlayerHealthController.playerHealthController.currentRunStamina / PlayerHealthController.playerHealthController.maximumRunStamina;
            }

            // if the player releases the run key
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                // stop the player from running
                moveSpeed /= runMultiplier;

                if (PlayerHealthController.playerHealthController.rechargeRunStamina != null)
                {
                    StopCoroutine(PlayerHealthController.playerHealthController.RechargeStamina());
                }

                PlayerHealthController.playerHealthController.rechargeRunStamina = StartCoroutine(PlayerHealthController.playerHealthController.RechargeStamina());
            }
        }

        //otherwise
        else
        {
            // keep the player on the ground
            moveDirection.y -= gravity * Time.deltaTime;
        }


        // then move the player
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }


} // end of class
