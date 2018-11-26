using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletBehaviour : MonoBehaviour {

    public GameObject bulletInstance;

    	
	// Update is called once per frame
	void Update () {
        if (bulletInstance.transform.position[1] < 0){
            print("GERONIMO!");
            Destroy(bulletInstance);
        }
    }


}
