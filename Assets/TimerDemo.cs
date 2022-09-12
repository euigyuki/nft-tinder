using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDemo : MonoBehaviour
{
    [SerializeField] Timer timer1 ;
    public GameObject GameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        GameOverMenu.SetActive(false);
        timer1
        .SetDuration(5)
        .OnEnd(() => {
            Debug.Log("Timer 1 ended");
            GameOverMenu.SetActive(true);
        })
        .Begin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
