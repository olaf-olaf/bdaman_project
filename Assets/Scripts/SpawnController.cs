/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {


    public GameObject[] pins;
    public Vector3 spawnValues;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public bool stop;
    private bool  pcgOn = true;

    int randPin; 
	void Start () {
        GameSettings GameSettings = GameObject.FindGameObjectWithTag("GameSettings").GetComponent<GameSettings>();
        pcgOn = GameSettings.contentGeneration;

        foreach (GameObject pin in pins)
        {
            pin.SetActive(false);
        } 
        StartCoroutine(waitSpawner());
	}
	 
	void Update () {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait) + Time.time;
	}
    IEnumerator waitSpawner()
    {
        while (pcgOn)
        {
            if (spawnWait < Time.time)
            {

                randPin = Random.Range(0, 2);
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 1, Random.Range(-spawnValues.z, spawnValues.z));
                pins[randPin].SetActive(true);

                pins[randPin].transform.position = spawnPosition;
                //Instantiate(pins[randPin], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
                yield return new WaitForSeconds(spawnWait);
                pins[randPin].SetActive(false);
            }
        }
    }

}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnController : MonoBehaviour
{ 
    public GameObject pin;                                 //The column game object.
public int columnPoolSize = 2;                                  //How many columns to keep on standby.
public float spawnRate = 10f;                                    //How quickly columns spawn.
public float columnMin = -8f;                                   //Minimum y value of the column position.
public float columnMax = 8f;                                  //Maximum y value of the column position.
    public bool pcgOn;
private GameObject[] pins;                                   //Collection of pooled columns.
private int currentColumn = 0;                                  //Index of the current column in the collection.

private Vector3 objectPoolPosition = new Vector3(-15,1000,1000);     //A holding position for our unused columns offscreen.
 
private float timeSinceLastSpawned;


void Start()
{
    timeSinceLastSpawned = 0f;

        //Initialize the columns collection.
        pins = new GameObject[columnPoolSize];
    //Loop through the collection... 
    for (int i = 0; i < columnPoolSize; i++)
    {
            //...and create the individual columns.
            pins[i] = (GameObject)Instantiate(pin, objectPoolPosition, Quaternion.identity);
    }
}


//This spawns columns as long as the game is not over.
void Update()
{
    timeSinceLastSpawned += Time.deltaTime;

    if (pcgOn == true && timeSinceLastSpawned >= spawnRate)
    {
        timeSinceLastSpawned = 0f;

        //Set a random y position for the column
        float spawnYPosition = Random.Range(columnMin, columnMax);
        float spawnXPosition = Random.Range(columnMin/2, columnMax/2);

            //...then set the current column to that position.
            pins[currentColumn].transform.position = new Vector3(spawnXPosition, 1, spawnYPosition);

        //Increase the value of currentColumn. If the new size is too big, set it back to zero
        currentColumn++;

        if (currentColumn >= columnPoolSize)
        {
            currentColumn = 0;
        }
    }
}
}


