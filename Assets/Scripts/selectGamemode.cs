using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectGameMode : MonoBehaviour {

    public GameObject[] gameModes;
    public int gameModeElement = 0;
    public string[] modes = { "DHB", "PUCK" };
    public int maxGoals = 3;
    public bool content_generation = false;
    Text obs;
    Text goals;

    // Use this for initialization
    void Start () {
        obs = GameObject.FindGameObjectWithTag("ObstacleText").GetComponent<Text>();
        goals = GameObject.FindGameObjectWithTag("GoalText").GetComponent<Text>();
        obs.text = "";
        goals.text = "";
        setGameModeColor(); 
	}
	
	// Update is called once per frame
	void Update () {

        // selecting game mode
        if ( MenuController.gameModeSelection == true){


           

            

            if (gameModeElement == 1)
            {

                obs.text = "";
                goals.text = "Maximum Goals: " + maxGoals;
            }
            else
            {
                obs.text = "Obstacles: " + content_generation.ToString();
                goals.text = ""; 
            }


            if(Input.GetKeyDown(KeyCode.DownArrow)){
                gameModeElement = (gameModeElement + 1) % gameModes.Length;
                setGameModeColor();
                content_generation = false;

            }

            // selecting maxGoals
            if (Input.GetKeyDown(KeyCode.RightArrow) & gameModeElement == 1)
            { 
                maxGoals += 1;

            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) & gameModeElement == 1 & maxGoals  > 1)
            { 
                maxGoals -= 1;

            }

            // selecting object generation
            if (Input.GetKeyDown(KeyCode.RightArrow) & gameModeElement == 0)
            {
                content_generation = true;

            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) & gameModeElement == 0)
            {
                content_generation = false;

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
