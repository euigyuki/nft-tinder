using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class NftNameDisplay : MonoBehaviour
{
    public TextMeshProUGUI Name;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("setNftName", 0, 0.000015f);
    }

    // Update is called once per frame
    void Update()
    {
        setNftName();
    }

    void setNftName() {
        Name.text = PriceManager.getNftId();
    }
}
