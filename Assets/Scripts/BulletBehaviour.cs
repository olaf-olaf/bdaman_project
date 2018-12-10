using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletBehaviour : MonoBehaviour {

    public GameObject bulletInstance;
    public float m_MaxDamage;                    // The amount of damage done if the explosion is centred on a tank.
    public float m_ExplosionForce = 1000f;              // The amount of force added to a tank at the centre of the explosion.
    public float m_MaxLifeTime = 2f;                    // The time in seconds before the shell is removed.  
    public string shooter;

 
    // Update is called once per frame
    void Update () {
        if (bulletInstance.transform.position[1] < 0){
            print("GERONIMO!");
            Destroy(bulletInstance);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //behaviour when hitting the opponent 
        if ((collision.gameObject.tag == "Player1" | collision.gameObject.tag == "Player2") & collision.gameObject.tag != shooter)
        {
            var hit = collision.gameObject; 
            PlayerHealth health = hit.GetComponent<PlayerHealth>();
            var vel = this.GetComponent<Rigidbody>().velocity;      //to get a Vector3 representation of the velocity
            float speed = vel.magnitude;

            Debug.Log(speed);

            float damage = 10f; 
            health.TakeDamage(damage);
            Destroy(bulletInstance);
        }
         
         
    }
   

    private void Start()
    {
        // If it isn't destroyed by then, destroy the shell after it's lifetime.
        Destroy(gameObject, m_MaxLifeTime);
    }


    


}
