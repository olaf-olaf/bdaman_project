using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGameMode : MonoBehaviour {

    public GameObject[] gameModes;
    public int gameModeElement = 0;
    public string[] modes = { "DHB", "PUCK" };

    // Use this for initialization
    void Start () {
        setGameModeColor(); 
	}
	
	// Update is called once per frame
	void Update () {
        if ( MenuController.gameModeSelection == true){

            if(Input.GetKeyDown(KeyCode.DownArrow)){
                gameModeElement = (gameModeElement + 1) % gameModes.Length;
                setGameModeColor();

            }
        }
	}

    void setGameModeColor(){
        Debug.Log(modes[gameModeElement]);
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
