using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewCarMovement : MonoBehaviour
{
    float horizontal;
    float vertical;
    Vector3 move;
    Rigidbody rb;
    public float speed;
    public float rotSpeed;
    public float breakPower;
    public float maxSpeed;
    float brake;
    CarMovements movements;
    public string controlScheme;
    [HideInInspector] public FollowPlayer playerNumberText;
    public ParticleSystem carSmoke;
    public ParticleSystem carSmoke2;
    public PlayerController playerController;
    public Transform groundCheck;
    public Transform sparkPoints;

    Player1Input player1Input;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movements = GetComponent<CarMovements>();
        
    }

    public void SetupCar()
    {
        //controlScheme = GetComponent<PlayerInput>().currentControlScheme;
    }

    public bool GroundCheck()
    {
        if(Physics.Raycast(groundCheck.transform.position, -Vector3.up, out RaycastHit hit ,0.25f))
        {
            //transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {

        //if (movements.currentDriver != null)
        //{
            

            //if (movements.isInputEnabled && playerController.pad != null)
            //{


            //    Vector2 stickL = playerController.pad.leftStick.ReadValue();
            //    Vector2 stickR = playerController.pad.rightStick.ReadValue();


            //    if (controlScheme == "GamePadLeft")
            //    {

            //        brake = playerController.pad.leftTrigger.ReadValue();

            //        if (playerController.pad.dpad.down.isPressed)
            //        {
            //            movements.DisableInput();
            //        }

            //        move = new Vector3(stickL.x, 0, stickL.y);


            //        //carSmoke.Play();
            //        //carSmoke2.Play();
                    
            //        horizontal = stickL.x;
            //        vertical = stickL.y;
            //    }
            //    else if (controlScheme == "GamePadRight")
            //    {

            //        brake = playerController.pad.rightTrigger.ReadValue();

            //        if (playerController.pad.aButton.isPressed)
            //        {
            //            movements.DisableInput();
            //        }

            //        move = new Vector3(stickR.x, 0, stickR.y);


            //        //carSmoke.Play();
            //        //carSmoke2.Play();
                    

            //        horizontal = stickR.x;
            //        vertical = stickR.y;
            //    }
            //}

            //horizontal = Input.GetAxis("Horizontal");
            //vertical = Input.GetAxis("Vertical");
            //move.x = horizontal;
            //move.z = vertical;

            //movements.moveInput = rb.velocity.magnitude;

            //float rot = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;

            //if (move != Vector3.zero)
            //{
            //    Vector3 newAngle = new Vector3(0, rot, 0);
            //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(newAngle.x, newAngle.y, newAngle.z), Time.deltaTime * rotSpeed);
            //}

            
            

        //}
    }

    private void OnDisable()
    {
        //player1Input.Disable(); 
    }

    private void FixedUpdate()
    {
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        //if(movements.currentDriver != null) 
        //{

        //    if(move == Vector3.zero)
        //    {
        //        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * 0.5f);

        //        //carSmoke.Stop();
        //        //carSmoke2.Stop();


        //    }
            
        //    if(brake == 1)
        //    {
        //        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * breakPower);
        //    }
        //    else
        //    {
        //        rb.AddForce(move * speed, ForceMode.Acceleration);
        //    }
            
        //}
        

        
    }
}
