using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class partSelector : MonoBehaviour {
    public GameObject[] sqauds;
    private int bodyPart;

    // Use this for initialization
    void Start () {
        bodyPart = 0;
        sqauds[bodyPart].GetComponent<Renderer>().material.color = Color.green;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            print("downarrow");
            bodyPart = (bodyPart + 1) % sqauds.Length;
            setPartSelectColor();
            print(bodyPart);
         }
        // EDIT THIS ONCE IF FIGURED OUT HOW TO GO FROM 0 to 2 with uparrow.
        //else if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    print("UPARROW");
        //    bodyPart = (bodyPart - 1) % sqauds.Length;
        //    print(bodyPart);
        //}
    }

    void setPartSelectColor(){
        for (int i = 0; i < sqauds.Length; i++){
            if (i == bodyPart){
                sqauds[i].GetComponent<Renderer>().material.color = Color.green;
                //rend.material.color = Color.green;
                //print("TEE");

            } else {
                sqauds[i].GetComponent<Renderer>().material.color = Color.white;

                //print("HEE");

            }

        }


    }
}
