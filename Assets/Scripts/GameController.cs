using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
 

    GameSettings settings;
    GameObject player1; 
    GameObject player2;

    GameObject Puck;
    private float p1_health = 100f;
    private float p2_health = 100f;
    private float sessionRemainingSeconds;
    private int sessionRemainingMinutes;
    public Text timeText;
    public bool generateGameObjects = false;
    private Text p1_message;
    private Text p2_message;
    public bool gamePaused = false;

    GameObject GameSettings;




    // during pausegame the following values need to be stored
    Vector3 player1Velocity ;
    Vector3 player2Velocity ;
    Vector3 player1Angular;
    Vector3 player2Angular;
    Vector3 puckVelocity;
    Vector3 puckAngularVelocity;
    List<Vector3> bulletBackups = new List<Vector3>();


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

        // init settings
        GameSettings = GameObject.FindGameObjectWithTag("GameSettings");
        settings = GameSettings.GetComponent<GameSettings>();
      
        // init players
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        gameMode = "DHB";

        p1_message = GameObject.FindGameObjectWithTag("P1Message").GetComponent<Text>();
        p1_message.text = "";
        p2_message = GameObject.FindGameObjectWithTag("P2Message").GetComponent<Text>();
        p2_message.text = "";

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
        player1.GetComponent<PlayerController>().bodyIndex = settings.body_settings_p1;
        player1.GetComponent<MovePlayer>().speed = settings.p1_movement_speed;
        player1.GetComponent<PlayerController>().power = settings.p1_power;
        player1.GetComponent<PlayerController>().magazineSize = settings.p1_mag_size;
        player1.GetComponent<PlayerController>().reloadTime = settings.p1_reload_time;
        player1.GetComponent<PlayerController>().accuracy = settings.p1_accuracy;
        player1.GetComponent<PlayerController>().fireRate = settings.p1_fire_rate;

  
        player2.GetComponent<PlayerController>().bodyIndex =settings.body_settings_p2;
        player2.GetComponent<MovePlayer>().speed = settings.p2_movement_speed;
        player2.GetComponent<PlayerController>().power = settings.p2_power;
        player2.GetComponent<PlayerController>().magazineSize = settings.p2_mag_size;
        player2.GetComponent<PlayerController>().reloadTime = settings.p2_reload_time;
        player2.GetComponent<PlayerController>().accuracy = settings.p2_accuracy;
        player2.GetComponent<PlayerController>().fireRate = settings.p2_fire_rate;
       
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

        if (Input.GetKeyDown(KeyCode.P))
        {
            gamePaused = true; 
        }

        if (gamePaused)
        {
            pauseGame();
        }
        else
        {
            // measure the Remaining time of the session
            UpdateTime();

            // Finish game after certain time
            if (sessionRemainingMinutes < 0)
            {
                GameFinished();
            }

            // Finish game based on mode-dependent condition
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
    }
    void pauseGame()
    {
        // 
        p1_message.text ="Game paused; Press q (quit), r (restart), c (continue)";
        p2_message.text = "Game paused; Press q (quit), r (restart), c (continue)";

        // save all moving objects:
         player1Velocity = player1.GetComponent<Rigidbody>().velocity;
         player2Velocity = player2.GetComponent<Rigidbody>().velocity;
         player1Angular = player1.GetComponent<Rigidbody>().angularVelocity; 
         player2Angular = player1.GetComponent<Rigidbody>().angularVelocity;
         puckVelocity = Vector3.zero;
         puckAngularVelocity = Vector3.zero;

        if (gameMode == "PUCK")
        {
            puckVelocity = Puck.GetComponent<Rigidbody>().velocity;
            puckAngularVelocity  = Puck.GetComponent<Rigidbody>().angularVelocity;

            Puck.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Puck.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; 
        }

        player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player1.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        player1.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

         for (int i = 0; i < GameObject.FindGameObjectsWithTag("Bullet").Length; i++){// GameObject B in GameObject.FindGameObjectsWithTag("Bullet"))
    
            GameObject B = GameObject.FindGameObjectsWithTag("Bullet")[i];
            bulletBackups.Add(B.GetComponent<Rigidbody>().velocity);
            B.GetComponent<Rigidbody>().velocity = Vector3.zero; 
        } 
        // listen for user input

        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitGame();
        }
    
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(sceneBuildIndex: 2);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            p1_message.text = "";
            p2_message.text = "";


            player1.GetComponent<Rigidbody>().velocity = player1Velocity;
            player2.GetComponent<Rigidbody>().velocity = player2Velocity;
            player1.GetComponent<Rigidbody>().angularVelocity = player1Angular;
            player1.GetComponent<Rigidbody>().angularVelocity = player2Angular;

            if (gameMode == "PUCK")
            {
                Puck.GetComponent<Rigidbody>().velocity = puckVelocity;
                Puck.GetComponent<Rigidbody>().angularVelocity = puckAngularVelocity;
            } 
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Bullet").Length; i++)
            { 
           
                    GameObject B = GameObject.FindGameObjectsWithTag("Bullet")[i]; 
                    B.GetComponent<Rigidbody>().velocity = bulletBackups[i];
             }
            gamePaused = false;
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
        if (player1.GetComponent<PlayerHealth>().m_Dead) {
            GameWon(player2);
        }
        else if (player2.GetComponent<PlayerHealth>().m_Dead)
        {
            GameWon(player1);
        }     
    }

    void updatePuck()
    {
        foreach (GameObject goal in GameObject.FindGameObjectsWithTag("Goal"))
        {

            //check if one of the goals has surpassed the maximum 
    
            if (goal.GetComponent<GoalController>().points >= maxGoals)
            {
                GameWon(goal.GetComponent<GoalController>().scoringPlayer);    
            } 

        }
    }
    void GameWon(GameObject winningPlayer)
    { 
        p1_message.text = winningPlayer.tag + " won";
        p2_message.text = winningPlayer.tag + " won";
          
         GameFinished();
    }


    void QuitGame()
    {
        GameFinished();
    }


    void GameFinished()
    {
        // return to the main menu 
        Debug.Log("Game Finished!");
        SceneManager.LoadScene(sceneBuildIndex: 1);
    } 

    
}