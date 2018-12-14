using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{ 
    public GameObject[] feetParts;
    public GameObject[] cannonParts;
    public GameObject[] armParts;
    public GameObject bullet;
    public int magazineSize;
    private int remainingBullets;
    public float reloadTime;
    public float power;
    public float accuracy;
    public float fireRate;
    public float initRotation;


    private Rigidbody rb; 
    public float speed;
    public string horAxis;
    public string verAxis;


    public GameObject player;
    private float lastShot = 0.0f;
    public float spawnDistance;
    public Transform bulletSpawn;
    public string fireButton;
    private GameObject chilBulletTwo;
    public Text magazineText;
    public List<int> bodyIndex;

    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();


        // set the player arms/barrel/feet
        armParts[bodyIndex[0]].SetActive(true);
        feetParts[bodyIndex[1]].SetActive(true);
        cannonParts[bodyIndex[2]].SetActive(true); 
         
        remainingBullets = magazineSize;
        updateMagazineUI();
    }

    // Update is called once per frame
    void Update()
    {
        bool paused = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().gamePaused;
         
            // Determine if player is allowed to shoot 
            if (Time.time > fireRate + lastShot & remainingBullets > 0 & !paused)
        {
            // Shoot  
            if (Input.GetKeyDown(fireButton))
            { 
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

    // update magazine text
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

    // fire bullet
    GameObject Fire()
    {

        // add random rotation to the bullets
        float rotation_Y_Offset = 1.0f * accuracy;
        Quaternion targetRotation = Quaternion.Euler(1, Random.Range(-rotation_Y_Offset, rotation_Y_Offset), 1);

        Quaternion playerRotation = player.transform.Find("barrelDirection").gameObject.transform.rotation * targetRotation;
        Vector3 playerPos = player.transform.Find("barrelDirection").gameObject.transform.position;
        Vector3 playerDirection = player.transform.Find("barrelDirection").gameObject.transform.forward;
         
        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
      

        GameObject childBullet = Instantiate(bullet, spawnPos, playerRotation  );
        childBullet.GetComponent<Rigidbody>().velocity = childBullet.transform.forward * power;
        childBullet.GetComponent<BulletBehaviour>().shooter = player.tag;
        lastShot = Time.time;

        remainingBullets = remainingBullets - 1;

        return childBullet;

    }

    // player movement
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis(horAxis);
        float moveAxis = Input.GetAxis(verAxis);


        bool paused = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().gamePaused;

        if (!paused)
        {
            Vector3 movement = new Vector3(moveHorizontal, 0, 0);

            rb.AddForce(movement * speed);
            player.transform.Rotate(0, moveAxis, 0);
        }



    }
}