using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer2 : MonoBehaviour {

    private Rigidbody rb;
    public GameObject player;
    public float speed;

    // Use this for initialization
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        //go = GetComponent<GameObject>();
    }


    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal_p2");
        float moveAxis = Input.GetAxis("Vertical_p2");
        print(moveAxis);

        Vector3 movement = new Vector3(moveHorizontal, 0, 0);
        //Vector3 rotation = new Vector3(0, moveAxis, 0);
        rb.AddForce(movement * speed);
        player.transform.Rotate(0, moveAxis, 0);
        //rb.transform.Rotate(rotation);
        // Is called every physics step. This is a set interval of seconds.




    }
}
