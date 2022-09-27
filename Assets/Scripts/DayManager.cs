using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DayManager : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public TextMeshProUGUI Day;
    public float timeStart = 60;
    public int day = 0;
    public static DayManager instance;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Day.text = "Day: "+day.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timeStart = Mathf.Round(timeStart - Time.deltaTime);
       
    
        Day.text = "Day: "+day.ToString();
        if(Input.GetKeyDown("u")){
            day+=1;
            StartCoroutine(Test());

        }
    }



    IEnumerator Test()
    {
        yield return new WaitForSeconds(1);
        //Debug.Log("Wait is over");            

    }

  
 

 
}
