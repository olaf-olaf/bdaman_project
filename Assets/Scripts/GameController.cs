using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance { get { return instance; } }

    // all the settings from the main menu
    public enum GameMode { DHB, Puck, ShootGap, Count }
    private GameMode gameMode;
    private delegate void UpdateDelegate();
    private UpdateDelegate[] UpdateDelegates;


    public int gameLevel;


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
    private float p1_health = 100f;
    private float p2_health = 100f;

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


        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");


        //setup all UpdateDelegates here to avoid runtime memory allocation
        UpdateDelegates = new UpdateDelegate[(int)GameMode.Count];

        //and then each UpdateDelegate
        UpdateDelegates[(int)GameMode.DHB] = UpdateDHBState;
        UpdateDelegates[(int)GameMode.Puck] = UpdatePuckState;
        UpdateDelegates[(int)GameMode.ShootGap] = UpdateShootGapState;


        // set general player attributes
        setPlayerAttributes();


        // should be altered according to the defined gameMode
        gameMode = GameMode.DHB;
        awakeDHB();
    }

    void setPlayerAttributes()
    { 
        player1.GetComponent<MovePlayer>().speed = p1_movement_speed;
        player1.GetComponent<PlayerController>().power = p1_power;
        player1.GetComponent<PlayerController>().magazineSize = p1_mag_size;
        player1.GetComponent<PlayerController>().reloadTime = p1_reload_time;
        player1.GetComponent<PlayerController>().accuracy = p1_accuracy;
        player1.GetComponent<PlayerController>().fireRate = p1_fire_rate;

        player2.GetComponent<MovePlayer>().speed = p2_movement_speed;
        player2.GetComponent<PlayerController>().power = p2_power;
        player2.GetComponent<PlayerController>().magazineSize = p2_mag_size;
        player2.GetComponent<PlayerController>().reloadTime = p2_reload_time;
        player2.GetComponent<PlayerController>().accuracy = p2_accuracy;
        player2.GetComponent<PlayerController>().fireRate = p2_fire_rate; 
    }


    void awakeDHB()
    {

        // set the health bars for the players 

    }




    // update functions
    void Update()
    {
       
        //call the update method of current state
        if (UpdateDelegates[(int)gameMode] != null)
            UpdateDelegates[(int)gameMode]();
    }

    void UpdateDHBState()
    {
       


    }

    void UpdatePuckState()
    {

    }

    void UpdateShootGapState()
    {

    }





}