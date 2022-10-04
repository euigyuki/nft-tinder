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
    private void Awake(){
        recommender = GetComponent<Slider>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        setRecommender();
    }

    // void setBuyRecommendation() {
    //     BuyRecommendation.text = PriceManager.recommendToBuy();
    // }
    public void setRecommender(){
        double n = PriceManager.buyProb();
        recommender.value = (float)n;
    }
}
