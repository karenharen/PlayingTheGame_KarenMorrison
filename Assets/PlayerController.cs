using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    public float forSpeed;

    private GameObject focalPoint;
    private Renderer playerRend;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRend = GetComponent<Renderer>();

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
    }
}
