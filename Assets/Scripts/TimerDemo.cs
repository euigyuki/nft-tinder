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
                SceneManager.LoadScene("sell2");
            } else if(scene.name == "sell2"){
                SceneManager.LoadScene("trendingDerrick");
            } else {
                GameOverMenu.SetActive(true);
            }

            StaticAnalytics.toJson();
        })
        .Begin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
