using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class partSelector : MonoBehaviour {
    public GameObject[] sqauds;
    public GameObject[] feetParts;
    public GameObject[] cannonparts;
    private int bodyPart;
    private int feetIndex;
    private int cannonIndex;

    // Use this for initialization
    void Start () {
        bodyPart = 0;
        feetIndex = 0;
        cannonIndex = 0;
        sqauds[bodyPart].GetComponent<Renderer>().material.color = Color.green;
        feetParts[feetIndex].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {

        // Scroll through different sections of the body
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            selectBodySection();
         }

        // Select feet
        if(bodyPart == 2 && Input.GetKeyDown("right")){
            selectFeetPart();

        }

        // Select cannon
        if (bodyPart == 1 && Input.GetKeyDown("right"))
        {
            selectCannonPart();
        }
    }

    void selectCannonPart(){

        cannonIndex = (cannonIndex + 1) % cannonparts.Length;
        for (int i = 0; i < cannonparts.Length; i++)
        {
            if (i == cannonIndex)
            {
                cannonparts[i].SetActive(true);
            }
            else
            {
                cannonparts[i].SetActive(false);
            }
        }

    }


    void selectFeetPart(){
        feetIndex = (feetIndex + 1) % feetParts.Length;
        for (int i = 0; i < feetParts.Length; i++)
        {
            if (i == feetIndex)
            {
                feetParts[i].SetActive(true);
            } else {
                feetParts[i].SetActive(false);
            }
        }
    }

    void selectBodySection(){
        bodyPart = (bodyPart + 1) % sqauds.Length;
        setPartSelectColor();

    }

    void setPartSelectColor(){
        for (int i = 0; i < sqauds.Length; i++){
            if (i == bodyPart){
                sqauds[i].GetComponent<Renderer>().material.color = Color.green;
            } else {
                sqauds[i].GetComponent<Renderer>().material.color = Color.white;

            }
        }


    }
}
