using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class NftBuyRecommDisplay : MonoBehaviour
{
    //public TextMeshProUGUI BuyRecommendation;
    // Start is called before the first frame update
    public Slider recommender;
    public Image Fill;
    private void Awake(){
        recommender = GetComponent<Slider>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        changeColorFill();
        setRecommender();
    }

    // void setBuyRecommendation() {
    //     BuyRecommendation.text = PriceManager.recommendToBuy();
    // }
    public void setRecommender(){
        double n = PriceManager.buyProb();
        recommender.value = (float)n;
    }
    public void changeColorFill()
    {
         if(ClickMode.Mode=="Normal"){
            Fill.color=new Color (0.38f,0.81f,0.43f,1.0f);
         }
         if(ClickMode.Mode=="ColorBlind"){
            Fill.color=new Color (0.047f,0.48f,0.863f,1.0f);
         }

    }
}
