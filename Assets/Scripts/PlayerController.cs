using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    Animator animator;
    private GameObject currentCar;

    private Vector2 movementInput;
    [HideInInspector] public Vector3 movement;
    private Vector3 originalPosition; 

    public bool carParked = false;
    public int score = 0;

    public Player1Input controls;

    public int playerIndex;

    //public enum ControllerSide { LEFT, RIGHT };
    //public ControllerSide controllerSide;

    public bool canMove;

    int deviceIndex;
    public string controlScheme;
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;
    bool dash;
    bool canDash = true;
    float dashTimer;
    public float dashCooldown;

    public float dashForce;
    public TMP_Text scoreTextMesh;
    Rigidbody rb;

    public GameObject icon;
    
    PlayerInput playerInput;

    public CapsuleCollider triggerCollider;
    public CapsuleCollider normalCollider;

    public float gravity = -1f;
    float yVal;

    public bool inCar;

    [HideInInspector] public FollowPlayer playerNumberText;


    public Gamepad pad;
    private bool Oncar = false;

    [Header("Car Controls")]
    float brake;
    Vector3 carMove;
    float horizontal;
    float vertical;
    CarMovements carMovement;
    NewCarMovement newCarMovement;
    Rigidbody carRb;

    public Transform groundCheckPos;

    Vector3 prevPos;
    float posTimer;

    Vector3 startPos;

    [HideInInspector] public Vector2 stickL;
    [HideInInspector] public Vector2 stickR;
    bool inWater;
    float waterTimer;

    public ParticleSystem splash;

    public GameObject floatingText;

    public AudioClip waterSplash;
    public AudioSource spashSource;
    public ParticleSystem dashSmoke;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        startPos = transform.position;
        prevPos = startPos;

        canMove = true;

        originalPosition = transform.position;
        string objectName = gameObject.name.Split('r')[1];
        //playerIndex = int.Parse(objectName) - 1;
        audioSource = GetComponent<AudioSource>();

        playerInput = GetComponent<PlayerInput>();

        animator = transform.GetChild(0).GetComponent<Animator>();

        //deviceIndex = context.control.device.device.deviceId;



        //Debug.Log(pad);


    }

    

    private void UpdateScore()
    {
        score += carMovement.parkingScoreEarned;

        scoreTextMesh.text = score.ToString();

    }

    public void UpdateScoreText() {
        if (score > 1) {
            score -= 1;
            scoreTextMesh.text = score.ToString();
        }
       
    }

    void SlamDoor()
    {

        audioSource.PlayOneShot(audioSource.clip, volume);
        
        


    }

    public void OnSpawn() 
    {

        GetComponent<PlayerInput>().SwitchCurrentControlScheme(controlScheme);

        controls = new Player1Input();
        //controls.devices = new[] {pad};

        

        controls.Enable();

        controls.Player.Move.performed += OnMovementPerformed;
        controls.Player.Move.canceled += OnMovementCanceled;
       

    }

    void OnEnable()
    {
    }

    void OnDisable()
    {
        controls.Disable();
        //controls.Player.Move.performed -= OnMovementPerformed;
        //controls.Player.Move.canceled -= OnMovementCanceled;


    }

    bool GroundCheck()
    {
        if(Physics.Raycast(groundCheckPos.position, -Vector3.up, out RaycastHit hit, 0.5f))
        {
            return true;
        }
        return false;
    }


    void WaterTimer()
    {
        if (inWater)
        {
            waterTimer += Time.deltaTime;
            if(waterTimer >= 0.1f)
            {
                waterTimer = 0;
                inWater = false;
            }
        }
    }

    void Update()
    {
        if (!PauseMenu.Instance.paused)
        {
            WaterTimer();

            if (pad != null)
            {


                if (canMove)
                {
                    stickL = pad.leftStick.ReadValue();
                    stickR = pad.rightStick.ReadValue();

                }
                else
                {
                    stickL = Vector2.zero;
                    stickR = Vector2.zero;
                }


                if (playerInput.currentControlScheme == "GamePadLeft")
                {
                    if (!inCar)
                    {

                        if (!GroundCheck())
                        {

                            yVal = 1 + (gravity * Time.deltaTime);

                        }
                        else
                        {
                            yVal = 0;
                        }

                        if (carMovement != null)
                        {
                            if (carMovement.Emissionparticle.isPlaying)
                            {

                                carMovement.Emissionparticle.Clear();
                                carMovement.Emissionparticle.Stop();
                            }

                        }

                        dash = pad.leftShoulder.isPressed;
                        //scoreTextMesh.text = "" + score;


                        movement = new Vector3(stickL.x, -yVal, stickL.y);
                        if (new Vector3(stickL.x, 0, stickL.y) != Vector3.zero)
                        {
                            animator.SetBool("isWalking", true);
                        }
                        else
                        {
                            animator.SetBool("isWalking", false);
                        }
                        //rigidbody.velocity =  movement;
                        transform.Translate(movement * moveSpeed * Time.deltaTime);

                        //if(transform.childCount > 0)
                        //{
                        //    transform.GetChild(0).rotation = Quaternion.LookRotation(movement, Vector3.up);
                        //}

                    }
                    else
                    {
                        if (!carMovement.Emissionparticle.isPlaying)
                        {
                            carMovement.Emissionparticle.Play();
                        }

                        brake = pad.leftTrigger.ReadValue();

                        if (pad.dpad.down.isPressed)
                        {
                            carMovement.DisableInput();
                        }

                        carMove = new Vector3(stickL.x, 0, stickL.y);


                        //carSmoke.Play();
                        //carSmoke2.Play();

                        horizontal = stickL.x;
                        vertical = stickL.y;

                        carMovement.moveInput = carRb.velocity.magnitude;

                        float rot = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;



                        if (carMove != Vector3.zero)
                        {
                            Vector3 newAngle = new Vector3(0, rot, 0);
                            currentCar.transform.rotation = Quaternion.Lerp(currentCar.transform.rotation, Quaternion.Euler(newAngle.x, newAngle.y, newAngle.z),
                                Time.deltaTime * newCarMovement.rotSpeed);
                        }
                    }

                }
                else if (playerInput.currentControlScheme == "GamePadRight")
                {
                    if (!inCar)
                    {

                        if (!GroundCheck())
                        {

                            yVal = 1 + (gravity * Time.deltaTime);

                        }
                        else
                        {
                            yVal = 0;
                        }

                        dash = pad.rightShoulder.isPressed;
                        //scoreTextMesh.text = "" + score;
                        movement = new Vector3(stickR.x, -yVal, stickR.y);

                        if (animator != null)
                        {
                            if (new Vector3(stickR.x, 0, stickR.y) != Vector3.zero)
                            {
                                animator.SetBool("isWalking", true);
                            }
                            else
                            {
                                animator.SetBool("isWalking", false);
                            }
                        }

                        //rigidbody.velocity =  movement;

                        transform.Translate(movement * moveSpeed * Time.deltaTime);

                    }
                    else
                    {
                        brake = pad.rightTrigger.ReadValue();

                        if (pad.buttonSouth.isPressed)
                        {
                            carMovement.DisableInput();
                        }

                        carMove = new Vector3(stickR.x, 0, stickR.y);


                        //carSmoke.Play();
                        //carSmoke2.Play();

                        horizontal = stickR.x;
                        vertical = stickR.y;

                        carMovement.moveInput = carRb.velocity.magnitude;

                        float rot = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;

                        if (carMove != Vector3.zero)
                        {
                            Vector3 newAngle = new Vector3(0, rot, 0);
                            currentCar.transform.rotation = Quaternion.Lerp(currentCar.transform.rotation, Quaternion.Euler(newAngle.x, newAngle.y, newAngle.z),
                                Time.deltaTime * newCarMovement.rotSpeed);
                        }

                    }

                }

            }


            if (!canDash)
            {
                dashTimer += Time.deltaTime;
                if (dashTimer >= dashCooldown)
                {
                    canDash = true;
                    dashTimer = 0;
                }
            }
        }



    }

    private void FixedUpdate()
    {
        if (!PauseMenu.Instance.paused)
        {
            if (dash && canDash)
            {
                rb.AddForce(movement * dashForce, ForceMode.Impulse);
                Instantiate(dashSmoke, gameObject.transform.position, Quaternion.identity);
                canDash = false;
            }

            if (inCar)
            {
                carRb.velocity = Vector3.ClampMagnitude(carRb.velocity, currentCar.GetComponent<NewCarMovement>().maxSpeed);

                if (carMove == Vector3.zero)
                {
                    carRb.velocity = Vector3.Lerp(currentCar.GetComponent<Rigidbody>().velocity, Vector3.zero, Time.deltaTime * 0.5f);

                    //carSmoke.Stop();
                    //carSmoke2.Stop();


                }

                if (brake == 1)
                {
                    carRb.velocity = Vector3.Lerp(carRb.velocity, Vector3.zero, Time.deltaTime * currentCar.GetComponent<NewCarMovement>().breakPower);
                }
                else
                {
                    if (currentCar.GetComponent<NewCarMovement>().GroundCheck())
                    {
                        carRb.AddForce(carMove * currentCar.GetComponent<NewCarMovement>().speed, ForceMode.Acceleration);
                    }
                    else
                    {
                        carRb.AddForce(-Vector3.up * 10);
                    }

                }




            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Cars") && other.gameObject.CompareTag("freeCar"))
        {

            transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = false;

            if (Camera.main.GetComponent<MultipleTargetCamera>())
            {
                Debug.Log("cam");
                for (int i = 0; i < Camera.main.GetComponent<MultipleTargetCamera>().targets.Count; i++)
                {
                    if (Camera.main.GetComponent<MultipleTargetCamera>().targets[i].gameObject == gameObject)
                    {
                        Camera.main.GetComponent<MultipleTargetCamera>().targets[i] = other.transform;
                    }
                }

                //other.GetComponent<NewCarMovement>().playerController = this;
                
            }

            SwitchToCar(other.gameObject);
            other.gameObject.tag = "pickedUpCar";

        }
        else if (other.CompareTag("OutOfBounds"))
        {
            transform.position = prevPos;
            GetComponent<OnCollidedWith>().isProtected = true;
        }
        else if (other.CompareTag("Water"))
        {
            ParticleSystem newSplash = Instantiate(splash, transform.position, Quaternion.identity);
            newSplash.transform.localScale = new Vector3(4f, 4f, 4f);
            spashSource.Play();

            if (score > 0 && !inWater)
            {
                score -= 25;
                if(score < 0)
                {
                    score = 0;
                }
                scoreTextMesh.text = score.ToString();
                GameObject txt = Instantiate(floatingText, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.Euler(90,0,0));
                txt.GetComponent<TextMeshPro>().text = "-25";
                txt.GetComponent<TextMeshPro>().color = Color.red;
                
                inWater = true;
            }
            transform.position = prevPos;
            GetComponent<OnCollidedWith>().isProtected = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("InBounds"))
        {
            posTimer += Time.deltaTime;
            if (posTimer >= 1)
            {
                prevPos = transform.position;
                posTimer = 0;
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {

        if (other.collider.gameObject.layer == 10) 
        {
            rb.AddForce(-movement * 1000);
        }
    }



    private void SwitchToCar(GameObject car)
    {
        SlamDoor();

        normalCollider.enabled = false;
        triggerCollider.enabled = false;

        inCar = true;

        transform.localPosition = Vector3.zero;
        GetComponent<MeshRenderer>().enabled = false;
        
        

        currentCar = car;
        currentCar.SetActive(true);

        
        car.GetComponent<NewCarMovement>().SetupCar();
        if(playerNumberText != null)
        {
            playerNumberText.target = car.transform;
            car.GetComponent<NewCarMovement>().playerNumberText = playerNumberText;
        }

        car.GetComponent<NewCarMovement>().playerController = this;
        car.GetComponent<NewCarMovement>().controlScheme = controlScheme;

        car.GetComponent<CarMovements>().icon = icon;
        if(icon != null)
        {
            icon.GetComponent<FollowPlayer>().target = car.transform;
        }
        

        carRb = currentCar.GetComponent<Rigidbody>();
        newCarMovement = currentCar.GetComponent<NewCarMovement>();

        // Enable car input
        carMovement = currentCar.GetComponent<CarMovements>();
        if (carMovement != null)
        {
            Oncar = true;
            carMovement.EnableInput(playerIndex, gameObject, originalPosition);
            carMovement.OnSwitch();
            carMovement.OnCarParked += UpdateScore;

        }
    }

    public void ExitCar()
    {
        gameObject.SetActive(true);
        Oncar = false;
      
    }

    public void OnMovementPerformed(InputAction.CallbackContext context)
    {
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            if (Gamepad.all[i] == pad)
            {
                movementInput = context.ReadValue<Vector2>();
            }
        }
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }
}
