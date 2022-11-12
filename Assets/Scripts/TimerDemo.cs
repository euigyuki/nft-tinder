using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerDemo : MonoBehaviour
{
    [SerializeField] Timer timer1 ;
    [SerializeField] phoneBehavior phone;
    public GameObject GameOverMenu;
    public TextMeshProUGUI countdownDisplay;
    public int Time;

    // Start is called before the first frame update
    void Start()
    {
        GameOverMenu.SetActive(false);
        timer1
        .SetDuration(Time)
        .OnEnd(() => {
            Debug.Log("Timer 1 ended");
            Scene scene = SceneManager.GetActiveScene();
            Debug.Log(scene.name);
            if(scene.name == "ObjectiveBuyDerrick") {
                endBuy();
                // SceneManager.LoadScene("ObjectiveSellDerrick");
            //if(scene.name == "ObjectiveBuyPage") {
            //    SceneManager.LoadScene("ObjectiveSellPage");
            } 
            else if(scene.name == "ObjectiveSellDerrick"){
                Debug.Log("ObjectiveSellPage pushing sell states");
                sellHelper.pushSellStats();
                if(PriceManager.currentDay==6){
                    StaticAnalytics.toJson();
                    GameOverMenu.SetActive(true);
                }
                else {
                    //SceneManager.LoadScene("ObjectiveSellDerrick");
                    SceneManager.LoadScene("sellSummary");
                }
            } else {
                GameOverMenu.SetActive(true);
            }

            
        })
        .Begin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endBuy(){
        timer1.SetPaused(true);
        phone.disablePhone();
        countdownDisplay.text = "FINISHED!";
        StartCoroutine(moveToSell());
    }

    IEnumerator moveToSell(){
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("ObjectiveSellDerrick");
    }
}
