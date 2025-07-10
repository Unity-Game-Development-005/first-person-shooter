
using UnityEngine;


public class FPSControllerScript : MonoBehaviour
{
    // reference to camera component
    private Camera playerCamera;


    // player move speed
    private float playerMoveSpeed = 3f;

    // player walk speed
    private float walkSpeed = 3f;

    // run speed
    private float runSpeed = 6f;

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
    private Vector3 playerMoveDirection;

    // reference to the character controller component
    private CharacterController characterController;

    // whether minimap is active
    private bool minimapActive;

    // minimap
    [SerializeField] private GameObject miniMap;

    private const int CONSOLE_ACTIVE = 1;
    private const int CONSOLE_INACTIVE = -1;

    // console control modes
    [HideInInspector] public int consoleState;


    public Animator consoleAnimator;








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


        // set the player's current health, stamina and ammo
        PlayerHealthController.playerHealthController.currentHealth = PlayerHealthController.playerHealthController.maximumHealth;

        PlayerHealthController.playerHealthController.currentStamina = PlayerHealthController.playerHealthController.maximumStamina;

        PlayerAmmoController.playerAmmoController.currentAmmo = PlayerAmmoController.playerAmmoController.maximumAmmo;


        InitialiseUI();

        // show map console
        consoleState = CONSOLE_ACTIVE;

        SetConsoleState(CONSOLE_ACTIVE);
    }


    // Update is called once per frame
    void Update()
    {
        // switch minimap on/off
        ActivateMinimap();


        GetPlayerInput();
    }


    private void GetPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayerHealthController.playerHealthController.DamagePlayer(25);
        }


        // if the player presses the 'r' key
        if (Input.GetKeyDown(KeyCode.R))
        {
            // and the player is out of ammo
            if (PlayerAmmoController.playerAmmoController.currentAmmo == 0)
            {
                // then replenish the player's ammo
                PlayerAmmoController.playerAmmoController.currentAmmo = PlayerAmmoController.playerAmmoController.maximumAmmo;
                
                UIController.uiController.ammoBarSlider.value = PlayerAmmoController.playerAmmoController.currentAmmo;

                UIController.uiController.ammoText.text = PlayerAmmoController.playerAmmoController.currentAmmo + " / " + PlayerAmmoController.playerAmmoController.maximumAmmo;
            }
        }



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
            float moveDirectionY = playerMoveDirection.y;

            // get the player's move direction
            playerMoveDirection = (horizontalInput * transform.right) + (verticalInput * transform.forward);


            // player jump
            // if the player presses the jump button
            if (Input.GetButtonDown("Jump"))
            {
                // make the player jump
                playerMoveDirection.y = jumpForce;
            }

            // otherwise
            else
            {
                // the player is still on the ground
                playerMoveDirection.y = moveDirectionY;
            }


            // player run
            // if the player presses the run key ( 'left-shift' )
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // and the player has current stamina
                if (PlayerHealthController.playerHealthController.currentStamina > 0)
                {
                    // the player starts to run
                    //walkSpeed = runSpeed;
                    playerMoveSpeed = runSpeed;

                    // so decrease the player's current stamina
                    PlayerHealthController.playerHealthController.currentStamina -= PlayerHealthController.playerHealthController.energyRequiredToRun * Time.deltaTime;

                    // if the player has no more stamina
                    if (PlayerHealthController.playerHealthController.currentStamina <= 0)
                    {
                        playerMoveSpeed = walkSpeed;
                    }

                    else
                    {
                        playerMoveSpeed = runSpeed;
                    }
                }
            }

            // otherwise
            // if the player releases the run key
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                // the player returns to walking
                playerMoveSpeed = walkSpeed;

                // if the player's current stamina is less than or equal the player's maximum stamina
                if (PlayerHealthController.playerHealthController.currentStamina <= PlayerHealthController.playerHealthController.maximumStamina)
                {
                    // increase the player's current stamina, but at half the stamina recharge rate
                    PlayerHealthController.playerHealthController.currentStamina += PlayerHealthController.playerHealthController.halfStaminaRechargeRate * Time.deltaTime;
                }
            }

            // otherwise
            else
            {
                // the player is idle
                if (PlayerHealthController.playerHealthController.currentStamina <= PlayerHealthController.playerHealthController.maximumStamina)
                {
                    // so increase the player's current stamina at the normal rate
                    PlayerHealthController.playerHealthController.currentStamina += PlayerHealthController.playerHealthController.staminaRechargeRate * Time.deltaTime;
                }
            }

            // update the stamina ui
            UIController.uiController.staminaBarSlider.value = PlayerHealthController.playerHealthController.currentStamina;

            // stamina text
            float staminaPercentage = (float)PlayerHealthController.playerHealthController.currentStamina / PlayerHealthController.playerHealthController.maximumStamina * 100f;

            UIController.uiController.staminaText.text = $"{(int)staminaPercentage}%";
        }

        //otherwise
        else
        {
            // keep the player on the ground
            playerMoveDirection.y -= gravity * Time.deltaTime;
        }


        // then move the player
        characterController.Move(playerMoveDirection * playerMoveSpeed * Time.deltaTime);
    }


    private void InitialiseUI()
    {
        // health bar
        UIController.uiController.healthBarSlider.maxValue = PlayerHealthController.playerHealthController.maximumHealth;

        UIController.uiController.healthBarSlider.value = PlayerHealthController.playerHealthController.currentHealth;

        // health text
        float healthPercentage = (float)PlayerHealthController.playerHealthController.currentHealth / PlayerHealthController.playerHealthController.maximumHealth * 100f;

        UIController.uiController.healthText.text = $"{healthPercentage}%";

        // stamina bar
        UIController.uiController.staminaBarSlider.maxValue = PlayerHealthController.playerHealthController.maximumStamina;

        UIController.uiController.staminaBarSlider.value = PlayerHealthController.playerHealthController.currentStamina;
        
        // stamina text
        float staminaPercentage = (float)PlayerHealthController.playerHealthController.currentStamina / PlayerHealthController.playerHealthController.maximumStamina * 100f;

        UIController.uiController.staminaText.text = $"{(int)staminaPercentage}%";


        // ammo bar
        UIController.uiController.ammoBarSlider.maxValue = PlayerAmmoController.playerAmmoController.maximumAmmo;

        UIController.uiController.ammoBarSlider.value = PlayerAmmoController.playerAmmoController.currentAmmo;

        // ammo text
        UIController.uiController.ammoText.text = PlayerAmmoController.playerAmmoController.currentAmmo + " / " + PlayerAmmoController.playerAmmoController.maximumAmmo;
    }


    // select whether minimap is displayed
    private void ActivateMinimap()
    {
        // if the player presses the 'M' key
        if (Input.GetKeyDown(KeyCode.M))
        {
            // display the minimap depending upon its previous state
            consoleState = -consoleState;

            SetConsoleState(consoleState);
        }
    }


    private void SetConsoleState(int mapMode)
    {
        switch (mapMode)
        {
            // display the minimap
            case CONSOLE_ACTIVE:

                consoleAnimator.SetBool("consoleMode", true);

                break;

            // hide the minimap
            case CONSOLE_INACTIVE:

                consoleAnimator.SetBool("consoleMode", false);

                break;
        }
    }


} // end of class
