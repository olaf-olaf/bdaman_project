using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
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

            GameObject.FindGameObjectWithTag("P1Select").GetComponent<Text>().text = "";
            GameObject.FindGameObjectWithTag("P2Select").GetComponent<Text>().text = "";
            print("ENTER PRESSED");
        }

        if (Input.GetKeyDown("backspace") && gameModeSelection == true && backPanelRotation.w < 0.001f)
        {
            frontPanelRotation = new Quaternion(0, 0, 0, 0);
            gameModeSelection = false;

            GameObject.FindGameObjectWithTag("P1Select").GetComponent<Text>().text = "use downarrow, rightarrow to select";
            GameObject.FindGameObjectWithTag("P2Select").GetComponent<Text>().text = "Use s,a to select";
        }
        if (Input.GetKeyDown("space") && gameModeSelection == true && backPanelRotation.w < 0.001f)
        {

            // initialise game
            GameObject.FindGameObjectWithTag("GameSettings").GetComponent<GameSettings>().initGame();
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


