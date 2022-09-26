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
    }

    // Update is called once per frame
    void Update()
    {
        setNftPrice();
    }

    void setNftPrice() {
        Price.text = "$" + PriceManager.getCurrentNftPrice();
    }
}
