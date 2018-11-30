using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject bullet;
    public int magazineSize;
    public float reloadTime;
    public float power; 
    public float accuracy;
    public float fire_rate;

    public GameObject player;
    public float spawnDistance;
    public Transform bulletSpawn;
    public string fireButton;
    private GameObject chilBulletTwo; 

    // Update is called once per frame
    void Update()
    { 

        if (Input.GetKeyDown(fireButton))
        {
            chilBulletTwo = Fire(); 
        }

    }
    GameObject Fire()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 playerDirection = player.transform.forward;
        Quaternion playerRotation = player.transform.rotation;
        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;

        GameObject childBullet = Instantiate(bullet, spawnPos, playerRotation);
        childBullet.GetComponent<Rigidbody>().velocity = childBullet.transform.forward * power;
        childBullet.GetComponent<BulletBehaviour>().shooter =  player.tag;
   
        return childBullet;
         
    }
}