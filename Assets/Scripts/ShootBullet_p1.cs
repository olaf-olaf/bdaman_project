using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet_p1 : MonoBehaviour
{

    public GameObject bullet;
    public int magazineSize;
    public float reloadTime;

    public GameObject player;
    public float spawnDistance;
    public Transform bulletSpawn;
    private GameObject chilBulletTwo;
    //private bool bulletShot = false;

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Return))
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
        childBullet.GetComponent<Rigidbody>().velocity = childBullet.transform.forward * 20;

        return childBullet;



        // Destroy the bullet after 2 seconds
        //childBullet(bullet, 1.0f);

    }
}