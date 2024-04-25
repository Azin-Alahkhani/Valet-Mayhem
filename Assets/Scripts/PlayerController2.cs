using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
    public float moveSpeed = 5f;

    private GameObject currentCar;
    private bool isControllingCar = false;

    private Vector2 movementInput;
    private Vector3 originalPosition;

    public bool carParked = false;
    public int score = 0;

    public PlayerControls controls;

    public int playerIndex;

    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;

    public TMP_Text scoreTextMesh;

    CarMovements carMovement;

    void Start()
    {
        originalPosition = transform.position;
        string objectName = gameObject.name.Split('r')[1];
        playerIndex = int.Parse(objectName) - 1;

        audioSource = GetComponent<AudioSource>();


    }

    private void UpdateScore()
    {
        score += carMovement.parkingScoreEarned;

        scoreTextMesh.text = score.ToString();
        ;
    }

    void SlamDoor()
    {
        //if (!audioSource.isPlaying)
        //{
        audioSource.Play();
        Debug.Log("slammed");
        //}
    }

    void OnEnable()
    {
        controls = new PlayerControls();
        controls.Enable();

        controls.Player.Move2.performed += OnMovementPerformed;


        controls.Player.Move2.canceled += OnMovementCanceled;
        controls.Player.Switch.performed += ctx => SwitchToPlayer();
        // controls.Player.Switch.canceled += OnMovementCanceled;
    }

    void OnDisable()
    {
        controls.Disable();

        controls.Player.Move2.performed -= OnMovementPerformed;
        controls.Player.Move2.canceled -= OnMovementCanceled;

    }


    void Update()
    {

        scoreTextMesh.text = "" + score;

        if (!isControllingCar)
        {

        }
        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y);
        transform.Translate(movement * moveSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Cars") && other.gameObject.CompareTag("freeCar"))
        {
            SwitchToCar(other.gameObject);
            other.gameObject.tag = "pickedUpCar";

        }
    }

    private void SwitchToPlayer()
    {

        transform.position = originalPosition;
        gameObject.SetActive(true);
        ExitCar();
    }

    private void SwitchToCar(GameObject car)
    {
        SlamDoor();

        gameObject.SetActive(false);


        currentCar = car;
        currentCar.SetActive(true);



        isControllingCar = true;

        // Enable car input
         carMovement = currentCar.GetComponent<CarMovements>();
        if (carMovement != null)
        {
            carMovement.EnableInput(playerIndex, gameObject, originalPosition);
            carMovement.OnCarParked += UpdateScore;

        }
    }

    public void ExitCar()
    {
        gameObject.SetActive(true);

        isControllingCar = false;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        movementInput = Vector2.zero;
    }
}
