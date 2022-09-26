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
    public TextMeshProUGUI BuyRecommendation;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        setBuyRecommendation();
    }

    void setBuyRecommendation() {
        BuyRecommendation.text = PriceManager.recommendToBuy();
    }
}
