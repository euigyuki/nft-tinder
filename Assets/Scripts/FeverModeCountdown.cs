using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class FeverModeCountdown : MonoBehaviour
{   
    public TextMeshProUGUI CountDownText;
    // Start is called before the first frame update
    void Start()
    {
        CountDownText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        setCountDownValue();
    }

    public void setCountDownValue() {
        CountDownText.text = HypeLevelManager.countdownTimerStr;
    }
}
