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
    public bool  pcgOn = false;

    int randPin;
	// Use this for initialization
	void Start () {


        foreach (GameObject pin in pins)
        {
            pin.SetActive(false);
        } 
        StartCoroutine(waitSpawner());
	}
	
	// Update is called once per frame
	void Update () {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
	}
    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWait);
        while (!stop & pcgOn)
        {
            randPin = Random.Range(0, 2);
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 1, Random.Range(-spawnValues.z, spawnValues.z));
            Instantiate(pins[randPin], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
            yield return new WaitForSeconds(spawnWait);
      
        }
    }

}
