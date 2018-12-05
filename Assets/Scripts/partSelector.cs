using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class partSelector : MonoBehaviour
{
    public GameObject[] sqauds;
    public GameObject[] feetParts;
    public GameObject[] cannonParts;
    public GameObject[] armParts;
    public GameObject currentSpeedDisplay;
    public GameObject currentAccuracyDisplay;
    public GameObject currentFireRateDisplay;

    private Vector3 maxDisplayLoc;
    private float maxDisplaywidth;
    private Dictionary<string, float[]> partstats = new Dictionary<string, float[]>();
    private float[] current_stats = { 120, 25, 0.3f, 20, 3, 5 };



    private int armIndex;
    private int bodyPart;
    private int feetIndex;
    private int cannonIndex;

    // Use this for initialization
    // these values need to be set in the game menu
    /*         NAME           Default               Range
     * movement_speed           120                  100, 150 
     * accuracy                  1                   1, 50  
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
        partstats.Add("cannonbalanced", new float[] { 0, 0, 0.2f, 0, 0, 0 });
        partstats.Add("cannonheavy", new float[] { 0, 0, 0, 2, 0, 0 });
        partstats.Add("armsbalanced", new float[] { 0, 0, 0.1f, 0, 0, 0 });
        partstats.Add("armscapacity", new float[] { 0, 0, 0, 0, 0, 5 });
        partstats.Add("armsspeed", new float[] { 10, 0, 0, 2, 0, 0 });
    }

    // Update is called once per frame
    void Update()
    {

        // Scroll through different sections of the body
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectBodySection();
        }

        // Select feet
        if (bodyPart == 2 && Input.GetKeyDown("right"))
        {
            selectPart(ref feetParts, ref feetIndex);
            updateStatsPart(ref feetParts);
            displayCurrentStats();

        }

        // Select cannon
        if (bodyPart == 1 && Input.GetKeyDown("right"))
        {
            selectPart(ref cannonParts, ref cannonIndex);
            updateStatsPart(ref cannonParts);
            displayCurrentStats();
        }

        // Select arms
        if (bodyPart == 0 && Input.GetKeyDown("right"))
        {
            selectPart(ref armParts, ref armIndex);
            updateStatsPart(ref armParts);
            displayCurrentStats();
        }

        // Display stats
    }


    void displayCurrentStats()
    {

        displayStat(ref currentSpeedDisplay, 150, 0);
        displayStat(ref currentAccuracyDisplay, 50, 1);
        displayStat(ref currentFireRateDisplay, 0.8f, 2);
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
