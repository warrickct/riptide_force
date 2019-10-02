using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Rigidbody rBody;

    public float swimmingStrength = 13.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PollMovements();
    }

    void PollMovements()
    {
        Vector3 direction = Vector3.zero; 
        if (Input.GetKey(KeyCode.W))
        {
            direction += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction -= transform.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += transform.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction -= transform.right;
        }
        direction = Vector3.Normalize(direction);
        rBody.AddForce(direction * swimmingStrength);
    }

}
