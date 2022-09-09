using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDemo : MonoBehaviour
{
    [SerializeField] Timer timer1 ;
    // Start is called before the first frame update
    void Start()
    {
        timer1
        .SetDuration(10)
        .OnEnd(() => Debug.Log ("Timer 1 ended"))
        .Begin();
        // TODO: Gameover popup OnEnd
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
