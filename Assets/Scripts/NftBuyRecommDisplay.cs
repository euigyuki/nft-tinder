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
        //setBuyRecommendation();
        Debug.Log("Update");
        setRecommender();
        //recommender.value= (float)n;
        // setRecommender(PriceManager.sellProb());
    }

    // void setBuyRecommendation() {
    //     BuyRecommendation.text = PriceManager.recommendToBuy();
    // }
    public void setRecommender(){
        double n = PriceManager.sellProb();
        recommender.value = (float)n;
        Debug.Log("Recommender");
        // Debug.Log(recommender.value);
    }
}
