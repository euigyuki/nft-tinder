using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class DisplayPortfolioValue : MonoBehaviour
{
    public TextMeshProUGUI Portfolio;
    // Start is called before the first frame update
    void Start()
    {
        // InvokeRepeating("setPortfolioValue", 0, 0.000015f);
    }

    // Update is called once per frame
    void Update()
    {
        setPortfolioValue();
    }

    void setPortfolioValue() {
        Portfolio.text = "Portfolio: $" + PriceManager.getPortfolioValue().ToString();
    }
}
