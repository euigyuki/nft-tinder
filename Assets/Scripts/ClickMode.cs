using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ClickMode : MonoBehaviour
{
    // public Button NormalMode;
    public static String Mode="Normal";
    // public Button ColorblindMode;
    // Start is called before the first frame update
    public static Toggle CBMode;

    void Start()
    {
        // Button btn1 = NormalMode.GetComponent<Button>();
        // Button btn2 = ColorblindMode.GetComponent<Button>();
        // btn1.onClick.AddListener(TaskOnClick1);
        // btn2.onClick.AddListener(TaskOnClick2);
        CBMode =  GetComponent<Toggle>();
       
        CBMode.onValueChanged.AddListener(delegate {
                ToggleValueChanged(CBMode);
            });
       


        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mode=="ColorBlind")
        {
            CBMode.isOn=true;
        }
       
        
    }
    //  void TaskOnClick1(){
    //     Debug.Log ("You have clicked the button!");
    //     Mode="Normal";
    //     Debug.Log(Mode);

    // }
    //  void TaskOnClick2(){
    //     Debug.Log ("You have clicked the button!");
    //     Mode="ColorBlind";
    //     Debug.Log(Mode);

    // }
    void ToggleValueChanged(Toggle change)
    {
         if(CBMode.isOn)
        {
            Mode="ColorBlind";
            Debug.Log("ColorBlind");
        }
        else
        {
            Mode="Normal";
             Debug.Log("Normal");
        }
        
    }
     
}
