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

    public float p1_movement_speed;
    public float p1_accuracy;
    public float p1_fire_rate;
    public float p1_power;
    public float p1_reload_speed;
    public float p1_mag_size;

    public float p2_movement_speed;
    public float p2_accuracy;
    public float p2_fire_rate;
    public float p2_power;
    public float p2_reload_speed;
    public float p2_mag_size;


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
        //setup all UpdateDelegates here to avoid runtime memory allocation
        UpdateDelegates = new UpdateDelegate[(int)GameMode.Count];

        //and then each UpdateDelegate
        UpdateDelegates[(int)GameMode.DHB] = UpdateDHBState;
        UpdateDelegates[(int)GameMode.Puck] = UpdatePuckState;
        UpdateDelegates[(int)GameMode.ShootGap] = UpdateShootGapState;
         
        // should be altered according to the defined gameMode
        gameMode = GameMode.DHB;
        awakeDHB();
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