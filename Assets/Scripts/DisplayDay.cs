using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class DisplayDay : MonoBehaviour
{
    public TextMeshProUGUI Day;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        setDayValue();
    }

    void setDayValue() {
         Day.text = "Day: " + PriceManager.currentDay.ToString();
    }
}
