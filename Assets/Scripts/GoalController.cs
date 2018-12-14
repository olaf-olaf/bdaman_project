using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {
    public GameObject scoringPlayer;
    public int points = 0;

    void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Puck"))
        {
            points += 1;
            Debug.Log("GOALL");

            other.GetComponent<PuckController>().Reset();

        }
        
    }
}
