using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public int jumpStrength = 4;
    public int forwardSpeed = 4;
    public int rightSpeed = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space")) {
            GetComponent<Rigidbody>().velocity = new Vector3( 0, jumpStrength, 0 );
        }
        if (Input.GetKey("w")) //forward
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, forwardSpeed);
        }
        if (Input.GetKey("s")) //backward
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -(forwardSpeed));
        }
        if (Input.GetKey("a")) //left
        {
            GetComponent<Rigidbody>().velocity = new Vector3(-rightSpeed, 0, 0);
        }
        if (Input.GetKey("d")) //right
        {
            GetComponent<Rigidbody>().velocity = new Vector3(rightSpeed, 0, 0);
        }
    }
}
