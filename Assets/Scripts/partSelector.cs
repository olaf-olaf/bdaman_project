using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class partSelector : MonoBehaviour
{
    public GameObject[] sqauds;
    public GameObject[] feetParts;
    public GameObject[] cannonParts;
    public GameObject[] armParts;
    public GameObject currentSpeedDisplay;
    public GameObject currentAccuracyDisplay;
    public GameObject currentFireRateDisplay;
    public GameObject currentPowerDisplay;
    public GameObject currentReloadTimeDisplay;
    public GameObject currentMagSizeDisplay;
    private bool gameModeselection;

    public string horAxis;
    public string verAxis;

    private Vector3 maxDisplayLoc;
    private float maxDisplaywidth;
    private Dictionary<string, float[]> partstats = new Dictionary<string, float[]>();
    public float[] current_stats = { 120, 25, 0.3f, 20, 3, 5 };



    public int armIndex;
    private int bodyPart;
    public int feetIndex;
    public int cannonIndex;

    // Use this for initialization
    // these values need to be set in the game menu
    /*         NAME           Default               Range
     * movement_speed           120                  100, 150 
     * accuracy                  1                   1, 5
     * fire_rate                 0.3                 0.1, 0.8
     * power                     20                  18, 25
     * reload time               3                   2, 4
     * mag size                  5                   3, 12 
    */
    void Start()
    {
        maxDisplaywidth = currentSpeedDisplay.GetComponent<Renderer>().bounds.size.x;
        maxDisplayLoc = currentSpeedDisplay.transform.position;
        armIndex = 0;
        bodyPart = 0;
        feetIndex = 0;
        cannonIndex = 0;

        sqauds[bodyPart].GetComponent<Renderer>().material.color = Color.green;
        feetParts[feetIndex].SetActive(true);
        cannonParts[cannonIndex].SetActive(true);
        armParts[armIndex].SetActive(true);
        partstats.Add("feetspeed", new float[] { 20, 0, 0, 0, 0, 0 });
        partstats.Add("feetbalance", new float[] { 0, 0, 0.2f, 0, 0, 0 });
        partstats.Add("feetaccuracy", new float[] { 0, 15, 0, 0, 0, 0 });
        partstats.Add("cannonbalanced", new float[] { 0, 0, 0.2f, 0,0.5f, 0 });
        partstats.Add("cannonheavy", new float[] { 0, 0, 0, 2, 0, 0 });
        partstats.Add("armsbalanced", new float[] { 0, 0, 0.1f, 0, 0.5f, 0 });
        partstats.Add("armscapacity", new float[] { 0, 0, 0, 0, 0, 5 });
        partstats.Add("armsspeed", new float[] { 10, 0, 0, 0, 0, 0 });

        displayCurrentStats();
    }


    bool horPressed = true; 
    bool verPressed = true;
    // Update is called once per frame
    void Update()
    {
        gameModeselection = MenuController.gameModeSelection;
        if (gameModeselection == false)
        {

            float upDown = Input.GetAxisRaw(verAxis);
            float leftRight = Input.GetAxisRaw(horAxis);

            // Scroll through different sections of the body


            if (upDown == -1 && horPressed)
            {
                selectBodySection();
            }

            // Select feet
            if (bodyPart == 2 && leftRight == 1 && verPressed)
            {
                selectPart(ref feetParts, ref feetIndex);
                updateStatsPart(ref feetParts);
                displayCurrentStats();
            }

            // Select cannon
            if (bodyPart == 1 && leftRight == 1 && verPressed)
            {
                selectPart(ref cannonParts, ref cannonIndex);
                updateStatsPart(ref cannonParts);
                displayCurrentStats();
            }

            // Select arms
            if (bodyPart == 0 && leftRight == 1 && verPressed)
            {
                selectPart(ref armParts, ref armIndex);
                updateStatsPart(ref armParts);
                displayCurrentStats();
            }

            // make sure the vertical button is only pressed once
            if (upDown == 0)
            {
                horPressed = true;
            }
            else
            {
                horPressed = false;
            }

            // make sure the horizontal button is only pressed once
            if (leftRight == 0)
            {
                verPressed = true;
            }
            else
            {
                verPressed = false;
            }
        }
    }


    void displayCurrentStats()
    { 
        displayStat(ref currentSpeedDisplay, 150, 0);
        displayStat(ref currentAccuracyDisplay, 50, 1);
        displayStat(ref currentFireRateDisplay, 0.8f, 2);
        displayStat(ref currentPowerDisplay, 25, 3);
        displayStat(ref currentReloadTimeDisplay, 4, 4);
        displayStat(ref currentMagSizeDisplay, 12, 5);
    }

    void displayStat(ref GameObject display, float maxValue, int statIndex)
    {

        float currentRatio = current_stats[statIndex] / maxValue;
        float offset = maxDisplaywidth - (maxDisplaywidth * currentRatio);
        float newLoc = maxDisplayLoc.x - offset / 2;
        display.transform.localScale = new Vector3(currentRatio, 1, 1);
        display.transform.position = new Vector3(newLoc, display.transform.position.y, display.transform.position.z);
    }


    void updateStatsPart(ref GameObject[] partsArray)
    {
        for (int i = 0; i < partsArray.Length; i++)
        {
            if (partsArray[i].activeSelf)
            {
                for (int j = 0; j < current_stats.Length; j++)
                {
                    if (i == 0)
                    {
                        current_stats[j] = current_stats[j] - partstats[partsArray[partsArray.Length - 1].tag][j];
                    }
                    else
                    {
                        current_stats[j] = current_stats[j] - partstats[partsArray[i - 1].tag][j];
                    }
                    current_stats[j] = current_stats[j] + partstats[partsArray[i].tag][j];
                }
            }
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
