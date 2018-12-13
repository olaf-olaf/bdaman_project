using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectGamemode : MonoBehaviour {

    public GameObject[] gameModes;
    private int gameModeElement = 0;
    private string[] modes = { "BHD", "PUCK" };

    // Use this for initialization
    void Start () {
        setGameModeColor();
		
	}
	
	// Update is called once per frame
	void Update () {
        if ( moveCamera.gameModeSelection == true){

            if(Input.GetKeyDown(KeyCode.DownArrow)){
                gameModeElement = (gameModeElement + 1) % gameModes.Length;
                setGameModeColor();

            }

            if (Input.GetKeyDown("enter")){

                //INIT GAME CODE HERE!
            }





        }
	}

    void setGameModeColor(){
        for (int i = 0; i < gameModes.Length;i++){
            if(i == gameModeElement){
                gameModes[i].GetComponent<Renderer>().material.color = Color.blue;
            }
            else{
                gameModes[i].GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}
