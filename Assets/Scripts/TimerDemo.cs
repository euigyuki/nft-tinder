using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerDemo : MonoBehaviour
{
    [SerializeField] Timer timer1 ;
    public GameObject GameOverMenu;
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
            if(scene.name == "trendingDerrick") {
                SceneManager.LoadScene("SellCardUpdate");
            } else if(scene.name == "SellCardUpdate"){
                sellHelper.pushSellStats();
                if(PriceManager.currentDay==6){
                    StaticAnalytics.toJson();
                    GameOverMenu.SetActive(true);
                }
                else {
                    // TODO: Add intermediate scene here
                    SceneManager.LoadScene("sellSummary");
                    // SceneManager.LoadScene("trendingDerrick");
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
}
