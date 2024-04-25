using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnCollidedWith : MonoBehaviour
{

    float timer;
    bool startTimer;
    public float timeToReset;
    public Collider nonTriggerCollider;
    public GameObject cooldownTimerText;
    GameObject spawnedTxt;
    public bool collided;
    [HideInInspector] public bool isProtected;
    float protectedTimer;
    public float protectedTime;
    public GameObject spawnProtectionPrefab;
    GameObject spawnProtectionObj;

    private void Start()
    {
        spawnedTxt = Instantiate(cooldownTimerText, new Vector3(transform.position.x, transform.position.y + 7f, transform.position.z), Quaternion.Euler(90,0,0));
        spawnedTxt.GetComponent<FollowPlayer>().target = transform;
        spawnedTxt.SetActive(false);
    }

    public void Collided(float stunTime)
    {
        if (!collided)
        {
            collided = true;
            GetComponent<PlayerController>().canMove = false;
            GetComponent<PlayerController>().stickL = Vector2.zero;
            GetComponent<PlayerController>().stickR = Vector2.zero;
            //nonTriggerCollider.enabled = false;
            startTimer = true;
            timer = stunTime;
            spawnedTxt.SetActive(true);

            GetComponent<PlayerController>().UpdateScoreText();
           


        }

    }

    private void Update()
    {

        if (isProtected)
        {
            if(spawnProtectionObj == null)
            {
                spawnProtectionObj = Instantiate(spawnProtectionPrefab, transform);
            }

            protectedTimer += Time.deltaTime;
            if(protectedTimer >= protectedTime)
            {
                isProtected = false;
                protectedTimer = 0;
                Destroy(spawnProtectionObj);
            }
        }

        if (startTimer)
        {
            if(GetComponent<PlayerController>().playerNumberText != null)
            {
                GetComponent<PlayerController>().playerNumberText.gameObject.SetActive(false);
            }
            
            timer -= Time.deltaTime;
            float timerText = Mathf.Round((timer) * 10f) * 0.1f;
            spawnedTxt.GetComponent<TextMeshPro>().text = (timerText).ToString();

            //Debug.Log(timer);

            if (timer <= 0)
            {
                isProtected = true;
                if (GetComponent<PlayerController>().playerNumberText != null) 
                {
                    GetComponent<PlayerController>().playerNumberText.gameObject.SetActive(true);
                }
                
                timer = 0;
                GetComponent<PlayerController>().canMove = true;
                nonTriggerCollider.enabled = true;
                spawnedTxt.SetActive(false);
                collided = false;
                startTimer = false;
            }
        }
    }

}
