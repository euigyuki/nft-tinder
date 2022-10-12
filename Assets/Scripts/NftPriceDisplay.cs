using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class NftPriceDisplay : MonoBehaviour
{
    public TextMeshProUGUI Price;
    // Start is called before the first frame update
    void Start()
    {
        // setNftPrice();
        InvokeRepeating("setNftPrice", 0, 0.000015f);
    }

    // Update is called once per frame
    void Update()
    {
        setNftPrice();
    }

    void setNftPrice() {
        if (!PriceManager.offerDiscount) {
            // Price.text = "$" + PriceManager.getCurrentNftPrice();
            Price.text = String.Format("${0:0.##}", PriceManager.getCurrentNftPrice());
        }
        else {
            // Price.text = "$" + PriceManager.getCurrentNftPrice() + " (-20%)";
            Price.text = String.Format("${0:0.##} (-20%)", PriceManager.getCurrentNftPrice());
        }
    }
}
