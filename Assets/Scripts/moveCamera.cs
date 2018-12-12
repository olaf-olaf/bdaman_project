using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour
{
    public Transform myTransform;
    public Transform backPanel;
    public Transform frontPanel;
    private Quaternion backPanelRotation = new Quaternion(0f, 0f, 0f, 0f);
    private Quaternion frontPanelRotation = new Quaternion(0.99f, 0.99f, 0.99f, 0.99f);
    public float rotationSpeed = 5;
    //private bool gameModeSelection = false;
    // Use this for initialization


    //public GameObject GameController;         






        void awake()
    {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        //if (GameController.Instance == null){
        //    Instantiate(GameController);
           

        //}

        ////Instantiate gameManager prefab
        //Instantiate(GameController);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && GameController.Instance.gamemodeSelection == false && frontPanelRotation.w < 1f)
        {
            backPanelRotation = new Quaternion(1, 1, 1, 1);
            GameController.Instance.gamemodeSelection = true;
            print("ENTER PRESSED");
        }

        if (Input.GetKeyDown("space") && GameController.Instance.gamemodeSelection == true && backPanelRotation.w < 0.001f)
        {
            frontPanelRotation = new Quaternion(0, 0, 0, 0);
            GameController.Instance.gamemodeSelection = false;
        }

 



        if (GameController.Instance.gamemodeSelection == true  && backPanelRotation.w > 0.001f )
        {
            backPanelRotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(backPanel.position - myTransform.position), rotationSpeed * Time.deltaTime);
            myTransform.rotation = backPanelRotation;
            print("ROTATING GAME SELECTION");
            print(backPanelRotation.w);

        }

        if (GameController.Instance.gamemodeSelection == false && frontPanelRotation.w < 0.999999f )
        {
            frontPanelRotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(frontPanel.position - myTransform.position), rotationSpeed * Time.deltaTime);
            myTransform.rotation = frontPanelRotation;
            print("ROTATING TO PART SELECTION");
            print(frontPanelRotation.w);
        }
    }
}


