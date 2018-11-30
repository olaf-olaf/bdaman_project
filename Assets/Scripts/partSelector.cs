using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class partSelector : MonoBehaviour {
    public GameObject[] sqauds;
    public GameObject[] feetParts;
    public GameObject[] cannonParts;
    public GameObject[] armParts;

    private int armIndex;
    private int bodyPart;
    private int feetIndex;
    private int cannonIndex;

    // Use this for initialization
    void Start () {
        armIndex = 0;
        bodyPart = 0;
        feetIndex = 0;
        cannonIndex = 0;
        sqauds[bodyPart].GetComponent<Renderer>().material.color = Color.green;
        feetParts[feetIndex].SetActive(true);
        cannonParts[cannonIndex].SetActive(true);
        armParts[armIndex].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {

        // Scroll through different sections of the body
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            selectBodySection();
         }

        // Select feet
        if(bodyPart == 2 && Input.GetKeyDown("right")){
            selectPart(ref feetParts, ref feetIndex);

        }

        // Select cannon
        if (bodyPart == 1 && Input.GetKeyDown("right"))
        {
            selectPart(ref cannonParts, ref cannonIndex);
        }

        // Select arms
        if (bodyPart == 0 && Input.GetKeyDown("right"))
        {
            selectPart(ref armParts, ref armIndex);
        }
    }

    void selectPart(ref GameObject[] parts, ref int index){
        index = (index + 1) % parts.Length;
        for (int i = 0; i < parts.Length; i++)
        {
            if (i == index)
            {
                parts[i].SetActive(true);
            }
            else
            {
                parts[i].SetActive(false);
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
