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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownToStart());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CountdownToStart()
    {
        while(countdownTime>0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1);
            countdownTime--;
        } 
        countdownDisplay.text = "GO!";
        yield return new WaitForSeconds(1);
        countdownDisplay.text = "";

    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Wait is over");            

    }
}
