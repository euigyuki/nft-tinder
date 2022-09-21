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
        Money.text = "Money: $"+money.ToString();
        genPrice();
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void passItem() {
        genPrice();
    }
    public void SubstractMoney() {
        money-=price;
        MoneyBar.instance.slider.value = MoneyBar.instance.slider.value - (float)price/6000;
        if(money<0) {
            MoneyBar.instance.Fill.color = Color.red;
        }
        else {
            MoneyBar.instance.Fill.color = Color.green;
        }
        
        Money.text = "Money: $"+money.ToString();
        genPrice();
    }

    public void genPrice(){
        price = Random.Range(500,3000);
        Price.text = "$"+price.ToString();
    }
}
