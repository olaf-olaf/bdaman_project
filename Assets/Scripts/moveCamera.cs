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
    public static bool gameModeSelection = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && gameModeSelection == false && frontPanelRotation.w < 1f)
        {
            backPanelRotation = new Quaternion(1, 1, 1, 1);
            gameModeSelection = true;
            print("ENTER PRESSED");
        }

        if (Input.GetKeyDown("space") && gameModeSelection == true && backPanelRotation.w < 0.001f)
        {
            frontPanelRotation = new Quaternion(0, 0, 0, 0);
            gameModeSelection = false;
        }

 



        if (gameModeSelection == true  && backPanelRotation.w > 0.001f )
        {
            backPanelRotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(backPanel.position - myTransform.position), rotationSpeed * Time.deltaTime);
            myTransform.rotation = backPanelRotation;
        }

        if (gameModeSelection == false && frontPanelRotation.w < 0.999999f )
        {
            frontPanelRotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(frontPanel.position - myTransform.position), rotationSpeed * Time.deltaTime);
            myTransform.rotation = frontPanelRotation;
        }
    }
}


