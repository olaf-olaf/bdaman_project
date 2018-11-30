using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{

    public GameObject bullet;
    public int magazineSize;
    private int remainingBullets;
    public float reloadTime;
    public float power; 
    public float accuracy;
    public float fireRate;

    public GameObject player;
    private float lastShot = 0.0f;
    public float spawnDistance;
    public Transform bulletSpawn;
    public string fireButton;
    private GameObject chilBulletTwo;
    public Text magazineText;

    private void Start()
    { 
        remainingBullets = magazineSize;
        updateMagazineUI();
    }


    // Update is called once per frame
    void Update()
    {
        // Determine if player is allowed to shoot 
        if (Time.time > fireRate + lastShot & remainingBullets > 0)
        {
            // Shoot  
            if (Input.GetKeyDown(fireButton))
            {
                Debug.Log(remainingBullets);
                chilBulletTwo = Fire();

            }
            updateMagazineUI();
        }

        // automatic reload
        if (remainingBullets <= 0)
        { 
            reload();
        } 
        

    }

    void updateMagazineUI()
    {
        magazineText.text = "Balls: " + remainingBullets.ToString() + "/" + magazineSize.ToString();
    }


    void reload()
    {
        
        lastShot = lastShot + reloadTime;
        magazineText.text = "RELOADING";
        remainingBullets = magazineSize;

    }


    GameObject Fire()
    {


        Vector3 playerPos = player.transform.position;
        Vector3 playerDirection = player.transform.forward;


        // add random rotation to the bullets
        float rotation_Y_Offset = 1.0f * accuracy; 
        Quaternion targetRotation = Quaternion.Euler(1, Random.Range(-rotation_Y_Offset, rotation_Y_Offset) , 1);

        Quaternion playerRotation = player.transform.rotation * targetRotation;
        
        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
        
        GameObject childBullet = Instantiate(bullet, spawnPos, playerRotation);
        childBullet.GetComponent<Rigidbody>().velocity = childBullet.transform.forward * power;
        childBullet.GetComponent<BulletBehaviour>().shooter = player.tag; 
        lastShot = Time.time;

        remainingBullets = remainingBullets - 1;
             
        return childBullet;
        
    }
}