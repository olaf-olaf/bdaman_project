using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class GameSettings : MonoBehaviour {
    public float p1_movement_speed;
    public float p1_accuracy;
    public float p1_fire_rate;
    public float p1_power;
    public float p1_reload_time;
    public int p1_mag_size;
    public List<int> body_settings_p1 = new List<int>();


    public float p2_movement_speed;
    public float p2_accuracy;
    public float p2_fire_rate;
    public float p2_power;
    public float p2_reload_time;
    public int p2_mag_size;
    public List<int> body_settings_p2 = new List<int>();
    public bool isActive = false;

    // Use this for initialization
    void Awake() {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update() {


        if (Input.GetKeyDown(KeyCode.Space) && !isActive)
        { 
            initGame();
        }
    }

    void initGame()
    {
        isActive = true;
          
        GameObject p1 =  GameObject.FindGameObjectWithTag("Player1");
        GameObject p2 = GameObject.FindGameObjectWithTag("Player2");
        // get the current state of the parameters
        float[] settings_p1 = p1.GetComponent<partSelector>().current_stats;
        float[] settings_p2 = p2.GetComponent<partSelector>().current_stats;

        p1_movement_speed = settings_p1[0];
        p1_accuracy = settings_p1[1]/10;
        p1_fire_rate = settings_p1[2];
        p1_power = settings_p1[3];
        p1_reload_time = settings_p1[4];
        p1_mag_size = Mathf.RoundToInt(settings_p1[5]); 
        body_settings_p1.Add(p1.GetComponent<partSelector>().armIndex);
        body_settings_p1.Add(p1.GetComponent<partSelector>().feetIndex);
        body_settings_p1.Add(p1.GetComponent<partSelector>().cannonIndex);

        p2_movement_speed = settings_p2[0];
        p2_accuracy=settings_p2[1]/10;
        p2_fire_rate=settings_p2[2];
        p2_power = settings_p2[3];
        p2_reload_time= settings_p2[4]; ;
        p2_mag_size = Mathf.RoundToInt(settings_p2[5]);
        body_settings_p2.Add(p2.GetComponent<partSelector>().armIndex);
        body_settings_p2.Add(p2.GetComponent<partSelector>().feetIndex);
        body_settings_p2.Add(p2.GetComponent<partSelector>().cannonIndex);

        SceneManager.LoadScene(sceneBuildIndex: 2);
    }
}
