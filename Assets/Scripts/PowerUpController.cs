using UnityEngine;
using UnityEngine.UIElements;

public class PowerUpController : MonoBehaviour
{
    public PowerUp powerUp;

    public bool activated = false;
    float timer = 7;

    GameObject carGO = null;

    public AudioSource powerUpCollectAudio;

    public GameObject powerupIcon;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<CarMovements>() && !activated && other.GetComponent<CarMovements>().currentDriver && other.GetComponent<CarMovements>().isUsingPowerups == false)
        {   
            
            carGO = other.gameObject;
            powerUp.Apply(carGO);
            activated = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;

            powerupIcon.SetActive(false);

            other.GetComponent<CarMovements>().isUsingPowerups = true;
            powerUpCollectAudio.Play();
        }


    }
    private void Update()
    {
        if (timer > 0 && activated)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                
               
                if(carGO != null)
                {
                     powerUp.Cancel(carGO);
                     carGO.GetComponent<CarMovements>().isUsingPowerups = false;
                }
                
                Destroy(gameObject);

            }

        }
    }
}
