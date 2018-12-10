using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    private Rigidbody rb;
    public GameObject player;
    public float speed;
    public string horAxis;
    public string verAxis;

    // Use this for initialization
    void Start()
    {
        rb = player.GetComponent<Rigidbody>(); 
    }


    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis(horAxis); 
        float moveAxis = Input.GetAxis(verAxis); 
     

        Vector3 movement = new Vector3(moveHorizontal, 0, 0);
         
        rb.AddForce(movement * speed);
        player.transform.Rotate(0, moveAxis, 0); 




    }
}
