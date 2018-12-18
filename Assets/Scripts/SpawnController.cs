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
    public bool  pcgOn = true;

    int randPin; 
	void Start () {
         
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
