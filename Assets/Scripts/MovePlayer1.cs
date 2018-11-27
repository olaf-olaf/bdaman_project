using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer1 : MonoBehaviour
{ 
    private Rigidbody rb;
    public GameObject player;
    public float speed;

    // Use this for initialization
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        //rb = GetComponent<Rigidbody>();
        //go = GetComponent<GameObject>();
    }


    void FixedUpdate()
    { 
        float moveHorizontal = Input.GetAxis("Horizontal_p1");
        float moveAxis = Input.GetAxis("Vertical_p1");
        print(moveAxis);

        Vector3 movement = new Vector3(moveHorizontal, 0, 0);
        //Vector3 rotation = new Vector3(0, moveAxis, 0);
        rb.AddForce(movement * speed);
        player.transform.Rotate(0, moveAxis, 0);
        //rb.transform.Rotate(rotation);
        // Is called every physics step. This is a set interval of seconds.
        
  

    }

}
