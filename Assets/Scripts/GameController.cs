using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance { get { return instance; } }

    public string gameMode;
    public int gameLevel;

    // max suration of a session
    public int sessionDuration = 5;

    // max amount of goals
    public int maxGoals = 2;

    // these values need to be set in the game menu
    /*         NAME           Default               Range
     * movement_speed           120                  100, 150 
     * accuracy                  1                   1, 50  
     * fire_rate                 0.3                 0.1, 0.8
     * power                     20                  18, 25
     * reload time               3                   2, 4
     * mag size                  5                   3, 12 
    */
    public float p1_movement_speed;
    public float p1_accuracy;
    public float p1_fire_rate;
    public float p1_power;
    public float p1_reload_time;
    public int p1_mag_size; 


    public float p2_movement_speed;
    public float p2_accuracy;
    public float p2_fire_rate;
    public float p2_power;
    public float p2_reload_time;
    public int p2_mag_size;
     

    GameObject player1; 
    GameObject player2;

    GameObject Puck;
    private float p1_health = 100f;
    private float p2_health = 100f;
    private float sessionRemainingSeconds;
    private int sessionRemainingMinutes;
    public Text timeText;
    public bool generateGameObjects = false;
    
     

    // awake functions
    void Awake()
    {
        // Singleton structure
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        
        // init players
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        gameMode = "DHB";
        
        // set general player attributes
        setPlayerAttributes();
         
        // initialise session time
        sessionRemainingMinutes = sessionDuration;

        //set gamemode specific
        //call the update method of current state
        switch (gameMode)
        {
            case "DHB":
                awakeDHB();
                break;
            case "PUCK":
                awakePuck();
                break; 
            default:
                Debug.Log("INCORRECT GAME MODE");
                GameFinished();
                break;
        }
    } 

    void setPlayerAttributes()
    {
        List<int> settings_p1 = new List<int>() { 0, 0, 0 };
        player1.GetComponent<PlayerController>().bodyIndex = settings_p1;
        player1.GetComponent<MovePlayer>().speed = p1_movement_speed;
        player1.GetComponent<PlayerController>().power = p1_power;
        player1.GetComponent<PlayerController>().magazineSize = p1_mag_size;
        player1.GetComponent<PlayerController>().reloadTime = p1_reload_time;
        player1.GetComponent<PlayerController>().accuracy = p1_accuracy;
        player1.GetComponent<PlayerController>().fireRate = p1_fire_rate;

 
        List<int> settings_p2 = new List<int>() { 1, 1, 1 };
        player2.GetComponent<PlayerController>().bodyIndex = settings_p2;
        player2.GetComponent<MovePlayer>().speed = p2_movement_speed;
        player2.GetComponent<PlayerController>().power = p2_power;
        player2.GetComponent<PlayerController>().magazineSize = p2_mag_size;
        player2.GetComponent<PlayerController>().reloadTime = p2_reload_time;
        player2.GetComponent<PlayerController>().accuracy = p2_accuracy;
        player2.GetComponent<PlayerController>().fireRate = p2_fire_rate;
       
    }


    // initialise DHB game
    void awakeDHB()
    { 

        GameObject.FindGameObjectWithTag("Puck").SetActive(false); 
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Goal"))
        {
            go.SetActive(false);
        }
        foreach (GameObject he in GameObject.FindGameObjectsWithTag("Health"))
        {
            he.SetActive(true);
        }
    }


    // initialise PUCK game
    void awakePuck()
    {

        GameObject.FindGameObjectWithTag("Puck").SetActive(true);
     
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Goal"))
        {
            go.SetActive(true);
        }
        foreach (GameObject he in GameObject.FindGameObjectsWithTag("Health"))
        {
            he.SetActive(false);
        } 

    }


 
    // update functions
    void Update()
    {
        // measure the Remaining time of the session
        UpdateTime();

        // 
        if (sessionRemainingMinutes < 0)
        {
            GameFinished();
        }

        switch (gameMode)
        {
            case "DHB":
                updateDHB();
                break;
            case "PUCK":
                updatePuck();
                break;
        
            default: 
                GameFinished();
                break;
        }
    }

    // update the timer
    void UpdateTime()
    {
        sessionRemainingSeconds -= Time.deltaTime;
        if (sessionRemainingSeconds <= 0)
        {
            sessionRemainingMinutes -= 1;
            sessionRemainingSeconds = 59;
        }

        player1.GetComponentInChildren<Text>().text = "Time: " + sessionRemainingMinutes.ToString() + ":" + Math.Round(sessionRemainingSeconds).ToString();
        player2.GetComponentInChildren<Text>().text = "Time: " + sessionRemainingMinutes.ToString() + ":" + Math.Round(sessionRemainingSeconds).ToString();
    }



    void updateDHB()
    {   
        // check if one of the players has died
        if(player1.GetComponent<PlayerHealth>().m_Dead | player2.GetComponent<PlayerHealth>().m_Dead)
        { 
            GameFinished();
        }
             
    }

    void updatePuck()
    {
        foreach (GameObject goal in GameObject.FindGameObjectsWithTag("Goal"))
        {

            //check if one of the goals has surpassed the maximum 
    
            if (goal.GetComponent<GoalController>().points >= maxGoals)
            {

                GameFinished();
            } 

        }
    }
 

    void GameFinished()
    {
        // return to the main menu
        // .....
        Debug.Log("Game Finished!");
    } 
}