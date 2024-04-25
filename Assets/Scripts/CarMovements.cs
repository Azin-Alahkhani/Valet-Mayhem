
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Runtime.ConstrainedExecution;
using UnityEngine.PlayerLoop;
using Unity.VisualScripting;

public class CarMovements : MonoBehaviour
{
    private CarSpawner carSpawner;
    [HideInInspector] public bool isInputEnabled = false;

    public float carSpeed = 100f;
    public float parkingSpaceRadius = 1.0f;

    public Material[] driverMaterials = new Material[8];
    private Material currentMaterial;
    public Material defaultMat;

    public ParticleSystem explosionParticleSystem;
    public ParticleSystem FireParticleSystem;

    public bool isOnFire = false;

    [HideInInspector]
    public CarObject thisCar;

    private int currentDriverIndex;

    public GameObject currentDriver;


    private string parkingScoreFloatingText;

    public GameObject FloatingTextPrefab;
    public GameObject dmgFloatingText;

    public bool Azine = false;

    bool invulnerable;
    public float invulnerableTime;
    float invulnerableTimer;


    public event Action OnCarParked;
    public int parkingScoreEarned;

    public Rigidbody sphereRB;
    public Rigidbody carRB;
    //public Rigidbody thiscarRB;


    public float moveInput;
    public float turnInput;
    public float fwdspeed;
    public float revSpeed;
    public float turnSpeed;

    public LayerMask parkingSpaceLayer;

    //public AudioSource screechAudio;
    public AudioSource colOOF;
    public AudioSource colCRASH;
    public AudioSource colSCREAM;

    public ParticleSystem Emissionparticle;
    NewCarMovement newMove;

    private Quaternion previousRotation;

    public bool parked;

    public float diff = 1f;
    public bool spawnDamage = false;

    public GameObject explosionEffectPrefab;

    Vector3 testMove;

    public Transform groundNormalCheck;

    public bool isShielded = false;

    public bool isUsingPowerups = false;

    float normalSpeed;

    public ParticleSystem carSmoke;

    [HideInInspector] public GameObject icon;


    public ParticleSystem splash;

    private void Start()
    {
        carSpawner = FindObjectOfType<CarSpawner>();
        thisCar = carSpawner.GetCarObject(gameObject);
        newMove = GetComponent<NewCarMovement>();
        normalSpeed = GetComponent<NewCarMovement>().speed;
        //sphereRB.transform.parent = null;
        //carRB.transform.parent = null;


        previousRotation = transform.rotation;
    }

    private void OnEnable()
    {


    }

    public void OnSwitch()
    {
        //controls = new Player1Input();
        //GetComponent<PlayerInput>().SwitchCurrentControlScheme(currentDriver.GetComponent<PlayerController>().controlScheme);
        //controls.Enable();
    }

    private void OnDisable()
    {
        //controls.Disable();
    }
    public void SetColor(Material currentMaterial, int driverIndex)
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        var mats = renderer.materials;
        mats[0] = currentMaterial;
        renderer.materials = mats;


    }
    public void EnableInput(int driverIndex, GameObject playerObject, Vector3 pos)
    {
        isInputEnabled = true;

        

        currentDriver = playerObject;
        currentDriverIndex = driverIndex;
        SetColor(currentDriver.GetComponent<MeshRenderer>().material, driverIndex);

    }

    public void DisableInput()
    {
        if(currentDriver != null)
        {
            //currentDriver.SetActive(true);
            currentDriver.GetComponent<PlayerController>().triggerCollider.enabled = true;
            currentDriver.GetComponent<PlayerController>().normalCollider.enabled = true;
            currentDriver.GetComponent<PlayerController>().inCar = false;
            if(currentDriver.transform.childCount < 2)
            {
                currentDriver.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                currentDriver.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = true;
            }


            //currentDriver.GetComponent<PlayerController>().OnSpawn();
            currentDriver.transform.position = new Vector3(transform.position.x + 5, transform.position.y + 1, transform.position.z + 5);
            if (Camera.main.GetComponent<MultipleTargetCamera>())
            {
                for (int i = 0; i < Camera.main.GetComponent<MultipleTargetCamera>().targets.Count; i++)
                {
                    if (Camera.main.GetComponent<MultipleTargetCamera>().targets[i].gameObject == thisCar.carObject)
                    {
                        Camera.main.GetComponent<MultipleTargetCamera>().targets[i] = currentDriver.transform;
                    }
                }
            }
            if(GetComponent<NewCarMovement>().playerNumberText != null)
            {
                GetComponent<NewCarMovement>().playerNumberText.target = currentDriver.transform;
            }
            
            if(icon != null)
            {
                icon.GetComponent<FollowPlayer>().target = currentDriver.transform;
            }

            currentDriver = null;
            isInputEnabled = false;
            if (!parked)
            {
                transform.tag = "freeCar";
                SetColor(defaultMat, currentDriverIndex);
            }
            else
            {
                if(carSpawner != null)
                {
                    carSpawner.OnCarPickedUp(thisCar);
                }
                
            }

            
        }



    }

    private void OnDestroy()
    {
        if(carSpawner != null)
        {
            carSpawner.OnCarPickedUp(thisCar);
        }
        
    }
  


    private void Update()
    {

        if (invulnerable)
        {
            invulnerableTimer += Time.deltaTime;
            if (invulnerableTimer >= invulnerableTime)
            {
                invulnerable = false;
                invulnerableTimer = 0;
            }


            if (groundNormalCheck != null)
            {

                if (Physics.Raycast(groundNormalCheck.position, -Vector3.up, out RaycastHit hit, 1f))
                {
                    transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                }
            }
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, parkingSpaceRadius, parkingSpaceLayer);
        
        //  change parking conditions 
        if (colliders.Length > 0 && moveInput < 1&& !parked && currentDriver!=null)
        {

            
            thisCar.isParked = (true);
            foreach (Collider c in colliders)
            {
                var spot = c.GetComponent<ParkingSpot>();
                switch (spot.points)
                {
                    
                    case ParkingSpot.Points.ONE:
                        if (spot.doublePoints)
                        {
                            parkingScoreEarned = 200 * thisCar.life / 100;
                        }
                        else
                        {
                            parkingScoreEarned = 100 * thisCar.life / 100;
                        }
                        break;
                    case ParkingSpot.Points.TWO:
                        if (spot.doublePoints)
                        {
                            parkingScoreEarned = 400 * thisCar.life / 100;
                        }
                        else
                        {
                            parkingScoreEarned = 200 * thisCar.life / 100;
                        }
                        break;
                    case ParkingSpot.Points.THREE:
                        if (spot.doublePoints)
                        {
                            parkingScoreEarned = 600 * thisCar.life / 100;
                        }
                        else
                        {
                            parkingScoreEarned = 300 * thisCar.life / 100;
                        }
                        break;
                    default:
                        break;
                }
                parkingScoreFloatingText = "+ " + parkingScoreEarned;
                parked = true;
                this.enabled = false;
                newMove.enabled = false;
                //GetComponent<PlayerInput>().enabled = false;
                //Destroy(gameObject);


            }

            //makes car heavy after parking
            //carRB.drag = 15;
           // carRB.mass = 15;
            //sphereRB.drag = 5;
            //sphereRB.mass = 5;
            //transform.Rotate(0f, 0f, 0f, Space.World);
            // carRB.constraints = RigidbodyConstraints.FreezePositionY;
            /*thiscarRB.drag = 100;
            thiscarRB.mass = 100;*/

            ShowFloatingScore();
            DisableInput();
            RandomEventController.Instance.drivableCars.Remove(gameObject);
        }

           if(!isOnFire  && thisCar.life < 40)
        { 
            ParticleSystem fire = Instantiate(FireParticleSystem, new Vector3(transform.position.x, transform.position.y+4,transform.position.z ), Quaternion.identity);
            fire.gameObject.transform.parent = gameObject.transform;
            isOnFire = true;
            Destroy(fire, 30);

        } 
    }



    private void FixedUpdate()
    {

        //sphereRB.AddForce(transform.forward * 500, ForceMode.Acceleration);

        if (isInputEnabled)
        {
            
            //sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
            //transform.Translate(testMove * 100 * Time.deltaTime);

        }

        //carRB.MoveRotation(transform.rotation);
        
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        
        if (currentDriver != null && !isShielded && !gameObject.CompareTag("freeCar"))
        {
            if (collision.gameObject.CompareTag("Player") && !gameObject.CompareTag("freeCar"))
            {
                ReduceLifeOnDamage(10);

                colOOF.Play();
                colCRASH.Play();

                ShowFloatingLostLife(10);
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Cars") || collision.gameObject.CompareTag("freeCar") || collision.gameObject.CompareTag("pickedUpCar"))
            {
                ReduceLifeOnDamage(10);
                colCRASH.Play();
                ShowFloatingLostLife(10);

            }
            else if (collision.gameObject.CompareTag("Walls"))
            {
                ReduceLifeOnDamage(20);
                ShowFloatingLostLife(20);

            }
            else if (collision.gameObject.CompareTag("TrafficCone") || collision.gameObject.CompareTag("Barrier"))
            {
                ReduceLifeOnDamage(10);
                colCRASH.Play();
                ShowFloatingLostLife(10);

            }
            /*else if (collision.gameObject.CompareTag("SpeedBump"))
            {
                ReduceLifeOnDamage(5);
                Debug.Log("wall HIT: " + thisCar.life);
                ShowFloatingLostLife();

            }*/
        }

        if (spawnDamage)
        {



        }

        spawnDamage = true;
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("OutOfBounds"))
        {
            DisableInput();
            RandomEventController.Instance.drivableCars.Remove(gameObject);
            Destroy(gameObject);
        }

        if (!isShielded)
        {
            
            if (other.gameObject.CompareTag("SpeedBump"))
            {
                GetComponent<Rigidbody>().velocity /= 2;
                //colOOF.Play();
            }


            if (other.gameObject.CompareTag("freeCar"))
            {
                ReduceLifeOnDamage(10);
                Debug.Log("wall HIT: " + thisCar.life);
                colCRASH.Play();
                ShowFloatingLostLife(10);

            }

            if (other.CompareTag("test"))
            {
                Debug.Log("hello");
            }

            if (other.gameObject.CompareTag("Water"))
            {
                DisableInput();
                Instantiate(splash, transform.position, Quaternion.identity);
                RandomEventController.Instance.drivableCars.Remove(gameObject);
                Destroy(gameObject);

            }
        }
       
       


    }

    private void OnTriggerExit(Collider other)
    {


        if (other.gameObject.CompareTag("SpeedBump"))
        {
            //GetComponent<NewCarMovement>().speed = normalSpeed;


        }

    }




    void ApplyMaterialToChild(string childObjectName, Material material)
    {
        Transform childTransform = transform.Find(childObjectName);


        if (childTransform != null)
        {
            Renderer childRenderer = childTransform.GetComponent<Renderer>();

            if (childRenderer != null)
            {
                childRenderer.material = material;
            }
            else
            {
                Debug.LogError("Renderer component not found on child object: " + childObjectName);
            }
        }
    }


    public void ShowFloatingLostLife(int damage)
    {
        if (FloatingTextPrefab != null && !invulnerable)
        {
            var go = Instantiate(FloatingTextPrefab, new Vector3(transform.position.x, 2, transform.position.z), Quaternion.Euler(90, 0, 0));
            go.GetComponent<TextMeshPro>().color = Color.red;
            go.GetComponent<TextMeshPro>().text = "-" + damage;
            invulnerable = true;
        }
    }
    
    void ShowFloatingScore()
    {

        if (FloatingTextPrefab != null)
        {

            var go = Instantiate(dmgFloatingText, new Vector3(transform.position.x, 2, transform.position.z), Quaternion.Euler(90, 0, 0));
            go.GetComponent<TextMeshPro>().text = parkingScoreFloatingText;
        }

        OnCarParked?.Invoke();
    }


    public void ReduceLifeOnDamage(int damage)
    {
        if (!invulnerable)
        {
            if (thisCar.life - damage <= 0)
            {

                // colSCREAM.Play();

                DisableInput();

                ParticleSystem explosion = Instantiate(explosionParticleSystem, transform.position, Quaternion.identity);

                Destroy(gameObject);

                //play audio

                colSCREAM.Play();

            }
            else
            {
                thisCar.life -= damage;
            }
        }

    }
    public void activateCarSmoke()
    {
        carSmoke.Play();
    }

    public void deactivateCarSmoke()
    {
        carSmoke.Clear();
        carSmoke.Stop();
    }
}

