using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class DisplayMoney : MonoBehaviour
{
    public TextMeshProUGUI Money;
    // Start is called before the first frame update
    void Start()
    {
        setMoneyValue();
    }

    // Update is called once per frame
    void Update()
    {
        setMoneyValue();
    }

    void setMoneyValue() {
         Money.text = "Money: $" + PriceManager.walletValue;
    }
}
