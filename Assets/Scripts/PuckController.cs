using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckController : MonoBehaviour {
    public Vector3 initialPos; 

    private void Start()
    { 
    }

     
    private IEnumerator RestartBall()
    {


        yield return new WaitForSeconds(2);
        Debug.Log("in restart bal function");

        transform.position = initialPos; 
    }

    public void Reset()
    {
        StartCoroutine(RestartBall());
         
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("");
        if (other.CompareTag("Goal"))
        { 
            Debug.Log("GOALL puckkie");

            Reset();

        }

    }
}
