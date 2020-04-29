using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    public float forSpeed;
    public bool hasPowerUp = false;
    public GameObject powerUpRing;

    private GameObject focalPoint;
    private Renderer playerRend;
    private float powerupPush = 10.0f;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRend = GetComponent<Renderer>();
        anim = GetComponent<Animator>();

        focalPoint = GameObject.Find("focalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float VerticalInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint.transform.forward * VerticalInput * forSpeed);
        
        if (VerticalInput > 0)
        {
            playerRend.material.color = new Color(1 - VerticalInput, 1, 1 - VerticalInput);
        } else
        {
            playerRend.material.color = new Color(1 + VerticalInput, 1, 1 + VerticalInput);
        }
        powerUpRing.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("powerup"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            anim.SetBool("HasPowerUp", true);
            powerUpRing.SetActive(true);
            StartCoroutine(PowerUpCountdown());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player collided with " + collision.gameObject + " with powerup set to " + hasPowerUp);
        if (collision.gameObject.CompareTag("enemy") && hasPowerUp)
        {      
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 dir = collision.gameObject.transform.position - transform.position;
            enemyRB.AddForce(dir * powerupPush, ForceMode.Impulse);
            
        }
    }

    IEnumerator PowerUpCountdown()
    {
        yield return new WaitForSeconds(5.0f);
        anim.SetBool("HasPowerUp", false);
        powerUpRing.SetActive(false);
        hasPowerUp = false;
        
    }
}
