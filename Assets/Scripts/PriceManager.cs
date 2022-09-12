using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System;
using TMPro;
using UnityEngine.UI;

public class PriceManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public TextMeshProUGUI Money;
    public TextMeshProUGUI Price;
    public int price;
    public int money;
    

    public static PriceManager instance;

    private void Awake() {
        instance = this;
    }
    void Start()
    
    {
        price = Random.Range(1,10);
        Money.text = money.ToString();
        Price.text = price.ToString();
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void passItem() {
        price = Random.Range(1,10);
        Price.text = price.ToString();
    }
    public void SubstractMoney() {
        money-=price;
        Money.text = money.ToString();
        price = Random.Range(1,10);
        Price.text = price.ToString();
    }
}
