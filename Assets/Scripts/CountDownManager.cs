using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CountDownManager : MonoBehaviour
{
    public int countdownTime;
    public TextMeshProUGUI countdownDisplay;

    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        StartCoroutine(CountdownToStart());   
    }

  
    IEnumerator CountdownToStart()
    {
        timer.SetPaused(true);
        while(countdownTime>0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1);
            countdownTime--;
        } 
        countdownDisplay.text = "GO!";
        yield return new WaitForSeconds(1);
        countdownDisplay.text = "";
        timer.SetPaused(false);

    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Wait is over");            

    }
}
